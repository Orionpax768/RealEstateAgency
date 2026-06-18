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
    /// Логика взаимодействия для AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow()
        {
            InitializeComponent();
            RefreshTable();
        }

        private void RefreshTable()
        {
            dgRequests.ItemsSource = null;
            if (RegustrationWindow.PendingRequests != null)
            {
                dgRequests.ItemsSource = RegustrationWindow.PendingRequests.ToList();
            }
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            if (dgRequests.SelectedItem == null)
            {
                MessageBox.Show("Выберите заявку из таблицы!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            dynamic selectedRow = dgRequests.SelectedItem;
            string email = selectedRow.Key;
            string name = selectedRow.Value;

            Random rand = new Random();
            int randomNumber = rand.Next(1000, 10000);
            string basePassword = "Pass" + randomNumber;

            List<string> passwords = new List<string>();
            passwords.Add(basePassword);
            passwords.Add(basePassword + "1");
            passwords.Add(basePassword + "2");

            RegustrationWindow.UsersDatabase.Add(email, passwords);
            RegustrationWindow.PendingRequests.Remove(email);
            string messageText = "Заявка сотрудника " + name + " успешно одобрена!\n\n" +
                                 "Передайте ему созданные пароли для входа:\n" +
                                 "1-й вход: " + basePassword + "\n" +
                                 "2-й вход: " + basePassword + "1\n" +
                                 "3-й вход: " + basePassword + "2\n\n" +
                                 "Пароли автоматически стираются после использования!";
            MessageBox.Show(messageText, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshTable();
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            if (dgRequests.SelectedItem == null)
            {
                MessageBox.Show("Выберите заявку для отклонения!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            dynamic selectedRow = dgRequests.SelectedItem;
            string emailKey = selectedRow.Key;
            string nameValue = selectedRow.Value;
            RegustrationWindow.PendingRequests.Remove(emailKey);
            MessageBox.Show("Заявка пользователя " + nameValue + " успешно отклонена.", "Готово");
            RefreshTable();
        }

        private void btnGoToCatalog_Click(object sender, RoutedEventArgs e)
        {
            CatalogWindow catalogWin = new CatalogWindow(true);
            catalogWin.Owner = this;
            catalogWin.ShowDialog();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            this.Close();
        }
    }
}