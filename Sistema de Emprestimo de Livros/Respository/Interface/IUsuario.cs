using Sistema_de_Emprestimo_de_Livros.Models;

namespace Sistema_de_Emprestimo_de_Livros.Respository.Interface
{
    public interface IUsuario
    {
        Task<List<UsuariosModel>> BuscarUsuarios(int? id);
    }
}
