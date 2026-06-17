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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Переходим на окно регистрации...", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            RegustrationWindow regustrationWindow = new RegustrationWindow();
            regustrationWindow.Show();
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string inputLeftEmail = txtLogin.Text.Trim();
            string inputPassword = passLogin.Password.Trim();

            if (String.IsNullOrWhiteSpace(inputLeftEmail) || String.IsNullOrWhiteSpace(inputPassword))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (inputLeftEmail == "admin" && inputPassword == "admin")
            {
                MessageBox.Show("Добро пожаловать, Администратор!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                AdminPanelWindow adminWin = new AdminPanelWindow();
                adminWin.Show();
                this.Close();
                return;
            }
            ComboBoxItem selectedDomainItem = cmbDomainsLog.SelectedItem as ComboBoxItem;
            string selectedDomain = "";
            if (selectedDomainItem != null)
            {
                selectedDomain = selectedDomainItem.Content.ToString();
            }
            string fullInputEmail = inputLeftEmail + selectedDomain;
            if (RegustrationWindow.UsersDatabase.ContainsKey(fullInputEmail))
            {
                List<string> passwords = RegustrationWindow.UsersDatabase[fullInputEmail];

                if (passwords.Contains(inputPassword))
                {
                    passwords.Remove(inputPassword);
                    if (passwords.Count == 1)
                    {
                        MessageBoxResult result = MessageBox.Show(
                            "Внимание! В вашем списке остался всего 1 пароль.\n\nСгенерировать для вас еще 5 случайных паролей?",
                            "Безопасность системы", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            Random rand = new Random();
                            for (int i = 0; i < 5; i++)
                            {
                                passwords.Add("NewPass" + rand.Next(100, 999));
                            }
                            MessageBox.Show("Новые 5 паролей успешно добавлены в ваш список!", "Система");
                        }
                    }
                    if (passwords.Count == 0)
                    {
                        RegustrationWindow.UsersDatabase.Remove(fullInputEmail);
                        MessageBox.Show("Ваш список одноразовых паролей пуст. Доступ запрещен!", "Блокировка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    MessageBox.Show("Авторизация успешна! Переход в каталог объектов...", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    CatalogWindow catalogWindow = new CatalogWindow();
                    catalogWindow.Show();
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}