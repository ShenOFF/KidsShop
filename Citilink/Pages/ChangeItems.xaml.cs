using Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using HappyKids.Classes;

namespace HappyKids.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangeItems.xaml
    /// </summary>
    public partial class ChangeItems : Page
    {
        MainWindow mainWindow;
        Classes.Product products;
        byte[] image;
        Classes.Basket baskets;
        Elements.ProductItem item;

        public ChangeItems(MainWindow mainWindow, Classes.Product products)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.products = products;
            //if (products != null)
            //{


            //    if (File.Exists(products.src))
            //    {
            //        s_src = products.src;
            //        src.Source = new BitmapImage(new Uri(s_src));
            //    }
            tb_name.Text = products.name;
            tb_old.Text = products.old;
            tb_price.Text = products.price;
            tb_weight.Text = products.weight;
            tb_count_storage.Text = products.count_storage;
            tb_description.Text = products.description;
           
            src.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray(products.src));
            //}
        }
        private void Click_Reg_Accept(object sender, RoutedEventArgs e)
        {
            //Проверка на введеное название
            if (!mainWindow.ItsOnlyFIO(tb_name.Text) || tb_name.Text == " ")
            {
                MessageBox.Show("Вы не правильно написали название.Перепроверьте!");
                return;
            }

            //Проверка на введеное цены на товар
            if (!mainWindow.ItsNumber(tb_price.Text) || tb_price.Text == " ")
            {
                MessageBox.Show("Вы не правильно указали цену.Перепроверьте!");
                return;
            }

            //Проверка на введеное кол-во на складе
            if (!mainWindow.ItsNumber(tb_count_storage.Text) || tb_count_storage.Text == " ")
            {
                MessageBox.Show("Вы не правильно указали вол-во товара.Перепроверьте!");
                return;
            }

            //Проверка на введеное возростного ограничения
            if (!mainWindow.ItsNumber(tb_old.Text) || tb_old.Text == " ")
            {
                MessageBox.Show("Вы не правильно указали цену.Перепроверьте!");
                return;
            }

            //Проверка на введеное веса
            if (!mainWindow.ItsNumber(tb_weight.Text ) || tb_weight.Text == " ")
            {
                MessageBox.Show("Вы не правильно указали вес.Перепроверьте!");
                return;
            }

            //Проверка на введеный адрес
            if (!mainWindow.ItsOnlyFIO(tb_description.Text) || tb_description.Text==" ")
            {
                MessageBox.Show("Вы не правильно написали описание.Перепроверьте!");
                return;
            }

            if (isImage)
            {
                string connectionString = Class1.pathDB;
                //string connectionString = "server=10.0.146.3;Trusted_Connection=no;DataBase=KidsShop;User=c;PWD=1";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    //command.CommandText = $"INSERT INTO Products(NameProduct, QuantityStock, Description, Price, img) VALUES('{tb_NameProduct.Text}', '{tb_QuantityStock.Text}', '{tb_Description.Text}','{tb_Cost.Text}', (@ImageData))";
                    command.CommandText = ($"UPDATE Product SET src=@ImageData,name='{tb_name.Text}',weight='{tb_weight.Text}',count_storage='{tb_count_storage.Text}',old='{tb_old.Text}',price='{tb_price.Text}',description='{tb_description.Text}'  WHERE Product.id={products.id}");

                    SqlParameter sqlParam = command.Parameters.AddWithValue("@ImageData", image);
                    sqlParam.DbType = DbType.Binary;
                    command.ExecuteNonQuery();

                    MessageBox.Show("Данные изменены");
                    mainWindow.products.Clear();
                    mainWindow.LoadProduct();
                    mainWindow.frame.Navigate(new Pages.main(mainWindow, products, baskets, item));
                }
            }
            else
            {
                DialogResult res= System.Windows.Forms.MessageBox.Show("Оставить старое изображение?","",MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    Connection.Class1.Select($"UPDATE Product SET name='{tb_name.Text}',weight='{tb_weight.Text}',count_storage='{tb_count_storage.Text}',old='{tb_old.Text}',price='{tb_price.Text}',description='{tb_description.Text}'  WHERE Product.id={products.id}");

                    MessageBox.Show("Данные изменены");
                    mainWindow.products.Clear();
                    mainWindow.LoadProduct();
                    mainWindow.frame.Navigate(new Pages.main(mainWindow, products, baskets, item));
                }
                else
                    return;
            }

        }
        ////public string s_src = "BackBear.jpg";
        bool isImage;
        private void Click_Image_Items(object sender, RoutedEventArgs e)
        {
            
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = File.ReadAllBytes(openFileDialog.FileName);
                isImage=true;
            }
            else 
                return;
            
            
            src.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])image));
        }

        private void Click_Main_Page(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.main(mainWindow,products,baskets,item));
        }
    }
}
