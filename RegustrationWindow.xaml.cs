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
        public static Dictionary<string, List<string>> UsersDatabase = new Dictionary<string, List<string>>();
        public RegustrationWindow()
        {
            InitializeComponent();
        }
        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            string fullname = txtBxFullname.Text.Trim();
            string email = txtBxEmail.Text.Trim();
            string pass1 = txtBxPassword.Password;
            string pass2 = txtBxPassword2.Password;
            if (String.IsNullOrWhiteSpace(fullname))
            {
                MessageBox.Show("Поле ФИО не заполнено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }
            if (String.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Поле email не заполнено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (String.IsNullOrWhiteSpace(pass1) || String.IsNullOrWhiteSpace(pass2))
            {
                MessageBox.Show("Поле пароль или подтверждение пароля не заполнено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (UsersDatabase.ContainsKey(email))
            {
                MessageBox.Show("Пользователь с таким Email уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (pass1 != pass2)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBxPassword.Clear();
                txtBxPassword2.Clear();
                return;
            }
            List<string> userPasswordList = new List<string>();
            userPasswordList.Add(pass1);
            userPasswordList.Add(pass1 + "1");
            userPasswordList.Add(pass1 + "2");
            UsersDatabase.Add(email, userPasswordList);
            MessageBox.Show($"Вы зарегистрированы!\nДля вас создано {userPasswordList.Count} одноразовых паролей:\n1) {pass1}\n2) {pass1}1\n3) {pass1}2",
                            "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            txtBxEmail.Clear();
            txtBxPassword.Clear();
            txtBxPassword2.Clear();
        }

        private void btnLoginback_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}