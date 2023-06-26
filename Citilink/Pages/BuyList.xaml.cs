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
using HappyKids.Classes;

namespace HappyKids.Pages
{
    /// <summary>
    /// Логика взаимодействия для BuyList.xaml
    /// </summary>
    public partial class BuyList : Page
    {
        MainWindow mainWindow;
        Classes.Purchased purchased;
        Classes.Product products;
        Classes.Basket baskets;
        Elements.ProductItem item;

        public BuyList(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            LoadData();
            
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.main(mainWindow,products,baskets,item));
        }

        public void LoadData()
        {
            listView_Buy.Items.Clear();
            var a = mainWindow.purchaseds.FindAll(x => x.id_buyer == data.id);
            for (int i = 0; i < a.Count; i++)
            {
                listView_Buy.Items.Add(a[i]);
            }
            
        }
    }
}
