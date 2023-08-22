using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Entities;

namespace Task1
{
    public static class ImportFileToDB
    {
        public static void Import(string fileName)
        {
            string filePath = @"..\..\..\files\"+fileName;
            using (Task1DbContext db = new Task1DbContext())
            {
                int totalRows = GetTotalRows(filePath);
                int importedRows = 0;
                while (importedRows < totalRows)
                {
                    var rows = ReadRows(filePath, importedRows, 1000);

                    // Добавление строк в БД
                    db.TaskTables.AddRange(rows.Select(x=>new TaskTable
                    {
                        Date=x.Date,
                        LatinSymbols = x.LatinSymbols,
                        RusSymbols = x.RusSymbols,
                        Number = x.Number,
                        Float = x.Float,
                    }));

                    // Сохранение изменений
                    db.SaveChanges();

                    importedRows += rows.Count();

                    // Вывод хода импорта
                    Console.WriteLine($"Imported {importedRows} of {totalRows} rows");
                }
            }
        }

        private static int GetTotalRows(string fileName)
        {
            int totalRows = 0;

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    totalRows++;
                }
            }

            return totalRows;
        }

        // Метод чтения порции строк из файла
        private static IEnumerable<TaskTable> ReadRows(string fileName, int fromRow, int count)
        {
            List<TaskTable> rows = new List<TaskTable>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                for (int i = 0; i < fromRow && !sr.EndOfStream; i++)
                    sr.ReadLine();

                for (int i = 0; i < count && !sr.EndOfStream; i++)
                {
                    string line = sr.ReadLine();
                    // парсинг строки в объект Post
                    rows.Add(ParseLineToPost(line));
                }
            }

            return rows;
        }

        // Метод парсинга строки в объект Post
        private static TaskTable ParseLineToPost(string line)
        {
            // Разделение строки и создание объекта
            var values = line.Split("||");

            return new TaskTable
            {
                Date = DateOnly.Parse(values[0]),
                LatinSymbols = values[1],
                RusSymbols = values[2],
                Number = Int32.Parse(values[3]),
                Float = Decimal.Parse(values[4])
            };
        }
    }
}
