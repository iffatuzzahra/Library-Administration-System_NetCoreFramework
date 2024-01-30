using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.Bussiness.Interfaces;

public interface IBookRepository
{
    public List<Book> GetBooksByAuthorId(long authorId, int pageNUmber, int pageSize);
    public int CountBooksByCategoryName(string? title, string? author, bool? isAvailable, string category);
    public int CountBooksByCategoryName(string category);
    public int CountBooksByAuthorId(long id);
    public List<Book> GetBooksByCategoryName(string? title, string? author, bool? isAvailable, string category, int pageNumber, int pageSize);
    public Book GetBookByCode(string code);
    public void Insert(Book book);
    public void Update(Book book);
    public void Delete(Book book);
    public List<Book> GetAllAvailableBooks();

}
