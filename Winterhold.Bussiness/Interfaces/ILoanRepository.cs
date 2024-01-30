using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.Bussiness.Interfaces;

public interface ILoanRepository
{
    public List<Loan> GetAll(string? title, string? name, bool? isDuePass, int pageNumber, int pageSize);
    public int CountAll(string? title, string? name, bool? isDuePass);
    public Loan GetById(long id);
    public Loan GetLoanDetailById(long id);
    public void Insert(Loan loan);
    public void Update(Loan loan);
    public void Update(Loan loan, string returnBook, string borrowBook);
    public void Delete(Loan loan);

}
