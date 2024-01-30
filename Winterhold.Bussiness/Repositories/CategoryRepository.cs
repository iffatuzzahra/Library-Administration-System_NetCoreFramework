using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.Bussiness.Interfaces;
using Winterhold.DataAccess.Models;

namespace Winterhold.Bussiness.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private WinterholdContext _dBContext;

    public CategoryRepository(WinterholdContext dBContext)
    {
        _dBContext = dBContext;
    }
    public List<Category> GetAll(string? name, int pageNumber, int pageSize)
    {
        return _dBContext.Categories
            .Where(c=> c.Name.Contains(name) || name == null)
            .Where(c => c.DeleteDate == null)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToList();
    }
    public int CountAllCategory(string? name)
    {
        return _dBContext.Categories
            .Where(c => c.DeleteDate == null)
            .Where(c => c.Name.Contains(name) || name == null)
            .Count();
    }
    public Category GetCategoryByName(string name)
    {
        _dBContext.Categories.Find(name);
        return _dBContext.Categories
            .Where(b => b.DeleteDate == null)
            .FirstOrDefault(b => b.Name == name);
    }
    public void Insert(Category category)
    {
        _dBContext.Categories.Add(category);
        _dBContext.SaveChanges();

    }
    public void Update(Category category)
    {
        _dBContext.Categories.Update(category);
        _dBContext.SaveChanges();
    }
    public void Delete(Category category)
    {
        _dBContext.Categories.Update(category);
        //_dBContext.Authors.Remove(author);
        _dBContext.SaveChanges();
    }
}
