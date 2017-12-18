using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab2.Models
{
    public class Jedi: Model
    {
        public enum StageEnum { MASTER, PADAVAN, KNIGHT, GENERAL }
        public enum SideOfStrengthEnum { DARK, LIGHT }
        public string Name { get; set; }
        public int Age { get; set; }
        public StageEnum Stage { get; set; }
        public SideOfStrengthEnum SideOfStrength { get; set; }
        //public int Master { get; set; }
    }
}