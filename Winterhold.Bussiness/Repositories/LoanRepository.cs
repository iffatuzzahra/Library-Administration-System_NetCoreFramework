using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.Bussiness.Interfaces;
using Winterhold.DataAccess.Models;

namespace Winterhold.Bussiness.Repositories;

public class LoanRepository : ILoanRepository
{
    private WinterholdContext _dBContext;

    public LoanRepository(WinterholdContext dBContext)
    {
        _dBContext = dBContext;
    }
    public List<Loan> GetAll(string? title, string? name, bool? isDuePass, int pageNumber, int pageSize)
    {
        return _dBContext.Loans
            .Include(l => l.CustomerNumberNavigation)
            .Include(l => l.BookCodeNavigation)
            .Where(c => c.BookCodeNavigation.Title.Contains(title) || title == null)
            .Where(c => c.CustomerNumberNavigation.FirstName.Contains(name) || c.CustomerNumberNavigation.LastName.Contains(name) || name == null)
            .Where(c => (isDuePass == null
                        || (isDuePass.Value && c.DueDate < DateTime.Now && c.ReturnDate == null)
                        || (!isDuePass.Value && c.DueDate >= DateTime.Now)
                        ))
            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToList();
    }
    public int CountAll(string? title, string? name, bool? isDuePass)
    {
        return _dBContext.Loans
            .Include(l => l.CustomerNumberNavigation)
            .Include(l => l.BookCodeNavigation)
            .Where(c => c.BookCodeNavigation.Title.Contains(title) || title == null)
            .Where(c => c.CustomerNumberNavigation.FirstName.Contains(name) || c.CustomerNumberNavigation.LastName.Contains(name) || name == null)
            .Where(c => (isDuePass == null
                        || (isDuePass.Value && c.DueDate < DateTime.Now && c.ReturnDate != null)
                        || (!isDuePass.Value && c.DueDate >= DateTime.Now)
                        ))
            .Count();
    }
    public Loan GetById(long id)
    {
        _dBContext.Loans.Find(id);
        return _dBContext.Loans
            .Include(b => b.BookCodeNavigation)
            .Include(b => b.CustomerNumberNavigation)
            .FirstOrDefault(b => b.Id == id)
               ?? throw new NullReferenceException($"Loan not found with Id ${id}");
    }
    public Loan GetLoanDetailById(long id)
    {
        _dBContext.Loans.Find(id);
        return _dBContext.Loans
            .Include(l => (l.BookCodeNavigation))
            .Include(l => (l.CustomerNumberNavigation))
            .Include(l => l.BookCodeNavigation.CategoryNameNavigation)
            .Include(l => l.BookCodeNavigation.Author)
            .FirstOrDefault(b => b.Id == id)
                ?? throw new NullReferenceException($"Loan not found with Id ${id}");
        
    }
    public void Insert(Loan loan)
    {
        _dBContext.Loans.Add(loan);
        _dBContext.SaveChanges();
    }
    public void Update(Loan loan)
    {
        _dBContext.Loans.Update(loan);
        _dBContext.SaveChanges();
    }
    public void Update(Loan loan, string returnBook, string borrowBook)
    {
        var updateReturnBook = _dBContext.Books.First(b => b.Code == returnBook);
        updateReturnBook.IsBorrowed = false;
        _dBContext.Books.Update(updateReturnBook);

        var updateBorrowBook = _dBContext.Books.First(b => b.Code == borrowBook);
        updateReturnBook.IsBorrowed = true;
        _dBContext.Books.Update(updateBorrowBook);

        _dBContext.Loans.Update(loan);
        _dBContext.SaveChanges();
    }
    public void Delete(Loan loan)
    {
        _dBContext.Loans.Update(loan);
        //_dBContext.Authors.Remove(author);
        _dBContext.SaveChanges();
    }

}
