using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.DataAccess.Dependencies;

public static  class Dependencies
{
    public static void AddDataAccessServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WinterholdContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("WinterholdConnection"))
        );
    }
}
