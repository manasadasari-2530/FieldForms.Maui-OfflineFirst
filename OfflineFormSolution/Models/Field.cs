using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineFormSolution.Models
{
    public class Field
    {
        public string Label { get; set; }
        public string Type { get; set; }
        public string Binding { get; set; }
        public bool Required { get; set; }
        public string DataSource { get; set; }
    }

}
