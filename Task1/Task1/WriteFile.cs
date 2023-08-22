using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public static class FilesWork
    {
        static string directoryPath = @"..\..\..\files\";
        public static void CreateFiles()
        {
            for (int i = 1; i <= 100; i++)
            {
                StringBuilder sb = new StringBuilder();
                string filePath = directoryPath + "file" + i+".txt";
                for (int j = 0; j < 100000; j++)
                {
                    sb.Append(LogicString.CreateFinalString());
                }
                byte[] buffer = Encoding.Default.GetBytes(sb.ToString());
                WriteFile(filePath, buffer);
                Console.WriteLine("файл записан приступаем к следующему");
            }
        }

        public static int CombineFilesWithDeleted(string deletedString)
        {
            int counter=0;
            string combineFilePath = directoryPath + "CombineFile.txt";
            using (FileStream CombineFS = new FileStream(combineFilePath, FileMode.Create))
            {
                for (int i = 1; i <= 100; i++)
                {
                    string filePath = directoryPath + "file" + i + ".txt";
                    string textFromFile = ReadFile(filePath);
                    string[] textInStrings = textFromFile.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < textInStrings.Length; j++)
                    {
                        if (!textInStrings[j].Contains(deletedString))
                        {
                            sb.Append(textInStrings[j] + "\n");
                        }
                        else counter++;
                    }
                    byte[] buffer = Encoding.Default.GetBytes(sb.ToString());
                    WriteFile(filePath, buffer);
                    CombineFS.Write(buffer, 0, buffer.Length);
                }
            }
            return counter;
        }
        
        //

        private static string ReadFile(string filePath)
        {
            using (FileStream fs = new FileStream(@filePath, FileMode.Open))
            {
                byte[] buffer = new byte[fs.Length];
                // считываем данные
                fs.Read(buffer, 0, buffer.Length);

                return Encoding.Default.GetString(buffer);
            }
            return "file not exist";
        }
        private static void WriteFile(string filePath, byte[] buffer)
        {
            using (FileStream fs = new FileStream(@filePath, FileMode.Create))
            {
                // пишем данные
                fs.Write(buffer, 0, buffer.Length);
            }
        }
    }
}

