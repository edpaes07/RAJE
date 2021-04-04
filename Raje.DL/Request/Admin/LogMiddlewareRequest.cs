namespace Raje.DL.Request.Admin
{
    public class LogMiddlewareRequest
    {
        /// <summary>
        /// Guarda o valor da chamada da api - exemplo: "/api/v1/Log"
        /// </summary>
        public string Api { get; set; }

        /// <summary>
        /// Valor do body que a é mandado na request
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// QueryString da url da chamada da requisição
        /// </summary>
        public string UrlQuery { get; set; }

        /// <summary>
        /// Método da requisição de log - GET,POST,PUT,DELETE
        /// </summary>
        public string Method { get; set; }
    }
}
