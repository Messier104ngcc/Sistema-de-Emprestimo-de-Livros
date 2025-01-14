namespace Sistema_de_Emprestimo_de_Livros.Logs_System
{
    public interface ILogs
    {
        void RegistrarLogDeExcecao(Exception ex);
    }
}
