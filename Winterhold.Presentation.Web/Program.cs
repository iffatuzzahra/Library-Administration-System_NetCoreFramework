using Winterhold.Bussiness.Interfaces;
using Winterhold.Bussiness.Repositories;
using Winterhold.DataAccess.Dependencies;
using Winterhold.Presentation.Web.Services;

namespace Winterhold.Presentation.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Dependencies.AddDataAccessServices(builder.Services, builder.Configuration);

        builder.Logging.AddConsole();
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<AuthorService>();
        builder.Services.AddScoped<BookService>();
        builder.Services.AddScoped<CategoryService>();
        builder.Services.AddScoped<CustomerService>();
        builder.Services.AddScoped<LoanService>();

        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>(); 
        builder.Services.AddScoped<IBookRepository, BookRepository>(); 
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); 
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>(); 
        builder.Services.AddScoped<ILoanRepository, LoanRepository>(); 

        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}"
            );

        app.Run();
    }
}