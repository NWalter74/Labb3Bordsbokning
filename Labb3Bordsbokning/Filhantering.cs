using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Text.RegularExpressions;

namespace Labb3Bordsbokning
{
    public class Filhantering
    {
        public void SaveBokingsToFile(string outputString)
        {
            if (File.Exists("FilMedAllaBokningar.txt"))
            {
                using (StreamReader sr = File.OpenText("FilMedAllaBokningar.txt"))
                {
                    string[] lines = File.ReadAllLines("FilMedAllaBokningar.txt");
                    bool isMatch = false;
                    for (int x = 0; x < lines.Length - 1; x++)
                    {
                        if (outputString == lines[x])
                        {
                            sr.Close();
                            isMatch = true;
                        }
                    }
                    if (!isMatch)
                    {
                        sr.Close();
                        File.AppendAllText("FilMedAllaBokningar.txt", outputString + "\n");
                        isMatch = false;
                    }
            }
        }
            else
            {
                File.AppendAllText("FilMedAllaBokningar.txt", outputString + "\n");
            }
        }

        public void DeleteBokingFromFile(string deleteString)
        {
            List<string> lines = File.ReadAllLines("FilMedAllaBokningar.txt").ToList();

            lines.Remove(deleteString);

            File.WriteAllLines("FilMedAllaBokningar.txt", lines);            
        }

        public string[] ReadAllBokingsFromFile()
        {
            string[] lines = File.ReadAllLines("FilMedAllaBokningar.txt");
            return lines;
        }
    }
}
