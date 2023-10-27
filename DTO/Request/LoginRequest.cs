using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request
{
    public class LoginRequest
    {
        public long userId { get; set; }
        public string password   { get; set; }
        //public string token   { get; set; }
    }
}
