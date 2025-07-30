using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineFormSolution.Models
{
    public class FormSchema
    {
        public string FormName { get; set; }
        public List<Field> Fields { get; set; }
    }
}
