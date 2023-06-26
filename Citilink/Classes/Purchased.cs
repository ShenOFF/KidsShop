using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyKids.Classes
{
    public class Purchased
    {
        public int id { get; set; }
        public string name_puchased { get; set; }
        public int id_puchased { get; set; }
        public int count_puchased { get; set; }
        public int price_puchased { get; set; }
        public int id_buyer { get; set; }
        public string fio_buy { get; set; }
    }
}
