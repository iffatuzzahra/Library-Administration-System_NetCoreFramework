using Microsoft.AspNetCore.Mvc.Rendering;
using Winterhold.Bussiness.Interfaces;
using Winterhold.Bussiness.Repositories;
 
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Services;

public class BookService
{
    private readonly IBookRepository _bookrepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(IBookRepository bookrepository, IAuthorRepository authorRepository)
    {
        _bookrepository = bookrepository;
        _authorRepository = authorRepository;
    }

    public BookIndexViewModel GetBookByCategory(string? title, string? author, bool? isAvailable, string category, int pageNumber, int pageSize)
    {
        var books = _bookrepository.GetBooksByCategoryName(title, author, isAvailable, category, pageNumber, pageSize)
            .Select(b =>
            new BookIndexTableViewModel
            {
                Code = b.Code,
                Title = b.Title,
                AuthorFirstName = b.Author.FirstName == null ? null : b.Author.FirstName,
                AuthorLastName = b.Author.LastName == null ? null : b.Author.LastName,
                AuthorTitle = b.Author.Title == null ? null : b.Author.Title,
                IsBorrowed = b.IsBorrowed ? "Borrowed" : "Available",
                ReleaseDate = b.ReleaseDate,
                TotalPage = b.TotalPage, 
                Summary = b.Summary
            }).ToList();

        return new BookIndexViewModel {
            Books = books,
            CategoryName = category,
            PaginationInfo = new PaginationInfoViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = _bookrepository.CountBooksByCategoryName(title, author, isAvailable, category)
            }
        };
    }
    public BookUpsertIndexViewModel GetBookUpsertVmByCode(string code)
    {
        var book = _bookrepository.GetBookByCode(code);
        var vmBook = new BookUpsertViewModel
        {
            Code = book.Code,
            Title = book.Title,
            CategoryName = book.CategoryName,
            AuthorId = book.AuthorId,
            Summary = book.Summary,
            ReleaseDate = book.ReleaseDate,
            TotalPage = book.TotalPage
        };

        return new BookUpsertIndexViewModel
        {
            Book = vmBook,
            Authors = GetAuthorList()
        };
    }
    public List<SelectListItem> GetAuthorList()
    {
        return _authorRepository.GetAll()
           .Select(a => new SelectListItem
           {
               Value = a.Id.ToString(),
               Text = $"{a.Title} {a.FirstName} {a.LastName}",
           }).ToList();
    }
    public void InsertBook(BookUpsertViewModel vm)
    {
        var newBook = new Book
        {
            Code = vm.Code,
            Title = vm.Title,
            CategoryName = vm.CategoryName,
            AuthorId = vm.AuthorId,
            Summary = vm.Summary,
            ReleaseDate = vm.ReleaseDate == DateTime.MinValue ? null : vm.ReleaseDate,
            TotalPage = vm.TotalPage
        };
        _bookrepository.Insert(newBook);
    }
    public void UpdateBook(BookUpsertViewModel vm)
    {
        var book = _bookrepository.GetBookByCode(vm.Code);
        book.Title = vm.Title;
        book.CategoryName = vm.CategoryName;
        book.AuthorId = vm.AuthorId;
        book.Summary = vm.Summary;
        book.ReleaseDate = vm.ReleaseDate;
        book.TotalPage = vm.TotalPage;

        _bookrepository.Update(book);
    }
    public void DeleteBook(string code)
    {
        var book = _bookrepository.GetBookByCode(code); 
        book.DeleteDate = DateTime.Now;
        _bookrepository.Delete(book);
    }
}
