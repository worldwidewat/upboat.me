using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpboatMe.Models
{
    public class MemeRequest
    {
        public string Name { get; set; }
        public string Top { get; set; }
        public string Bottom { get; set; }

        public MemeRequest()
        {
            Name = "";
            Top = "";
            Bottom = "";
        }
    }
}