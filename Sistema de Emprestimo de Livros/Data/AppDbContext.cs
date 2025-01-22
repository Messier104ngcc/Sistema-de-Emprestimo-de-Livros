using Microsoft.EntityFrameworkCore;
using Sistema_de_Emprestimo_de_Livros.Models;

namespace Sistema_de_Emprestimo_de_Livros.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UsuariosModel> Usuarios { get; set; }
        //public DbSet<ClienteModel> Clientes { get; set; }
        //public DbSet<EnderecoModel> Enderecos { get; set; }
        public DbSet<LivrosModel> Livros { get; set; }

        //public DbSet<EmprestimoModel> Emprestimos { get; set; }

    }
}
