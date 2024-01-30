 
using System.ComponentModel.DataAnnotations;
using Winterhold.Bussiness.Interfaces;
using Winterhold.Bussiness.Repositories;
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.Validations;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBookRepository _bookRepository;

    public CategoryService(ICategoryRepository categoryRepository, IBookRepository bookRepository)
    {
        _categoryRepository = categoryRepository;
        _bookRepository = bookRepository;
    }
    public CategoryIndexViewModel GetAllCategory(string? name, int pageNumber, int pageSize)
    {
        var categories = _categoryRepository.GetAll(name, pageNumber, pageSize)
            .Select(c =>
            new CategoryIndexTableViewModel {
                Name = c.Name,
                Floor = c.Floor,
                Isle = c.Isle,
                Bay = c.Bay,
                TotalBooks = _bookRepository.CountBooksByCategoryName(c.Name)
            }).ToList();

        return new CategoryIndexViewModel
        {
            Name = name,
            CategoriesSummary = categories,
            PaginationInfo = new PaginationInfoViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = _categoryRepository.CountAllCategory(name)
            }
        };
    }
    public CategoryUpsertViewModel GetCategoryByName(string name)
    {
        var category = _categoryRepository.GetCategoryByName(name);
        return new CategoryUpsertViewModel
        {
            Name = category.Name,
            Floor = category.Floor,
            Isle = category.Isle,
            Bay = category.Bay
        };
    }
    public void InsertCategory(CategoryUpsertViewModel vm)
    {
        if(_categoryRepository.GetCategoryByName(vm.Name) != null)
        {
            throw new Exception("CategoryName Shoud unique");
        }
        var category = new Category
        {
            Name = vm.Name,
            Floor = vm.Floor,
            Isle = vm.Isle,
            Bay = vm.Bay
        };
        _categoryRepository.Insert(category);
    }
    public void UpdateCategory(CategoryUpsertViewModel vm)
    {
        var category = _categoryRepository.GetCategoryByName(vm.Name);
        category.Floor = vm.Floor;
        category.Isle = vm.Isle;
        category.Bay = vm.Bay;

        _categoryRepository.Update(category);
    }
    public void DeleteCategory(string name)
    {
        var category = _categoryRepository.GetCategoryByName(name); 
        category.DeleteDate = DateTime.Now;
        _categoryRepository.Delete(category);
    }
}
