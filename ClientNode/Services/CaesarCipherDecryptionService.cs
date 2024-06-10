using System;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientNode
{
	public class CaesarCipherDecryptionService
	{
        public static async Task<string> DecodeCaesarCypher(string encryptedText)
        {
            Console.WriteLine("Decoding...");
            await Task.CompletedTask;
            // Find Comma 
            var offset = await FindOffset(encryptedText);
            var decryptedText = "";
            foreach (var character in encryptedText)
            {
                decryptedText = decryptedText + (char)(character - offset);
            }
            return decryptedText;
        }

        public static async Task<Dictionary<string, double>> GetMaxValues(string decryptedText)
        {
            await Task.CompletedTask;
            var table = ConvertCsvToDataTable(decryptedText);
            var jsonDict = new Dictionary<string, double>();
            foreach (var columnName in table.Columns.Cast<DataColumn>()
                         .Select(x => x.ColumnName)
                         .ToArray())
            {
                double parsedValue;
                double max = table.AsEnumerable()
                    .Max(row => double.Parse(row[columnName].ToString()));
                jsonDict.Add(columnName, max);
            }
            return jsonDict;
        }

        private static async Task<int> FindOffset(string encrypedText)
        {
            await Task.CompletedTask;
            string validRowValues = ",.0123456789";
            // Get the Last Character
            // Assumption: Last Character is a number
            var textLen = encrypedText.Length;
            var lastChar = encrypedText[textLen - 2];
            // Find the Range
            var maxOffset = lastChar - '0';
            var minOffset = lastChar - '9';
            for (int offset = minOffset; offset <= maxOffset; offset++)
            {
                string tryParse = "";
                var index = 2;
                var invalidOffset = false;
                while (index < textLen)
                {
                    char test = (char)(encrypedText[textLen - index] - offset);
                    if (validRowValues.Contains(test))
                    {
                        tryParse = tryParse + test;
                        index++;
                    }
                    else if (test == '\n')
                    {
                        var distinct = string.Concat(tryParse.Distinct().OrderBy(c => c));

                        if (String.Compare(distinct, validRowValues) == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        invalidOffset = true;
                        break;
                    }
                }

                if (!invalidOffset)
                {
                    return offset;
                }
            }
            return 0;
        }

        private static DataTable ConvertCsvToDataTable(string decryptedText)
        {
            // Separate Text to new Lines
            var rows = decryptedText.Split('\n');
            DataTable dtData = new DataTable();
            string[] rowValues = null;
            DataRow dr = dtData.NewRow();

            //Creating columns
            if (rows.Length > 0)
            {
                foreach (string columnName in rows[0].Split(','))
                    dtData.Columns.Add(columnName);
            }

            //Creating row for each line.(except the first line, which contain column names)
            for (int row = 1; row < rows.Length - 1; row++)
            {
                rowValues = rows[row].Split(',');
                dr = dtData.NewRow();
                dr.ItemArray = rowValues;
                dtData.Rows.Add(dr);
            }

            return dtData;
        }
    }
}

