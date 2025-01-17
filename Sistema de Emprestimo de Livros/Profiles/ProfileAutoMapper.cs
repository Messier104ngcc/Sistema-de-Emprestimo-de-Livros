using AutoMapper;
using Sistema_de_Emprestimo_de_Livros.Dto.Livro;
using Sistema_de_Emprestimo_de_Livros.Models;

namespace Sistema_de_Emprestimo_de_Livros.Profiles
{
	public class ProfileAutoMapper : Profile
	{

		public ProfileAutoMapper()
		{
			CreateMap<LivroCriacaoDto, LivrosModel>();
		}
	}
}
