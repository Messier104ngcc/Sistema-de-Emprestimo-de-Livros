
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Emprestimo_de_Livros.Data;
using Sistema_de_Emprestimo_de_Livros.Logs_System;
using Sistema_de_Emprestimo_de_Livros.Services.LivrosServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), builder =>
    {
        //builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();
builder.Services.AddScoped<ILivro, Livro>();
builder.Services.AddScoped<ILogs, Logs>();
//builder.Services.AddScoped<IHomeInterface, HomeService>();
//builder.Services.AddScoped<IAutenticacaoInterface, AutenticacaoService>();
//builder.Services.AddScoped<ISessao, Sessao>();
//builder.Services.AddScoped<IEmprestimoInterface, EmprestimoService>();
//builder.Services.AddScoped<IRelatorioInterface, RelatorioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Livros}/{action=Index}/{id?}");

app.Run();