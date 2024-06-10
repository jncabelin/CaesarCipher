using System;
namespace ClientNode.Services
{
	public interface ICaesarCipherDecryptionService
	{
        public Task<string> DecodeCaesarCypher(string encryptedText);
        public Task<Dictionary<string, double>> GetMaxValues(string decryptedText);
    }
}

