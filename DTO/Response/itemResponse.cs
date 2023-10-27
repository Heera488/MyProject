using DTO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response
{
   public class itemResponse
    {
      
            public itemResponse()
            {
                isDataAvailable = false;
            }
            public bool isDataAvailable { get; set; }
        public string message { get; set; }    
            public List<ItemData> item_List { get; set; }

        public long itmId { get; set; }
        public long itemCode { get; set; }


    }
    public class BillinsertResponse
    {
       
        //public BillinsertResponse()
        //{
        //    isDataAvailable = false;
        //}

        public string Message { get; set; }
        public bool isDataAvailable { get; set; }

    }

}
