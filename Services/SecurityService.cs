using System.Security.Cryptography;
using System.Text;

namespace BlazorServerTestDynamicAccess.Services
{
    public class SecurityService : ISecurityService
    {
        public string GetSha256Hash(string input)
        {
            using (var hashAlgorithm = SHA256.Create())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }
    }
}
