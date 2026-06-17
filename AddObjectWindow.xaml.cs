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
    /// Логика взаимодействия для AddObjectWindow.xaml
    /// </summary>
    public partial class AddObjectWindow : Window
    {
        public AddObjectWindow()
        {
            InitializeComponent();
        }
        private void btnSaveObject_Click(object sender, RoutedEventArgs e)
        {
            string title = txtObjTitle.Text.Trim();
            string priceText = txtObjPrice.Text.Trim();
            string areaText = txtObjArea.Text.Trim();
            string floor = txtObjFloor.Text.Trim();
            string address = txtObjAddress.Text.Trim();
            ComboBoxItem selectedStatusItem = cbObjStatus.SelectedItem as ComboBoxItem;
            string status = selectedStatusItem != null ? selectedStatusItem.Content.ToString() : "Свободно";
            if (String.IsNullOrWhiteSpace(title) || String.IsNullOrWhiteSpace(priceText) ||
                String.IsNullOrWhiteSpace(areaText) || String.IsNullOrWhiteSpace(floor) || String.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            double price = 0;
            double area = 0;
            if (!Double.TryParse(priceText, out price) || !double.TryParse(areaText, out area))
            {
                MessageBox.Show("В полях 'Цена' и 'Площадь' должны быть указаны только числа!", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            PropertyObject newProperty = new PropertyObject() {Title = title, Price = price, Area = area, Floor = floor,
                Address = address, Status = status};
            CatalogWindow.PropertiesDatabase.Add(newProperty);

            MessageBox.Show("Новый объект недвижимости успешно добавлен в каталог!", "Успех", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
        private void btnCancelAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}