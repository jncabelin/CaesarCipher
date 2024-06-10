using System;
namespace MasterNodeAPI.Services
{
	public interface ICaesarCipherService
	{
        public Task<string> ReadEncryptedFile(string filePath);
        public Task<string> SendFileContent(string encryptedText);
    }
}

