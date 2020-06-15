using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.Models.Home
{
    public class Menu
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }
    }
}
