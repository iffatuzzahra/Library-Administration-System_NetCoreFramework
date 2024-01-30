using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.Bussiness.Interfaces;
using Winterhold.DataAccess.Models;

namespace Winterhold.Bussiness.Repositories;

public class BookRepository : IBookRepository
{
    private WinterholdContext _dBContext;

    public BookRepository(WinterholdContext dBContext)
    {
        _dBContext = dBContext;
    }
    public int CountBooksByAuthorId(long authorId)
    {
        return _dBContext.Books.Where(b => b.AuthorId == authorId)
            .Where(b => b.DeleteDate == null)
            .Count();
    }
    public List<Book> GetBooksByAuthorId(long authorId, int pageNumber, int pageSize)
    {
        return _dBContext.Books.Where(b => b.AuthorId == authorId)
            .Where(b => b.DeleteDate == null)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToList();
    }
    public int CountBooksByCategoryName(string? title, string? author, bool? isAvailable, string category)
    {
        return _dBContext.Books
            .Include(b => b.Author)
            .Where(b => b.CategoryName == category)
            .Where(b => b.DeleteDate == null)
            .Where(b => b.Title.Contains(title) || title == null)
            //.Where(b => b.Author.FirstName.Contains(author) || b.Author.LastName.Contains(author) || author == null)
            .Where(a => (a.Author.FirstName + " " + a.Author.LastName).Contains(author) || author == null)
            .Where(b => b.IsBorrowed != isAvailable || isAvailable == null)
            .Count();
    }
    public int CountBooksByCategoryName(string category)
    {
        return _dBContext.Books
            .Where(b => b.CategoryName == category)
            .Where(b => b.DeleteDate == null)
            .Count();
    }
    public List<Book> GetBooksByCategoryName(string? title, string? author, bool? isAvailable, string category, int pageNumber, int pageSize)
    {
        return _dBContext.Books
            .Include(b => b.Author)
            .Where(b => b.CategoryName == category)
            .Where(b => b.DeleteDate == null)
            .Where(b => b.Title.Contains(title) || title == null)
            //.Where(b => b.Author.FirstName.Contains(author) || b.Author.LastName.Contains(author) || author == null)
            .Where(a => (a.Author.FirstName + " " + a.Author.LastName).Contains(author) || author == null)
            .Where(b => b.IsBorrowed != isAvailable || isAvailable == null)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToList();
    }
    public Book GetBookByCode(string code)
    {
        _dBContext.Books.Find(code);               // single maksudnya, jika datanya banyak dia akan throw exeption
        return _dBContext.Books
            .Where(b => b.DeleteDate == null)
            .FirstOrDefault(b => b.Code.Equals(code))
               ?? throw new NullReferenceException($"Book not found with Code ${code}");
    }
    public void Insert(Book book)
    {
        _dBContext.Books.Add(book);
        _dBContext.SaveChanges();

    }
    public void Update(Book book)
    {
        _dBContext.Books.Update(book);
        _dBContext.SaveChanges();
    }
    public void Delete(Book book)
    {
        _dBContext.Books.Update(book);
        //_dBContext.Authors.Remove(author);
        _dBContext.SaveChanges();
    }
    public List<Book> GetAllAvailableBooks()
    {
        return _dBContext.Books
            .Where(b => b.DeleteDate == null)
            .Where(b => b.IsBorrowed == false)
            .ToList ();
    }
}
