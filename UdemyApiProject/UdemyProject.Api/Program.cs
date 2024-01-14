using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using UdemyProject.Application;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;
using UdemyProject.Infrastructure;
using UdemyProject.Infrastructure.Helpers;
using UdemyProject.Infrastructure.Seeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddInfrastructureRegistration(builder.Configuration).AddApplicationServices();

var app = builder.Build();
app.UseStaticFiles();
using (var Scope = app.Services.CreateScope())
{
    var UserManger = Scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var RoleManger = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var CategoryRepository = Scope.ServiceProvider.GetRequiredService<ICourseCategoryRepository>();
    var LangugeRepository = Scope.ServiceProvider.GetRequiredService<ICourseLangugeRepository>();

    await new SeedAdminWithRolesinitialData(RoleManger, UserManger).SeedData();

    await new SeedCategoriesInitialData(CategoryRepository).SeedCategories();
    await new SeedLanguge(LangugeRepository).seedLanguge();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();