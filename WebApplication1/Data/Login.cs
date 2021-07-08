using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Data
{
    public class Login
    {
        [key]
        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Passwrod { get; set; }
        public DateTime? LastLoginDate { get; set; }  
        

    }
}