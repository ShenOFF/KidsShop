using Connection;
using HappyKids.Classes;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace HappyKids
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Classes.Purchased purchased;
        public static MainWindow mainWindow;
        public List<Classes.Product> products = new List<Classes.Product>();
        public List<Classes.Login> logins = new List<Classes.Login>();
        public List<Classes.Basket> baskets = new List<Classes.Basket>();
        public List<Classes.Purchased> purchaseds = new List<Classes.Purchased>();
        public static Pages.login loginp;
        public static Pages.Registration regp;
        public static Pages.ClientInfo clientInfo;
        public static Connection.Class1 connect;
        public static Class1 connect_It;
        public MainWindow()
        {
            InitializeComponent();

            //mainWindow = this;

            OpenPage(new Pages.login(this));

            LoadProduct();
            LoadLoginInfo();
            LoadBasket();
            LoadPurchased();
            
            //OpenPage(new Pages.main(this));

            //OpenPage(new Pages.BasketProduct(this, products[0]));
        }

        public void OpenPage(Page page)
        {
            // анимация исчезноваения
            // создаём анимацию
            DoubleAnimation opgridAnimation = new DoubleAnimation();
            // устанавливаем начальное время анимации
            opgridAnimation.From = 1;
            // устанавливаем конечное время анимации
            opgridAnimation.To = 0;
            // устанавливаем продолжительность времени анимации
            opgridAnimation.Duration = TimeSpan.FromSeconds(0.6);
            // при выполнении анимации выполняем:
            opgridAnimation.Completed += delegate
            {
                // переходим на страницу
                frame.Navigate(page);
                // анимация появления
                DoubleAnimation opgrisdAnimation = new DoubleAnimation();
                opgrisdAnimation.From = 0;
                opgrisdAnimation.To = 1;
                opgrisdAnimation.Duration = TimeSpan.FromSeconds(1.2);
                // начинаем выполнение анимации
                frame.BeginAnimation(Frame.OpacityProperty, opgrisdAnimation);
            };
            // начинаем выполнение анимации
            frame.BeginAnimation(Frame.OpacityProperty, opgridAnimation);
        }

        public void LoadProduct()
        {
            products.Clear();
            DataTable product_query = Connection.Class1.Select("SELECT * FROM Product");
            for (int i = 0; i < product_query.Rows.Count; i++)
            {
                products.Add(new Classes.Product()
                {
                    id = Convert.ToInt32(product_query.Rows[i][0]),
                    src = (byte[])(product_query.Rows[i][1]),
                    name = Convert.ToString(product_query.Rows[i][2]),
                    weight = Convert.ToString(product_query.Rows[i][3]),
                    count_storage = Convert.ToString(product_query.Rows[i][4]),
                    old = Convert.ToString(product_query.Rows[i][5]),
                    price = Convert.ToString(product_query.Rows[i][6]),
                    description = Convert.ToString(product_query.Rows[i][7]),
                    
                });

            }
        }

        public void LoadLoginInfo()
        {
            logins.Clear();
            DataTable login_query = Connection.Class1.Select("SELECT * FROM Login");
            for (int i = 0; i < login_query.Rows.Count; i++)
            {
                logins.Add(new Classes.Login()
                {
                    id_user = Convert.ToInt32(login_query.Rows[i][0]),
                    name_user = Convert.ToString(login_query.Rows[i][1]),
                    password_user = Convert.ToString(login_query.Rows[i][2]),
                    fio = Convert.ToString(login_query.Rows[i][3]),
                    number = Convert.ToString(login_query.Rows[i][4]),
                    addres = Convert.ToString(login_query.Rows[i][5]),
                    mail = Convert.ToString(login_query.Rows[i][6]),
                    bith = Convert.ToString(login_query.Rows[i][7]),
                    role = Convert.ToString(login_query.Rows[i][8]),
                });

            }
        }

        public void LoadBasket()
        {
            baskets.Clear();
            DataTable login_query = Connection.Class1.Select("SELECT * FROM Basket");
            for (int i = 0; i < login_query.Rows.Count; i++)
            {
                baskets.Add(new Classes.Basket()
                {
                    id = Convert.ToInt32(login_query.Rows[i][0]),
                    id_product = Convert.ToInt32(login_query.Rows[i][1]),
                    id_seller = Convert.ToInt32(login_query.Rows[i][2]),
                    product_name = Convert.ToString(login_query.Rows[i][3]),
                    //product_src = (byte[])(login_query.Rows[i][4]),
                    product_price = Convert.ToInt32(login_query.Rows[i][4]),
                    product_old=Convert.ToInt32(login_query.Rows[i][5]),
                    product_count = Convert.ToInt32(login_query.Rows[i][6]),
                    product_description = Convert.ToString(login_query.Rows[i][7]),
                });

            }
        }

        public void LoadPurchased()
        {
            purchaseds.Clear();
            DataTable login_query = Connection.Class1.Select("SELECT * FROM Purchased");
            for (int i = 0; i < login_query.Rows.Count; i++)
            {
                purchaseds.Add(new Classes.Purchased()
                {
                    id = Convert.ToInt32(login_query.Rows[i][0]),
                    name_puchased = Convert.ToString(login_query.Rows[i][1]),
                    id_puchased = Convert.ToInt32(login_query.Rows[i][2]),
                    count_puchased = Convert.ToInt32(login_query.Rows[i][3]),
                    price_puchased = Convert.ToInt32(login_query.Rows[i][4]),
                    id_buyer = Convert.ToInt32(login_query.Rows[i][5]),
                    fio_buy= Convert.ToString(login_query.Rows[i][6]),
                    

                });

            }
        }



        //Проверка на номер
        public bool ItsNumber(string str)
        {

            bool isflag = false;
            string isnum = "1234567890";
            if (str != "")
            {
                string write = str.ToLower();
                for (int i = 0; i < write.Length; i++)
                {
                    isflag = false;
                    for (int j = 0; j < isnum.Length; j++)
                    {
                        if (write[i] == isnum[j])
                        {
                            isflag = true;
                        }
                    }
                }
            }
            return isflag;
        }
        //Проверка на ФИО
        public bool ItsOnlyFIO(string str)
        {
            bool isflag = false;
            string isfio = "яфйчыцсвумакипетрньогблшюдщжзэхъzaqxswcdevfrbgtnhymjukilop ";
            if (str != "")
            {
                string note = str.ToLower();
                for (int i = 0; i < note.Length; i++)
                {
                    isflag = false;
                    for (int j = 0; j < isfio.Length; j++)
                    {
                        if (note[i] == isfio[j])
                        {
                            isflag = true;
                        }
                    }
                }
            }
            return isflag;
        }

        public bool IsValidEmailAddress(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public void SaveProducts()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files(.*xlsx)|*.xlsx|All files(*.*)|*.*";

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var excelApp = new Excel.Application();
                excelApp.Visible = false;
                Excel.Workbook workBook = excelApp.Workbooks.Add(Type.Missing);

                Excel.Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
                workSheet.Name = "Журнал покупателей";

                if (Pages.UsersControl.u_Buy == -1)
                {
                    for(int i=0; i < purchaseds.Count; i++)
                    {
                        ((Excel.Range)workSheet.Cells[2, 2]).Value = "ФИО " /*+ FIO*/;
                        ((Excel.Range)workSheet.Cells[2, 2]).ColumnWidth = 35;
                        ((Excel.Range)workSheet.Cells[2, 2]).RowHeight = 30;
                        ((Excel.Range)workSheet.Cells[2, 2]).Interior.Color = Excel.XlRgbColor.rgbOrange;
                        ((Excel.Range)workSheet.Cells[2, 2]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[2, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[2, 1]).Value = "№";
                        ((Excel.Range)workSheet.Cells[2, 1]).ColumnWidth = 35;
                        ((Excel.Range)workSheet.Cells[2, 1]).RowHeight = 30;
                        ((Excel.Range)workSheet.Cells[2, 1]).Interior.Color = Excel.XlRgbColor.rgbOrange;
                        ((Excel.Range)workSheet.Cells[2, 1]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[2, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[2, 3]).Value = "Название";
                        ((Excel.Range)workSheet.Cells[2, 3]).ColumnWidth = 35;
                        ((Excel.Range)workSheet.Cells[2, 3]).RowHeight = 30;
                        ((Excel.Range)workSheet.Cells[2, 3]).Interior.Color = Excel.XlRgbColor.rgbOrange;
                        ((Excel.Range)workSheet.Cells[2, 3]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[2, 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[2, 4]).Value = "Цена";
                        ((Excel.Range)workSheet.Cells[2, 4]).ColumnWidth = 35;
                        ((Excel.Range)workSheet.Cells[2, 4]).RowHeight = 30;
                        ((Excel.Range)workSheet.Cells[2, 4]).Interior.Color = Excel.XlRgbColor.rgbOrange;
                        ((Excel.Range)workSheet.Cells[2, 4]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[2, 4]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[3 + i, 1]).Value = i+1;
                        ((Excel.Range)workSheet.Cells[3 + i, 1]).ColumnWidth = 10;
                        ((Excel.Range)workSheet.Cells[3 + i, 1]).Interior.Color = Excel.XlRgbColor.rgbWhite;
                        ((Excel.Range)workSheet.Cells[3 + i, 1]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[3 + i, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[3 + i, 2]).Value = purchaseds[i].fio_buy;
                        ((Excel.Range)workSheet.Cells[3 + i, 2]).ColumnWidth = 10;
                        ((Excel.Range)workSheet.Cells[3 + i, 2]).Interior.Color = Excel.XlRgbColor.rgbWhite;
                        ((Excel.Range)workSheet.Cells[3 + i, 2]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[3 + i, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[3 + i, 3]).Value = purchaseds[i].name_puchased;
                        ((Excel.Range)workSheet.Cells[3 + i, 3]).ColumnWidth = 10;
                        ((Excel.Range)workSheet.Cells[3 + i, 3]).Interior.Color = Excel.XlRgbColor.rgbWhite;
                        ((Excel.Range)workSheet.Cells[3 + i, 3]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[3 + i, 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[3 + i, 4]).Value = purchaseds[i].price_puchased;
                        ((Excel.Range)workSheet.Cells[3 + i, 4]).ColumnWidth = 10;
                        ((Excel.Range)workSheet.Cells[3 + i, 4]).Interior.Color = Excel.XlRgbColor.rgbWhite;
                        ((Excel.Range)workSheet.Cells[3 + i, 4]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[3 + i, 4]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  
                    }
                }
                else
                {
                    string fio = "";
                    DataTable queryfio = Class1.Select($"select fio from Login where id_user='{Pages.UsersControl.u_Buy}'");
                    for(int i = 0; i < queryfio.Rows.Count; i++)
                    {
                        fio = Convert.ToString(queryfio.Rows[i][0]);
                    }

                    var resultID = purchaseds.FindAll(x => x.id_buyer.ToString().Contains(Pages.UsersControl.u_Buy.ToString()));

                    for(int i = 0; i < resultID.Count; i++)
                    {
                        ((Excel.Range)workSheet.Cells[2, 2]).Value = "ФИО " /*+ fio*/;
                        ((Excel.Range)workSheet.Cells[2, 2]).ColumnWidth = 35;
                        ((Excel.Range)workSheet.Cells[2, 2]).RowHeight = 30;
                        ((Excel.Range)workSheet.Cells[2, 2]).Interior.Color = Excel.XlRgbColor.rgbOrange;
                        ((Excel.Range)workSheet.Cells[2, 2]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[2, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[2, 1]).Value = "№";
                        ((Excel.Range)workSheet.Cells[2, 1]).ColumnWidth = 35;
                        ((Excel.Range)workSheet.Cells[2, 1]).RowHeight = 30;
                        ((Excel.Range)workSheet.Cells[2, 1]).Interior.Color = Excel.XlRgbColor.rgbOrange;
                        ((Excel.Range)workSheet.Cells[2, 1]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[2, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[2, 3]).Value = "Название";
                        ((Excel.Range)workSheet.Cells[2, 3]).ColumnWidth = 35;
                        ((Excel.Range)workSheet.Cells[2, 3]).RowHeight = 30;
                        ((Excel.Range)workSheet.Cells[2, 3]).Interior.Color = Excel.XlRgbColor.rgbOrange;
                        ((Excel.Range)workSheet.Cells[2, 3]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[2, 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[2, 4]).Value = "Цена";
                        ((Excel.Range)workSheet.Cells[2, 4]).ColumnWidth = 35;
                        ((Excel.Range)workSheet.Cells[2, 4]).RowHeight = 30;
                        ((Excel.Range)workSheet.Cells[2, 4]).Interior.Color = Excel.XlRgbColor.rgbOrange;
                        ((Excel.Range)workSheet.Cells[2, 4]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[2, 4]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[3 + i, 1]).Value = i + 1;
                        ((Excel.Range)workSheet.Cells[3 + i, 1]).ColumnWidth = 10;
                        ((Excel.Range)workSheet.Cells[3 + i, 1]).Interior.Color = Excel.XlRgbColor.rgbWhite;
                        ((Excel.Range)workSheet.Cells[3 + i, 1]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[3 + i, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[3 + i, 2]).Value = fio;
                        ((Excel.Range)workSheet.Cells[3 + i, 2]).ColumnWidth = 10;
                        ((Excel.Range)workSheet.Cells[3 + i, 2]).Interior.Color = Excel.XlRgbColor.rgbWhite;
                        ((Excel.Range)workSheet.Cells[3 + i, 2]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[3 + i, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
 
                        ((Excel.Range)workSheet.Cells[3 + i, 3]).Value = resultID[i].name_puchased;
                        ((Excel.Range)workSheet.Cells[3 + i, 3]).ColumnWidth = 10;
                        ((Excel.Range)workSheet.Cells[3 + i, 3]).Interior.Color = Excel.XlRgbColor.rgbWhite;
                        ((Excel.Range)workSheet.Cells[3 + i, 3]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[3 + i, 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        ((Excel.Range)workSheet.Cells[3 + i, 4]).Value = resultID[i].price_puchased;
                        ((Excel.Range)workSheet.Cells[3 + i, 4]).ColumnWidth = 10;
                        ((Excel.Range)workSheet.Cells[3 + i, 4]).Interior.Color = Excel.XlRgbColor.rgbWhite;
                        ((Excel.Range)workSheet.Cells[3 + i, 4]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        ((Excel.Range)workSheet.Cells[3 + i, 4]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    }

                }
                Pages.UsersControl.u_Buy = -1;
                workBook.SaveAs(saveFileDialog.FileName);
                workBook.Close();
                excelApp.Quit();
            }
        }

       
    }
}
