using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab2.Models
{
    public class Ability: Model
    {
        public string Description { get; set; }
        public int Power { get; set; }
        public int Jedi { get; set; }
    }
}