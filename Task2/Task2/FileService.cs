using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Task2.Entities;

namespace Task2
{
    //статический класс для работы с экселем и работы с данными(парсинг, преобразование)
    public static class FileService
    {
        public static void ReadExcel(string path)
        {   
            // Чтение данных из ячейки

            using (var context = new Task1DbContext())
            {
                context.Balances.AddRange(ParseExcel(GetSheetFromPath(path)));
                // Сохранение изменений
                context.SaveChanges();
            }
        }
        private static Balance ParseLineToBalance(int RowNum, ISheet sheet)
        {
            int AccountNumber;
            Int32.TryParse(sheet.GetRow(RowNum).GetCell(0).StringCellValue, out AccountNumber);
            // Разделение(парсинг) строки и создание объекта
            return new Balance
            {
                AccountNumber = AccountNumber,
                IbAssets = Convert.ToDecimal(sheet.GetRow(RowNum).GetCell(1).NumericCellValue),
                IbPassive = Convert.ToDecimal(sheet.GetRow(RowNum).GetCell(2).NumericCellValue),
                Debit = Convert.ToDecimal(sheet.GetRow(RowNum).GetCell(3).NumericCellValue),
                Credit = Convert.ToDecimal(sheet.GetRow(RowNum).GetCell(4).NumericCellValue)
            };
        }
        
        //метод для проходки по файлу
        private static List<Balance> ParseExcel(ISheet sheet)
        {
            List<Balance> balances = new List<Balance>();
            for (int RowNum = 9; RowNum < sheet.LastRowNum - 2; RowNum++)
            {
                //проверка и получение строк(были проблемы с типом счетов в excel файле) с помощью регулярнгого выражения
                if (sheet.GetRow(RowNum).GetCell(0).CellType == CellType.String && Regex.IsMatch(sheet.GetRow(RowNum).GetCell(0).StringCellValue, @"[0-9]*"))
                {
                    balances.Add(ParseLineToBalance(RowNum, sheet));
                }
            }
            return balances;
        }

        //запись спика загруженых файлов
        public static void AddFile(string path)
        {
            using (FileStream fs = new FileStream(@"..\..\..\LoadedFiles.txt", FileMode.OpenOrCreate))
            {
                byte[] buff = Encoding.Default.GetBytes(path+"||");
                fs.Write(buff, 0, buff.Length);
            }
        }

        //подгрузка списка загруженых файлов из файла
        public static void LoadListOfFile(ObservableCollection<string> files)
        {
            string filesString;
            using(FileStream fs = new FileStream(@"..\..\..\LoadedFiles.txt",FileMode.Open))
            {
                var buff = new byte[fs.Length];
                fs.Read(buff, 0, buff.Length);
                filesString = Encoding.Default.GetString(buff);
            }
            var filesArr = filesString.Split("||\n");
            foreach (string file in filesArr)
            {
                files.Add(file);
            }
        }

        //получение промежуточного файла и чтение сз СУБД, соотносим с выбраным экселем 
        public static void FindInfoInDB(string path, ObservableCollection<ModifyBalance> modifyBalances)
        {
            var listFromDB = new List<Balance>();
            var listFromExcel = new List<Balance>();
            var TempList = new List<ModifyBalance>();
            //запрос к бд с помощью linq и entity framework
            using (Task1DbContext db = new Task1DbContext())
            {
                var queryFromDB = db.Balances.Select(p => p);
                listFromExcel = ParseExcel(GetSheetFromPath(path));

                foreach (var itemFromDB in queryFromDB)
                {
                    foreach (var itemFromExcel in listFromExcel)
                    {
                        if (itemFromDB.AccountNumber == itemFromExcel.AccountNumber && itemFromExcel.AccountNumber!=0)
                        {
                            TempList.Add(new ModifyBalance(itemFromDB));
                        }
                    }
                }
            }

            GroupAndAdd(TempList, modifyBalances);
        }

        //получение листа c помощью библиотеки NPOI
        private static ISheet GetSheetFromPath(string path)
        {
            IWorkbook workbook;
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(fileStream);
            }
            // Получение листа
            return workbook.GetSheetAt(0);
        }

        //группировка для двоичных номеров счета и создание финального списка который и будет отображаться
        private static void GroupAndAdd(List<ModifyBalance> TempList, ObservableCollection<ModifyBalance> modifyBalances)
        {
            //группировка счетов по двум первым символам
             var query = from b in TempList
                         group b by b.AccountNumber.ToString().Substring(0, 2) into g
                         select new
                         {
                            GroupNumber = g.Key,
                            Balances = g       
                         };

            var queryArr = query.ToArray();
            var tempArr = TempList.ToArray();
            int j = 0;

            //вставка в правильное место и подведеия итогов по группам
            for(int i =0; i<tempArr.Length; i++)
            {
                if (Int32.Parse(queryArr[j].GroupNumber) < Int32.Parse(tempArr[i].AccountNumber.ToString().Substring(0,2)))
                {
                    modifyBalances.Add(new ModifyBalance(queryArr[j].GroupNumber, queryArr[j].Balances));
                    j++;
                }
                modifyBalances.Add(tempArr[i]);
            }
        }
    }

}
