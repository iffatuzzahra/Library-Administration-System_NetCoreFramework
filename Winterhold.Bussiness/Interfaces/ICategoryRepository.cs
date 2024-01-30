using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.Bussiness.Interfaces;

public interface ICategoryRepository
{
    public List<Category> GetAll(string? name, int pageNumber, int pageSize);
    public Category GetCategoryByName(string name);
    public int CountAllCategory(string? name);
    public void Insert(Category category);
    public void Update(Category category);
    public void Delete(Category category);

}
