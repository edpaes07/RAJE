namespace Raje.Infra.Util
{
    public static class HashHelper
    {
        /// <summary>
        /// Calcula MD% Hash de uma determinada string passada como parametro
        /// </summary>
        /// <param name="chave">String contendo a chave que deve ser criptografada para MD5 Hash</param>
        /// <returns>string com 32 caracteres com a chave criptografada</returns>
        public static string CalculaHashMD5(string chave)
        {
            if (string.IsNullOrWhiteSpace(chave))
                return chave;

            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(chave);
                byte[] hash = md5.ComputeHash(inputBytes);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString(); // Retorna chave criptografada
            }
            catch
            {
                return chave; // Caso encontre erro retorna a chave
            }
        }
    }
}