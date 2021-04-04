namespace Raje.DL.Services.DAL.Model
{
    public enum EnumEntityState
    {
        /// <summary>Operação de inclusão no banco de dados.</summary>
        Inserir = 1,

        /// <summary>Operação de atualização no banco de dados.</summary>
        Atualizar = 2,

        /// <summary>Operação de exclusão lógica no banco de dados.</summary>
        ExcluirLogico = 3,

        /// <summary>Operação de exclusão física no banco de dados.</summary>
        ExcluirFisico = 4,

        /// <summary>Operação de consulta no banco de dados.</summary>
        Consultar = 5
    }
}
