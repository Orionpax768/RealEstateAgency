using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace RealEstateAgency
{
    /// <summary>
    /// Логика взаимодействия для CatalogWindow.xaml
    /// </summary>
    public class PropertyObject
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public string Floor { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
    public partial class CatalogWindow : Window
    {
        public static List<PropertyObject> PropertiesDatabase = new List<PropertyObject>();
        private bool _isCurrentActorAdmin = false;
        public CatalogWindow()
        {
            InitializeComponent();
            _isCurrentActorAdmin = false;
            btnChangeStatus.IsEnabled = false;
            btnChangeStatus.ToolTip = "Изменение статусов доступно только Администратору!";
            InitializeTestData();
            RefreshCatalogTable();
        }
        public CatalogWindow(bool isAdmin)
        {
            InitializeComponent();
            _isCurrentActorAdmin = isAdmin;

            if (!_isCurrentActorAdmin)
            {
                btnChangeStatus.IsEnabled = false;
            }
            InitializeTestData();
            RefreshCatalogTable();
        }

        private void InitializeTestData()
        {
            if (PropertiesDatabase.Count == 0)
            {
                PropertiesDatabase.Add(new PropertyObject { Title = "2-к. квартира", Price = 12.5, Area = 56, Floor = "7/17", Address = "Центральный район, ул. Ленина, 24", Status = "Свободно" });
                PropertiesDatabase.Add(new PropertyObject { Title = "Студия", Price = 5.2, Area = 28, Floor = "3/9", Address = "Первомайский район, ул. Кирова, 12", Status = "Забронировано" });
                PropertiesDatabase.Add(new PropertyObject { Title = "Дом", Price = 25.0, Area = 150, Floor = "1/2", Address = "Октябрьский район, ул. Лесная, 5", Status = "Продано / Аренда" });
            }
        }

        private void RefreshCatalogTable()
        {
            dgCatalog.ItemsSource = null;
            dgCatalog.ItemsSource = PropertiesDatabase;
        }

        private void btnChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            PropertyObject selectedObj = dgCatalog.SelectedItem as PropertyObject;

            if (selectedObj == null)
            {
                MessageBox.Show("Выберите объект из таблицы для изменения его статуса!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (selectedObj.Status == "Свободно")
            {
                selectedObj.Status = "Забронировано";
            }
            else if (selectedObj.Status == "Забронировано")
            {
                selectedObj.Status = "Продано / Аренда";
            }
            else
            {
                selectedObj.Status = "Свободно";
            }
            MessageBox.Show("Статус объекта на '" + selectedObj.Address + "' успешно изменен на: " + selectedObj.Status, "Статус обновлен");
            RefreshCatalogTable();
        }

        private void btnAddObjOpen_Click(object sender, RoutedEventArgs e)
        {
            AddObjectWindow addWin = new AddObjectWindow();
            addWin.ShowDialog();
            RefreshCatalogTable();
        }

        private void btnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tagFilter_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}