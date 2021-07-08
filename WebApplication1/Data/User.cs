using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Data
{
    public class User
    {
        public int UserId { get; set; }
        public string Filename { get; set; }
        public string FilePath { get; set; }
        public DateTime? UploadTime { get; set; }

        [ForeignKey("Login")]
        public int LoginId { get; set; }
        public virtual Login Login { get; set; }

    }
}