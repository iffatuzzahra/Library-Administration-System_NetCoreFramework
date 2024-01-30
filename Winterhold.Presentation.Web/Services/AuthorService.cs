 
using Winterhold.Bussiness.Interfaces;
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Services;

public class AuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;

    public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
    }
    public AuthorIndexViewModel GetAllAuthor(string? name, int pageNumber, int pageSize)
    {
        var authors = _authorRepository.GetAll(name, pageNumber, pageSize)
            .Select(a =>
            new AuthorIndexTableViewModel
            {
                Id = a.Id,
                Title = a.Title,
                FirstName = a.FirstName,
                LastName = a.LastName,
                BirthDate = a.BirthDate,
                DeceasedDate = a.DeceasedDate,
                Education = a.Education
            }
        ).ToList();
        var pagination = new PaginationInfoViewModel
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = _authorRepository.GetTotal(name),
        };
        return new AuthorIndexViewModel { 
            Authors = authors, 
            Name = name, 
            PaginationInfo = pagination
        };
    }
    public AuthorUpsertViewModel GetUpsertVmByAuthorId(long id)
    {
        var author = _authorRepository.GetById(id);
        return new AuthorUpsertViewModel
        {
            Id = author.Id, 
            Title = author.Title,
            FirstName= author.FirstName,
            LastName= author.LastName,
            BirthDate = author.BirthDate,
            DeceasedDate= author.DeceasedDate,
            Education = author.Education,
            Summary= author.Summary
        };
    }
    public void InsertAuthor(AuthorUpsertViewModel vm)
    {
        var author = new Author
        {
            Title = vm.Title,
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            BirthDate = vm.BirthDate,
            DeceasedDate = vm.DeceasedDate,
            Education = vm.Education,
            Summary = vm.Summary
        };
        _authorRepository.Insert(author);
    }
    public void UpdateAuthor(AuthorUpsertViewModel vm)
    {
        var author = _authorRepository.GetById(vm.Id);
        author.Title = vm.Title;
        author.FirstName = vm.FirstName;
        author.LastName = vm.LastName;
        author.BirthDate = vm.BirthDate;
        author.DeceasedDate = vm.DeceasedDate;
        author.Education = vm.Education;
        author.Summary = vm.Summary;

        _authorRepository.Update(author);
    }
    public void DeleteAuthor(long id)
    {
        var author = _authorRepository.GetById(id);
        author.DeleteDate = DateTime.Now;
        _authorRepository.Delete(author); 
    }
    public AuthorBooksIndexViewModel GetAuthorDetails(long id, int pageNumber, int pageSize)
    {
        var books = _bookRepository.GetBooksByAuthorId(id, pageNumber, pageSize)
            .Select(b => new AuthorBooksTableViewModel
            {
                Title = b.Title, 
                CategoryName = b.CategoryName, 
                IsBorrowed = b.IsBorrowed? "Borrowed" : "Available",
                ReleaseDate = b.ReleaseDate, 
                TotalPage = b.TotalPage
            }).ToList();

        var author = _authorRepository.GetById(id);

        return new AuthorBooksIndexViewModel
        {
            Id = id,
            Title = author.Title,
            FirstName  = author.FirstName,
            LastName = author.LastName,
            BirthDate = author.BirthDate,
            DeceasedDate = author.DeceasedDate,
            Education = author.Education,
            Summary = author.Summary, 
            Books = books, 
            PaginationInfo = new PaginationInfoViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize, 
                TotalItems = _bookRepository.CountBooksByAuthorId(id)
            }
        };
    }
}
