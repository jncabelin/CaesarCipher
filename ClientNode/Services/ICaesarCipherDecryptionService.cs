using System;
namespace ClientNode.Services
{
	public interface ICaesarCipherDecryptionService
	{
        public Task<string> DecodeCaesarCipher(string encryptedText);
        public Task<Dictionary<string, double>> GetMaxValues(string decryptedText);
        public Task<int> FindOffset(string encrypedText);
    }
}

