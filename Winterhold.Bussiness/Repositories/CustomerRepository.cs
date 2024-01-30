using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.Bussiness.Interfaces;
using Winterhold.DataAccess.Models;

namespace Winterhold.Bussiness.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private WinterholdContext _dBContext;

    public CustomerRepository(WinterholdContext dBContext)
    {
        _dBContext = dBContext;
    }

    public List<Customer> GetAll(string? number, string? name, bool? isExpired, int pageNumber, int pageSize)
    {
        return _dBContext.Customers
            .Where(c => c.DeleteDate == null)
            .Where(c => c.MembershipNumber.Contains(number) || number == null)
            //.Where(c => name == null || c.FirstName.Contains(name) || c.LastName.Contains(name))
            .Where(a => (a.FirstName + " " + a.LastName).Contains(name) || name == null)
            .Where(c => (isExpired == null 
                        ||(isExpired.Value && c.MembershipExpireDate < DateTime.Now )
                        || (!isExpired.Value && c.MembershipExpireDate >= DateTime.Now) 
                        ))
            .Skip((pageNumber-1) * pageSize).Take(pageSize)
            .ToList();
    }
    public int countCustomers(string? number, string? name, bool? isExpired)
    {
        return _dBContext.Customers
            .Where(c => c.DeleteDate == null)
            .Where(c => c.MembershipNumber.Contains(number) || number == null)
            //.Where(c => name == null || c.FirstName.Contains(name) || c.LastName.Contains(name))
            .Where(a => (a.FirstName + " " + a.LastName).Contains(name) || name == null)
            .Where(c => (isExpired == null
                        || (isExpired.Value && c.MembershipExpireDate < DateTime.Now)
                        || (!isExpired.Value && c.MembershipExpireDate >= DateTime.Now)
                        ))
            .Count();
    }
    public Customer GetById(string number)
    {
        _dBContext.Customers.Find(number);
        return _dBContext.Customers
            .Where(b => b.DeleteDate == null)
            .FirstOrDefault(b => b.MembershipNumber == number)
               ?? throw new NullReferenceException($"Customer not found with Number ${number}");
    }
    public void Insert(Customer customer)
    {
        _dBContext.Customers.Add(customer);
        _dBContext.SaveChanges();
    }
    public void Update(Customer customer)
    {
        _dBContext.Customers.Update(customer);
        _dBContext.SaveChanges();
    }
    public void Delete(Customer customer)
    {
        _dBContext.Customers.Update(customer);
        //_dBContext.Authors.Remove(author);
        _dBContext.SaveChanges();
    }
    public List<Customer> GetNonExpiredCustomers()
    {
        return _dBContext.Customers
            .Where(c => c.DeleteDate == null)
            .Where(c => c.MembershipExpireDate >= DateTime.Now)
            .ToList();
    }
}
