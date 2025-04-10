
using System.Text;
using System.Security.Cryptography;

namespace SocialNetwork.Persistance.HerpersRepository.dbo.usuarios
{
    // NOT USING
    public class PasswordEncryption
    {
        public static string Compute256Hash(string contraseña)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));

                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++) 
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
    }
}
