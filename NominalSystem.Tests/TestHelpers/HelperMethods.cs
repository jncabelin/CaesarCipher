using System;
namespace NominalSystem.Tests.TestHelpers
{
	public class HelperMethods
	{
		public static string CaesarCipherEncryption(string text, int offset)
		{
			string encryptedString = "";
			foreach(var character in text)
			{
				encryptedString = encryptedString + (char)(character + offset);
			}
			return encryptedString;
		}
	}
}

