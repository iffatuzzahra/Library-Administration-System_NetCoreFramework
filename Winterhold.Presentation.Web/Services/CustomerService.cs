 
using System.Net;
using System.Numerics;
using System.Reflection;
using Winterhold.Bussiness.Interfaces;
using Winterhold.Bussiness.Repositories;
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public CustomerIndexViewModel GetAllCustomer(string? number, string? name, bool? isExpired, int pageNumber, int pageSize)
    {
        var customers = _customerRepository.GetAll(number, name, isExpired, pageNumber, pageSize)
            .Select(c => new CustomerIndexTableViewModel
            {
                MembershipNumber = c.MembershipNumber, 
                FirstName = c.FirstName,
                LastName = c.LastName,
                MembershipExpireDate = c.MembershipExpireDate
            }).ToList();
        return new CustomerIndexViewModel
        {
            Number = number,
            Name = name,
            Customers = customers,
            PaginationInfo = new PaginationInfoViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = _customerRepository.countCustomers(number, name, isExpired)
            }
        };
    }
    public CustomerUpsertViewModel GetCustomerUpsertVmByNumber(string number)
    {
        var customer = _customerRepository.GetById(number);
        return new CustomerUpsertViewModel
        {
            MembershipNumber = number,
            FirstName = customer.FirstName,
            LastName= customer.LastName,
            BirthDate  = customer.BirthDate,
            Gender = customer.Gender,
            Phone = customer.Phone,
            Address = customer.Address
        };
    }
    public void ExtendMembershipDate(string number)
    {
        var customer = _customerRepository.GetById(number);
        customer.MembershipExpireDate = DateTime.Now.AddYears(2);
        _customerRepository.Update(customer);
    }
    public void InsertCustomer(CustomerUpsertViewModel vm)
    {
        var customer = new Customer
        {
            MembershipNumber = vm.MembershipNumber, 
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            BirthDate = vm.BirthDate,
            Gender = vm.Gender,
            Phone = vm.Phone,
            Address = vm.Address, 
            MembershipExpireDate = DateTime.Now.AddYears(2)
        };
        _customerRepository.Insert(customer);
    }
    public void UpdateCustomer(CustomerUpsertViewModel vm)
    {
        var customer = _customerRepository.GetById(vm.MembershipNumber);
        customer.FirstName = vm.FirstName;
        customer.LastName = vm.LastName;
        customer.BirthDate = vm.BirthDate;
        customer.Gender = vm.Gender;
        customer.Phone = vm.Phone;
        customer.Address = vm.Address;

        _customerRepository.Update(customer);
    }
    public void DeleteCustomer(string number)
    {
        var customer = _customerRepository.GetById(number);
        customer.DeleteDate = DateTime.Now;
        _customerRepository.Delete(customer);
    }
    public CustomerPopupViewModel GetCustomerPopupData(string number)
    {
        var customer = _customerRepository.GetById(number);
        return new CustomerPopupViewModel
        {
            MembershipNumber = number,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            BirthDate = customer.BirthDate,
            Gender = customer.Gender,
            Phone = customer.Phone,
            Address = customer.Address
        }; 
    }
}
