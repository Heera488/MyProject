using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Data
{
    public class ItemData
    {

        public long itmId { get; set; }
        public long itemCode { get; set; }


        public string ID { get; set; }
        public string Item_Code { get; set; }
        public string Item_Name { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Createdby { get; set; }
        public string Createdon { get; set; }
        public string ModifiedBy { get; set; }
        public string Modifiedon { get; set; }
    }


}
