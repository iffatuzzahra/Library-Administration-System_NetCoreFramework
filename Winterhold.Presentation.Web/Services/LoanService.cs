using Microsoft.AspNetCore.Mvc.Rendering;
using Winterhold.Bussiness.Interfaces;
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.ViewModels;
 

namespace Winterhold.Presentation.Web.Services;

public class LoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IBookRepository _bookRepository;

    public LoanService(ILoanRepository loanRepository, ICustomerRepository customerRepository, IBookRepository bookRepository)
    {
        _loanRepository = loanRepository;
        _customerRepository = customerRepository;
        _bookRepository = bookRepository;
    }

    public LoanIndexViewModel GetAllLoans(string? title, string? name, bool? isDuePass, int pageNumber, int pageSize)
    {
        var loans = _loanRepository.GetAll(title, name, isDuePass, pageNumber, pageSize)
            .Select(l => new LoanIndexTableViewModel
            {
                Id = l.Id,
                CustomerFirstName = l.CustomerNumberNavigation.FirstName,
                CustomerLastName = l.CustomerNumberNavigation.LastName,
                BookTitle = l.BookCodeNavigation.Title,
                LoanDate = l.LoanDate,
                DueDate = l.DueDate,
                ReturnDate = l.ReturnDate
            }).ToList();
        return new LoanIndexViewModel
        {
            Title = title,
            Name = name,
            Loans = loans,
            PaginationInfo = new PaginationInfoViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = _loanRepository.CountAll(title, name, isDuePass)
            }
        };
    }
    public LoanUpsertIndexViewModel GetLoanUpsertVmById(long id)
    {
        var loan = _loanRepository.GetById(id);
        var loanUpsert = new LoanUpsertViewModel
        {
            Id = loan.Id,
            CustomerNumber = loan.CustomerNumber,
            BookCode = loan.BookCode,
            LoanDate = loan.LoanDate,
            Note = loan.Note
        };
        var customers = GetNonExpiredCustomerList();
        var books = GetAvailableBookList();
        return new LoanUpsertIndexViewModel
        {
            Loan = loanUpsert,
            Customers = customers,
            Books = books, 
            BookTitle = loan.BookCodeNavigation.Title,
            CustomerName = loan.CustomerNumberNavigation.FirstName + " " + loan.CustomerNumberNavigation.LastName
        };
    }
    public List<SelectListItem> GetNonExpiredCustomerList() {
    return _customerRepository.GetNonExpiredCustomers()
            .Select(c => new SelectListItem
            {
                Value = c.MembershipNumber,
                Text = $"{c.FirstName} {c.LastName}"
            }).ToList();
    }
    public List<SelectListItem> GetAvailableBookList()
    {
        return _bookRepository.GetAllAvailableBooks()
        .Select(b => new SelectListItem
        {
            Value = b.Code,
            Text = b.Title
        }).ToList();
    }
    public void InsertLoan(LoanUpsertViewModel vm)
    {
        var newLoan = new Loan
        {
            CustomerNumber = vm.CustomerNumber,
            BookCode = vm.BookCode,
            LoanDate = vm.LoanDate,
            DueDate = vm.LoanDate.AddDays(5), 
            Note = vm.Note
        }; 
        _loanRepository.Insert(newLoan);

        var book = _bookRepository.GetBookByCode(vm.BookCode); 
        book.IsBorrowed = true;
        _bookRepository.Update(book);
    }
    public void UpdateLoan(LoanUpsertViewModel vm)
    {
        var loan = _loanRepository.GetById(vm.Id);

        loan.CustomerNumber = vm.CustomerNumber;
        loan.BookCode = vm.BookCode;
        loan.LoanDate = vm.LoanDate;
        loan.Note = vm.Note;

        if (loan.BookCode != vm.BookCode)
        {
            //var returnBook = _bookRepository.GetBookByCode(loan.BookCode);
            //returnBook.IsBorrowed = false;
            
            //var borrowBook = _bookRepository.GetBookByCode(vm.BookCode);
            //borrowBook.IsBorrowed = true;

            _loanRepository.Update(loan, loan.BookCode, vm.BookCode);
        } 
        else
        {
            _loanRepository.Update(loan);
        }
    }
    public void ReturnLoanBook(long id) 
    {
        var loan = _loanRepository.GetById(id);
        loan.ReturnDate = DateTime.Now;
        _loanRepository.Update(loan);

        var book = _bookRepository.GetBookByCode(loan.BookCode);
        book.IsBorrowed = false;
        _bookRepository.Update(book);
    }
    public LoanDetailViewModel GetLoanDetail(long id)
    {
        var loan = _loanRepository.GetLoanDetailById(id);
        return new LoanDetailViewModel
        {
            MembershipNumber = loan.CustomerNumber,
            MemberFullName = loan.CustomerNumberNavigation.FirstName + " " + loan.CustomerNumberNavigation.LastName,
            MemberPhone = loan.CustomerNumberNavigation.Phone,
            MembershipExpireDate = loan.CustomerNumberNavigation.MembershipExpireDate,
            BookTitle = loan.BookCodeNavigation.Title,
            BookAuthorFullName = loan.BookCodeNavigation.Author.Title + ". " 
                                    + loan.BookCodeNavigation.Author.FirstName + " " 
                                    + loan.BookCodeNavigation.Author.LastName,
            BookCategory = loan.BookCodeNavigation.CategoryNameNavigation.Name,
            CategoryFloor = loan.BookCodeNavigation.CategoryNameNavigation.Floor,
            CategoryIsle = loan.BookCodeNavigation.CategoryNameNavigation.Isle,
            CategoryBay = loan.BookCodeNavigation.CategoryNameNavigation.Bay
        };
    }
}
