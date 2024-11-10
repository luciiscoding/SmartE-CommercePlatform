using System.Security.Cryptography;

namespace SmartE_commercePlatform.Helpers
{
    public class SecretProvider
    {
        private static SecretProvider instance = null!;
        private static readonly object padlock = new();

        public byte[] Secret { get; } = new byte[32];

        private SecretProvider()
        {
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(Secret);
        }

        public static SecretProvider Instance
        {
            get
            {
                lock (padlock)
                {
                    return instance ??= new SecretProvider();
                }
            }
        }

    }
}
