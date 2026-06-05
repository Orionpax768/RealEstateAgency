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
    /// Логика взаимодействия для RegustrationWindow.xaml
    /// </summary>
    public partial class RegustrationWindow : Window
    {
        public RegustrationWindow()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtBlkFullname.Text)) 
            {
                MessageBox.Show("Поле ФИО не заполенено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (txtBxPassword.Password == txtBxPassword2.Password)
            {
                MessageBox.Show("Вы зарегистрированны! Нажмите кнопку 'Зарегистрироваться'.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (String.IsNullOrWhiteSpace(txtBxPassword.Password) && String.IsNullOrWhiteSpace(txtBxPassword2.Password)) 
            {

            }
            else
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBxPassword.Clear();
                txtBxPassword2.Clear();
            }
        }
    }
}
