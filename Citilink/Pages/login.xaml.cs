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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Animation;
using Control = System.Windows.Controls.Control;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;
using MessageBox = System.Windows.MessageBox;
using HappyKids.Classes;

namespace HappyKids.Pages
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Page
    {
        //Database dataBase = new Database();
        public MainWindow mainWindow;
        Classes.Login logins;
        Classes.Product products;
        Classes.Basket baskets;
        Elements.ProductItem item;
        public enum pagess
        {
            login,none,regp,main
        }
        public static pagess page_select;
        public login(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            page_select = pagess.none;
        }

        
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var login = tb_login.Text;
            var password = tb_password.Password;

            
         
            DataTable table = new DataTable();

            string query = $"SELECT id_user,name_user,password_user,role FROM Login where name_user='{login}' and password_user='{password}'";
            table=Connection.Class1.Select(query);

            

            if (table.Rows.Count == 1)
            {
                Classes.data.role = Convert.ToString(table.Rows[0][3]);
                Classes.data.id = Convert.ToInt32(table.Rows[0][0]);
                MessageBox.Show("Вы успешно вошли!","Успешно!",MessageBoxButton.OK);
                
                mainWindow.OpenPage(new Pages.main(mainWindow,products,baskets,item));
            }
            else
            {
                MessageBox.Show("Такого аккаунта нету!", "Аккайунт не найден!", MessageBoxButton.OK);
            }

            
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.Registration(mainWindow,logins));
        }

        private void Recover_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.RecoveryPassword(mainWindow,logins));
        }

        private void Login_ClickGuest(object sender, RoutedEventArgs e)
        {
            data.role = "Guest";
            mainWindow.OpenPage(new Pages.main(mainWindow, products, baskets, item));



            


        }
    }
}
