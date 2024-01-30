using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.Bussiness.Interfaces;

public interface IAuthorRepository
{
    public List<Author> GetAll(string? name, int pageNumber, int pageSize);
    public List<Author> GetAll();
    public Author GetById(long id);
    public void Insert(Author author);
    public void Update(Author author);
    public void Delete(Author author);
    public int GetTotal(string? name);
}
