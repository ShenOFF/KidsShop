using HappyKids.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace HappyKids.Elements
{
    /// <summary>
    /// Логика взаимодействия для BasketItem.xaml
    /// </summary>
    public partial class BasketItem : UserControl
    {
        MainWindow mainWindow;
        public static BasketItem basketItem;
        public Classes.Basket baskets;
        public Classes.Product products;
        public Classes.Purchased purchased;
        Classes.Login logins;



        public BasketItem(MainWindow mainWindow, Classes.Basket baskets,Classes.Product products,Classes.Login logins)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.baskets = baskets;
            this.products = products;
            this.logins=logins;
            basketItem = this;
            
            product_name.Content = baskets.product_name;
            price_basket.Content = "Цена: " + baskets.product_price + "руб.";
            old_basket.Content = "От: " + baskets.product_old + "лет";
            count_basket.Content = "Кол-во: " + baskets.product_count + "шт";


            DataTable queryUsers = Connection.Class1.Select($"SELECT src FROM Product where id={baskets.id_product}");
            for (int i = 0; i < queryUsers.Rows.Count; i++)
            {
                imageB.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])queryUsers.Rows[i][0]));
            }
        }
        public string fio;
        private void Buy_basket(object sender, MouseButtonEventArgs e)
        {
            DataTable fio_s = Connection.Class1.Select($"SELECT fio FROM Login where id_user={data.id}");
            for (int i = 0; i < fio_s.Rows.Count; i++)
            {
                fio= Convert.ToString(fio_s.Rows[i][0]);
            }
            string fio_buy = fio;
            Connection.Class1.Select($"INSERT INTO Purchased(id_puchased,name_puchased,price_puchased,count_puchased,id_buyer,fio_buy) VALUES('{baskets.id_product}','{baskets.product_name}','{baskets.product_price}','{baskets.product_count}','{data.id}','{fio_buy}')");
            //Connection.Class1.Select($"INSERT INTO BuyList (id_seller) VALUES ('{data.id}')");
            MessageBox.Show("Вы успешно купили товар!");
            //SaveStudentsExcel();
            delete_buyprod(sender, e);
            mainWindow.LoadBasket();
            mainWindow.LoadPurchased();
            mainWindow.frame.Navigate(new Pages.BasketProduct(mainWindow, products,baskets));

        }
        public int str;
        private void delete_basket(object sender, MouseButtonEventArgs e)
        {
            int count_tb = Convert.ToInt32(baskets.product_count);
            DataTable storage = Connection.Class1.Select($"SELECT count_storage FROM Product where id={baskets.id_product}");
            for (int i = 0; i < storage.Rows.Count; i++)
            {
                
                str = Convert.ToInt32(storage.Rows[i][0]);
                
            }
            int kol = str + count_tb;
            //int str = Convert.ToInt32(storage);
            string query = $"DELETE FROM Basket where id = {baskets.id}";
            
            Connection.Class1.Select($"UPDATE Product SET count_storage='{kol}' WHERE Product.id={baskets.id_product}");
            Connection.Class1.Select(query);
            mainWindow.LoadBasket();
            mainWindow.LoadProduct();
            mainWindow.frame.Navigate(new Pages.BasketProduct(mainWindow, products,baskets));
        }

        private void delete_buyprod(object sender, MouseButtonEventArgs e)
        {
            string query = $"DELETE FROM Basket where id = {baskets.id}";
            Connection.Class1.Select(query);
            mainWindow.LoadBasket();
        }

        private void open_description_basket(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(baskets.product_description, "Описание");
        }

        

    }
}
