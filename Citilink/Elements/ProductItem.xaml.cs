using HappyKids.Classes;
using HappyKids.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
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
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace HappyKids.Elements
{
    /// <summary>
    /// Логика взаимодействия для ProductItem.xaml
    /// </summary>
    public partial class ProductItem : UserControl
    {
        public static int countS;
        public static ProductItem productItem;
        MainWindow mainWindow;
        Classes.Product products;
        Classes.Basket baskets;
        main main;
        Elements.ProductItem item;


        public ProductItem(MainWindow mainWindow, Classes.Product products, Classes.Basket baskets)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.products = products;
            this.baskets = baskets;
            productItem = this;

            //Вывод для main
            product_name.Content = products.name;
            count_storeg.Content = "На складе: " + products.count_storage + "шт";
            old.Content = "От: " + products.old + "лет";
            price_main.Content = "Цена: " + products.price + "руб.";




            //Вывод для Basket
            //price_basket.Content = "Цена: " + baskets.product_price + "руб.";
            //count_basket.Content = "Кол-во:  " + baskets.product_count;
            //old_basket.Content = "От: " + baskets.product_count + "лет";

            //разделение на роли
            if (data.role != "Admin") admin_role.Visibility = Visibility.Hidden;
            if (data.role != "Admin") admin_role1.Visibility = Visibility.Hidden;
            if (data.role == "Guest")
            {
                Pages.main.mainItem.admin_role.Visibility = Visibility.Hidden;
                Pages.main.mainItem.historyBuy.Visibility = Visibility.Hidden;
                Pages.main.mainItem.profile.Visibility = Visibility.Hidden;
                Pages.main.mainItem.cart.Visibility = Visibility.Hidden;

                Elements.ProductItem.productItem.admin_role.Visibility = Visibility.Hidden;
                Elements.ProductItem.productItem.admin_role1.Visibility = Visibility.Hidden;
                Elements.ProductItem.productItem.basketBtn.Visibility = Visibility.Hidden;
                Elements.ProductItem.productItem.count_in_basket.Visibility = Visibility.Hidden;
            }


            

            image.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])products.src));



        }

        private void change(object sender, MouseButtonEventArgs e)
        {

            mainWindow.frame.Navigate(new Pages.ChangeItems(mainWindow, products));
        }

        private void delete(object sender, MouseButtonEventArgs e)
        {
            string query = $"DELETE FROM Product where id = {products.id}";
            Connection.Class1.Select(query);
            mainWindow.LoadProduct();
            mainWindow.frame.Navigate(new Pages.main(mainWindow, products,baskets,item));

        }

        private void count_in_basket_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int count_b = Convert.ToInt32(count_in_basket.Text);
                int price_b = Convert.ToInt32(products.price);
                if (count_in_basket.Text.Length == 0 || count_b == 0 || count_in_basket.Text == "" || count_b<0)
                {
                    result_price.Content = "Итого: 0";

                }
                else
                {
                    int result_b = count_b * price_b;

                    result_price.Content = "Итого: " + result_b + "руб.";
                
                }
                return;
            }
            catch
            {
                result_price.Content = "Итого: 0";
            }



        }



        private void open_description(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(products.description, "Описание");
        }
        int idproduct;
        int countT;
        int price_basket;
        private void InBasket(object sender, MouseButtonEventArgs e)
        {
            for(int i = 0; i < mainWindow.baskets.Count; i++)
            {
                price_basket = mainWindow.baskets[i].product_price;
            }
            int count_tb=0;
            if (count_in_basket.Text != "" && mainWindow.ItsNumber(count_in_basket.Text))
            {
                count_tb = Convert.ToInt32(count_in_basket.Text);
            }
            
            int storage = Convert.ToInt32(products.count_storage);
            int price = Convert.ToInt32(products.price);
           
            if (count_tb > storage || count_tb == 0 || count_tb<0)
            {
                MessageBox.Show("Укажите другое кол-во.");
                return;
            }
            else
            {

                DataTable queryBasketAdd = Connection.Class1.Select($"select id_product,product_count from Basket where id_product = '{products.id}'");
                for (int i = 0; i < queryBasketAdd.Rows.Count; i++)
                {
                    idproduct = Convert.ToInt32(queryBasketAdd.Rows[i][0]);
                    countT = Convert.ToInt32(queryBasketAdd.Rows[i][1]);
                }
                if (products.id == idproduct)
                {

                    int transfercount = count_tb + countT;
                    int sum = count_tb * price;
                    int allsum = sum + price_basket;
                    int kol = storage - count_tb;
                    //int sum = transfercount * price;
                    Connection.Class1.Select($"UPDATE Product SET count_storage='{kol}' WHERE Product.id={products.id}");
                    Connection.Class1.Select($"UPDATE Basket SET product_count='{transfercount}',product_price='{allsum}' WHERE id_product={products.id}");
                    MessageBox.Show("Кол-во выбранного товара обновлено!");
                    
                }
                else
                {
                    int kol = storage - count_tb;
                    int sum = count_tb * price;
                    Connection.Class1.Select($"UPDATE Product SET count_storage='{kol}' WHERE Product.id={products.id}");
                    Connection.Class1.Select($"INSERT INTO Basket (id_product,id_seller,product_name,product_price,product_old,product_count,product_description) VALUES ('{products.id}','{data.id}','{products.name}','{sum}','{products.old}','{count_tb}','{products.description}')");
                }



                //string query = $"insert into Basket(NameProduct, Price, Product, UserId, ProductId) select '{itemProducts.NameProduct}', '{itemProducts.Price}', '{itemProducts.Description}', '{Pages.Authorization.UserId}', '{itemProducts.idProduct}' from Products where idProduct = '{itemProducts.idProduct}';";
                //string query = $"INSERT INTO Basket(NameProduct, Price, Product, img) VALUES('{itemProducts.NameProduct}', '{itemProducts.Price}', '{itemProducts.NameProduct}', '{itemProducts.img1}')";
                //mainWindow.Select(query);
                //mainWindow.LoadBasket();


                MessageBox.Show("Вы успешно добавили товар в корзину!");

                mainWindow.LoadProduct();
                mainWindow.frame.Navigate(new Pages.main(mainWindow, products, baskets, item));
                //main.AllProductLook(mainWindow.products);
            }


        }

        private void Buy_basket(object sender, MouseButtonEventArgs e)
        {

        }

        private void delete_basket(object sender, MouseButtonEventArgs e)
        {

        }

        private void open_description_basket(object sender, MouseButtonEventArgs e)
        {

        }

       
    }
}
