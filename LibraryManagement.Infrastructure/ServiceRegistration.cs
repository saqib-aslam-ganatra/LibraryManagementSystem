using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Identity;
using LibraryManagement.Infrastructure.Services;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Application.Common.Interfaces;

namespace LibraryManagement.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Database Context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register interface for ApplicationDbContext
            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());

            //Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Domain Repositories
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            // Application Services
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IBookRepository, BookRepository>();

            // Utility Services needed by ApplicationDbContext
            services.AddScoped<DateTimeService>();
            services.AddScoped<JwtTokenService>();


            return services;
        }
    }
}
