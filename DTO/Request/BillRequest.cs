using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request
{
    public  class BillRequest
    {
    public string Rqstlist { get; set; }

        //public string ID { get; set; }
        //public string Item_Code { get; set; }
        //public string Item_Name { get; set; }
        //public string Quantity { get; set; }
        //public string Price { get; set; }
        //public string Createdby { get; set; }
        //public string Createdon { get; set; }
        //public string ModifiedBy { get; set; }
        //public string Modifiedon { get; set; }

    }
    public class UpDelRequest
    {

       
        public string itemName { get; set; }
        public string itemRate { get; set; }
        public string type { get; set; }
       

    }

    public class ItemRequest
    {

        public string type { get; set; }
        public string name { get; set; }
        public string id { get; set; }


    }

}
