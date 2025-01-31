using Application;
using FluentValidation;
using FluentValidation.AspNetCore;
using Application.Interfaces.OpenAiInterface; 
using Infrastructure;
using Infrastructure.Databases;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using MyCvSite.Controllers;
using MyCvSite.Validators;

namespace MyCvSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Configuration.AddUserSecrets<Program>();
            builder.Services.AddControllers();
            builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<JobbApplicationViewModelValidator>();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Database>();
            builder.Services.AddRazorPages();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddTransient<JobbApplicationController>();


         
            builder.Services.AddHttpClient<IOpenAiService, OpenAiService>();
          
            builder.Services.AddTransient<CVController>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();

            app.Run();
        }
    }
}
