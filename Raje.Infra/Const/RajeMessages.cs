using System.Runtime.Serialization;

namespace Raje.Infra.Const
{
    public static class RajeMessages
    {
    }

    public static class RajeValidationMessages
    {
        public static readonly string CompanyAlreadyExist = "Empresa já cadastrada";

        public static readonly string CompanyNameRequired = "O nome da empresa é obrigatório.";

        public static readonly string CompanyName = "O nome da empresa não pode ser maior que 200.";

        public static readonly string FileName = "O nome do arquivo é obrigatório.";

        public static readonly string FilePath = "O FilePath é obrigatório.";

        public static readonly string Author = "Autor da ação é obrigatório. Entidade com auditoria configurada.";
    }

}
