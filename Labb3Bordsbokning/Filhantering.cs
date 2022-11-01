using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;

namespace Labb3Bordsbokning
{
    public class Filhantering
    {
        List<string> sortedOutputStringList = new List<string>();

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
                        sortedOutputStringList.Add(outputString);
                        sortedOutputStringList.Sort();

                        foreach(string s in sortedOutputStringList)
                        {
                            File.AppendAllText("FilMedAllaBokningar.txt", s + "\n");
                        }
                        
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
            if (File.Exists("FilMedAllaBokningar.txt"))
            {
                using (StreamReader sr = File.OpenText("FilMedAllaBokningar.txt"))
                {
                    string[] lines = File.ReadAllLines("FilMedAllaBokningar.txt");
                    bool isMatch = false;
                    for (int x = 0; x <= lines.Length - 1; x++)
                    {
                        if (deleteString == lines[x])
                        {
                            sr.Close();
                            isMatch = true;
                        }
                        else
                        {
                            File.AppendAllText("tmp.txt", lines[x] + "\n");
                        }
                    }

                    string[] tmpFile = File.ReadAllLines("tmp.txt");

                    File.Delete("FilMedAllaBokningar.txt");

                    foreach (string line in tmpFile)
                    {
                        File.AppendAllText("FilMedAllaBokningar.txt", line + "\n");
                    }

                    File.Delete("tmp.txt");
                }
            }
            else
            {
                MessageBox.Show("OBS! Något gick fel. Filen verkar inte finnas eller används av ett annat program!", "OBS!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
