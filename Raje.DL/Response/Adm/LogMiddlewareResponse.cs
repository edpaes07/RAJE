namespace Raje.DL.Response.Adm
{
    public class LogMiddlewareResponse
    {
        /// <summary>
        /// Resposta middleware log - status code
        /// </summary>
        public short Code { get; set; }

        /// <summary>
        /// Resposta middleware log - body da resposta
        /// </summary>
        public string Response { get; set; }

    }
}
