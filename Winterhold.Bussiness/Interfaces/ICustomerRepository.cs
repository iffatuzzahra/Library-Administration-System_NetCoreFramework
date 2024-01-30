using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.Bussiness.Interfaces;

public interface ICustomerRepository
{
    public List<Customer> GetAll(string? number, string? name, bool? isExpired, int pageNumber, int pageSize);
    public int countCustomers(string? number, string? name, bool? isExpired);
    public Customer GetById(string number);
    public void Insert(Customer customer);
    public void Update(Customer customer);
    public void Delete(Customer customer);
    public List<Customer> GetNonExpiredCustomers();

}
