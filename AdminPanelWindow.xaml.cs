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
            if (RegustrationWindow.PendingRequests != null)
            {
                dgRequests.ItemsSource = RegustrationWindow.PendingRequests.ToList();
            }
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            if (RegustrationWindow.PendingRequests.Count > 0)
            {
                var firstRequest = RegustrationWindow.PendingRequests.First();
                string email = firstRequest.Key;
                List<string> passwords = new List<string> {"Pass123", "Pass1231", "Pass1232"};
                RegustrationWindow.UsersDatabase.Add(email, passwords);
                RegustrationWindow.PendingRequests.Remove(email);
                MessageBox.Show($"Заявка {email} успешно одобрена!", "Успех");
                dgRequests.ItemsSource = RegustrationWindow.PendingRequests.ToList();
            }
            else
            {
                MessageBox.Show("Нет активных заявок!", "Инфо");
            }
        }
    }
}
