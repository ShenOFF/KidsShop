using HappyKids.Classes;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace HappyKids.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecoveryPassword.xaml
    /// </summary>
    public partial class RecoveryPassword : Page
    {
        MainWindow mainWindow;
        Classes.Login login;
        
        public RecoveryPassword(MainWindow mainWindow,Classes.Login login)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.login = login;
        }

        private void Click_Log_Page(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.login(mainWindow));
        }

        string test_login;
        string test_number;
        private void Click_Reg_Accept(object sender, RoutedEventArgs e)
        {
            var rec_login = login_rec.Text;
            var rec_number = tb_number.Text;
            for(int i = 0; i < mainWindow.logins.Count; i++)
            {
                test_login = mainWindow.logins[i].name_user;
                test_number = mainWindow.logins[i].number;
                if (test_login == rec_login && test_number == rec_number)
                {
                    Connection.Class1.Select($"UPDATE Login SET password_user='{password_rec.Password}' where name_user='{rec_login}'");
                    MessageBox.Show("Вы успешно изменили пароль!");
                    mainWindow.OpenPage(new Pages.login(mainWindow));
                    return;
                }
                
            }
                MessageBox.Show("Аккаунта с такими данными не существует. Перепроверите правильность заполнения!");


            


        }
    }
}
