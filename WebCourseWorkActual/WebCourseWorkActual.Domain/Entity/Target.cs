using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test3.Domain.Entity
{
    public class Target
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Sum { get; set; }
        public bool TergetType { get; set; }
        public Check Check { get; set; }
        public int CheckId { get; set; }
    }
}