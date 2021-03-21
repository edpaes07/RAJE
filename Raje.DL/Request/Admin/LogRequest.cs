using Raje.DL.Request.Admin.Base;

namespace Raje.DL.Request.Admin
{
    public class LogRequest : BaseRequest
    {
        /// <summary>
        /// Valor da requisição como true quando o Raje chama as requisições
        /// </summary>
        public bool Input { get; set; }

        /// <summary>
        /// StatusCode da requisição 200,201,202..
        /// </summary>
        public short Code { get; set; }

        /// <summary>
        ///  Guarda o valor da chamada da api - exemplo: "/api/v1/Log"
        /// </summary>
        public string Api { get; set; }

        /// <summary>
        /// Body da requisição de log
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// Response da requisição de log
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// QueryString da requisição
        /// </summary>
        public string UrlQuery { get; set; }

        /// <summary>
        /// Método da requisição de log - GET,POST,PUT,DELETE
        /// </summary>
        public string Method { get; set; }
    }
}
