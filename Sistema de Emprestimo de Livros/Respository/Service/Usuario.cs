using Microsoft.EntityFrameworkCore;
using Sistema_de_Emprestimo_de_Livros.Data;
using Sistema_de_Emprestimo_de_Livros.Models;
using Sistema_de_Emprestimo_de_Livros.Respository.Interface;

namespace Sistema_de_Emprestimo_de_Livros.Respository.Usuario
{
    public class Usuario : IUsuario
    {
        private readonly AppDbContext _context;

        public Usuario(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsuariosModel>> BuscarUsuarios(int? id)
        {
            var registro = new List<UsuariosModel>();

            if(id != null)
            {
                registro = await _context.Usuarios.Where(cliente => cliente.Cargo == 0).Include(e => e.Endereco).ToListAsync();
            }
            else
            {
                registro = await _context.Usuarios.Where(cliente => cliente.Cargo != 0).Include(e => e.Endereco).ToListAsync();
            }

            return registro;
        }
    }
}
