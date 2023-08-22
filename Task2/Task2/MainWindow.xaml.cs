using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task2.Entities;

namespace Task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //создание коллекций для привязки данных
        ObservableCollection<string> files = new ObservableCollection<string>();
        ObservableCollection<ModifyBalance> modifyBalances = new ObservableCollection<ModifyBalance>();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += LoadFiles;
            //привязка элементов упраления к колекциям
            ExcelList.ItemsSource = files;
            balanceGrid.ItemsSource = modifyBalances;
        }

        //событие загружающее список загруженых файлов
        private void LoadFiles(object sender, RoutedEventArgs e)
        {
            FileService.LoadListOfFile(files);
        }

        //обработчик события для кнопки импорта+добавления
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            //небольшая валидация
            try
            {
                string path = @PathBox.Text;
                FileService.ReadExcel(path);
                FileService.AddFile(path);
            }
            catch
            {
                MessageBox.Show("неверно указан путь");
            }
        }

        //обработчик события для кнопки отображения
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            string path=string.Empty;
            //проверка выбраного элемента в ComboBox
            try
            {
                if (ExcelList.SelectedValue == null) throw new Exception();
                path = ExcelList.SelectedValue.ToString().Trim('|');
                FileService.FindInfoInDB(path, modifyBalances);
            }
            catch
            {
                MessageBox.Show("Выберите файл");
            }
        }

    }
}
