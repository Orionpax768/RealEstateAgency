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
        public static Dictionary<string, string> PendingRequests = new Dictionary<string, string>();
        public static Dictionary<string, List<string>> UsersDatabase = new Dictionary<string, List<string>>();
        public RegustrationWindow()
        {
            InitializeComponent();

        }
        static RegustrationWindow()
        {
            //Данные администратора
            string adminEmail = "adminRealEstateAgency@gmail.com";
            List<string> adminPasswords = new List<string> { "EmiladmimRealEstateAgency" };
            if (!UsersDatabase.ContainsKey(adminEmail))
            {
                UsersDatabase.Add(adminEmail, adminPasswords);
            }
        }
        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            //Данные админа
            string inputEmail = txtBxEmail.Text.Trim();
            string inputPassword = txtBxPassword.Password;
            //
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
            ComboBoxItem selectedDomainItem = cmbDomains.SelectedItem as ComboBoxItem;
            string selectedDomain = "";
            if (selectedDomainItem != null)
            {
                selectedDomain = selectedDomainItem.Content.ToString();
            }
            string fullEmail = email + selectedDomain;

            if (UsersDatabase.ContainsKey(fullEmail))
            {
                MessageBox.Show("Пользователь с таким Email уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (PendingRequests.ContainsKey(fullEmail))
            {
                MessageBox.Show("Ваша заявка уже находится на рассмотрении у администратора!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (pass1 != pass2)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBxPassword.Clear();
                txtBxPassword2.Clear();
                return;
            }
            PendingRequests.Add(fullEmail, fullname);
            MessageBox.Show(
                $"Уважаемый(а) {fullname}, профиль успешно сформирован!\n\n" +
                "Заявка отправлена администратору. После её одобрения в Панели Администратора " +
                "система автоматически сгенерирует вам список паролей для входа.",
                "Заявка создана",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
            txtBxEmail.Clear();
            txtBxPassword.Clear();
            txtBxPassword2.Clear();
            this.DialogResult = true;
        }

        private void btnLoginback_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}