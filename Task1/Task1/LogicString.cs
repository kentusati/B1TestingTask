using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public static class LogicString
    {
        static string latinSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUWXYZ";
        static string russianSymbols = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяФБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private static string GetRandomDateLast5Years()
        {
            Random random = new Random();

            // Начальная дата - 5 лет назад
            DateTime start = DateTime.Now.AddYears(-5);

            // Конечная дата - сегодня
            DateTime end = DateTime.Now;

            // Вычисляем количество дней между началом и концом периода
            int days = (end - start).Days;

            // Генерируем случайное число дней 
            int randomDays = random.Next(days);

            // Возвращаем случайную дату
            return start.AddDays(randomDays).ToShortDateString();
        }

        private static string GetRandomSymbols(string Symbols)
        {

            // Создадим строку для результата
            string result = String.Empty;

            // Сгенерируем 10 случайных символов
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                // Получим случайный индекс в строке символов
                int index = random.Next(Symbols.Length);

                // Добавим символ по индексу в результат
                result += Symbols[index];
            }

            return result;
        }

        private static string GetRandomEvenNumber()
        {
            Random random = new Random();

            // Генерируем случайное число 
            int number = random.Next(1, 100000001);

            // Убеждаемся, что число чётное
            if (number % 2 != 0)
            {
                number += 1;
            }

            return number.ToString();
        }

        private static string GetRandomNumberWith8DecimalPlaces()
        {
            Random random = new Random();

            double min = 1;
            double max = 20;

            double number = random.NextDouble() * (max - min) + min;

            return Math.Round(number, 8).ToString();
        }

        public static string CreateFinalString()
        {
            return LogicString.GetRandomDateLast5Years() + "||" + LogicString.GetRandomSymbols(latinSymbols) + "||"
                + LogicString.GetRandomSymbols(russianSymbols) + "||" + LogicString.GetRandomEvenNumber() + "||"
                + LogicString.GetRandomNumberWith8DecimalPlaces() + "||"+"\n";
        }
    }
}
