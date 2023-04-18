namespace WebLuto.Utils.Messages
{
    public static class ClientMsg 
    {
        #region EXCEPTIONS (EXC0000)

        public const string EXC0001 = "Não foi possível encontrar nenhum cliente!";
        public const string EXC0002 = "Já existe um cliente com o email: {0}";
        public const string EXC0003 = "Erro ao buscar todos os clientes! - {0}";
        public const string EXC0004 = "Erro ao buscar o cliente: {0} - {1}";
        public const string EXC0005 = "Erro ao criar conta! - {0}";
        public const string EXC0006 = "Erro ao atualizar conta! - {0}";
        public const string EXC0007 = "Erro ao excluir conta! - {0}";
        public const string EXC0008 = "Erro ao confirmar conta! - {0}";

        #endregion

        #region INFORMATIONS (INF0000)

        public const string INF0001 = "Conta criado com sucesso! Realize a confirmação do email!";
        public const string INF0002 = "Conta atualizada com sucesso!";
        public const string INF0003 = "Conta excluida com sucesso!";

        #endregion
    }
}
