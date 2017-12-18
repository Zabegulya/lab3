using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab2.Respositories
{
    abstract public class Repository<T>
    {
        public int CurrentId { get; set; }
        protected Dictionary<int, T> Entities = new Dictionary<int, T>();
    }
}