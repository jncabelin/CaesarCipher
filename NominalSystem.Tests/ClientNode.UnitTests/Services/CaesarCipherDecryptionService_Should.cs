using System.ComponentModel;
using ClientNode;
using NominalSystem.Tests.TestHelpers;

namespace NominalSystem.Tests.ClientNode.UnitTests.Services
{
    public class CaesarCipherDecryptionService_Should
    {
        [Fact]
        [DisplayName("Decrypt Column Value")]
        public async void Decrypt_Column_Value()
        {
            // Arrange
            string value = "\n1234567890";
            string encrypted = HelperMethods.CaesarCipherEncryption(value, 15);
            var service = new CaesarCipherDecryptionService();

            // Act
            var result = await service.DecodeCaesarCipher(encrypted);

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        [DisplayName("Decrypt Multiple Lines")]
        public async void Decrypt_Multiple_Lines()
        {
            // Arrange
            string value = "\n12.3,4.5,6.7\n9.0,2.000,3.880";
            string encrypted = HelperMethods.CaesarCipherEncryption(value, 15);
            var service = new CaesarCipherDecryptionService();

            // Act
            var result = await service.DecodeCaesarCipher(encrypted);

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        [DisplayName("Decrypt Headers")]
        public async void Decrypt_Headers()
        {
            // Arrange
            string value = "ABCDEF, hijkLMNo, pqrSTUv, wXYz300\n12,3.45,67.8,91.2000";
            string encrypted = HelperMethods.CaesarCipherEncryption(value, 15);
            var service = new CaesarCipherDecryptionService();

            // Act
            var result = await service.DecodeCaesarCipher(encrypted);

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        [DisplayName("Find Max Value")]
        public async void Find_Max_Value()
        {
            // Arrange
            string value = "ColA,ColB\n1.2,6.9\n1000,5678";
            var expected = new Dictionary<string, double> { { "ColA", 1000},{"ColB", 5678} };
            var service = new CaesarCipherDecryptionService();

            // Act
            var result = await service.GetMaxValues(value);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}

