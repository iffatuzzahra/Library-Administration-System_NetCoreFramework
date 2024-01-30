using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.Bussiness.Interfaces;
using Winterhold.DataAccess.Models;

namespace Winterhold.Bussiness.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private WinterholdContext _dBContext;

    public AuthorRepository(WinterholdContext dBContext)
    {
        _dBContext = dBContext;
    }
    public List<Author> GetAll(string? name, int pageNumber, int pageSize)
    {
        return _dBContext.Authors
            //.Where(a => a.FirstName.Contains(name) || a.LastName.Contains(name) || name == null )
            .Where(a => (a.FirstName + " " + a.LastName).Contains(name) || name == null )
            .Where(a => a.DeleteDate == null)
            .Skip((pageNumber-1) * pageSize).Take(pageSize)
            .ToList();
    }
    public List<Author> GetAll()
    {
        return _dBContext.Authors
            .Where(a => a.DeleteDate == null)
            .ToList();
    }
    public Author GetById(long id)
    {
        _dBContext.Authors.Find(id);
        _dBContext.Authors.SingleOrDefault(c => c.Id == id);
        return _dBContext.Authors
            .Where(a => a.DeleteDate == null)
            .FirstOrDefault(c => c.Id == id)
               ?? throw new NullReferenceException($"Author not found with ID ${id}");
    }
    public void Insert(Author author)
    {
        _dBContext.Authors.Add(author);
        _dBContext.SaveChanges();

    }
    public void Update(Author author)
    {
        _dBContext.Authors.Update(author);
        _dBContext.SaveChanges();
    }
    public void Delete(Author author)
    {
        _dBContext.Authors.Update(author);
        //_dBContext.Authors.Remove(author);
        _dBContext.SaveChanges();
    }
    public int GetTotal(string? name)
    {
        return _dBContext.Authors
            .Where(a => a.DeleteDate == null)
            //.Where(a => a.FirstName.Contains(name) || a.LastName.Contains(name) || name == null)
            .Where(a => (a.FirstName + " " + a.LastName).Contains(name) || name == null)
            .Where(a => a.DeleteDate == null)
            .Count();
    }

}
