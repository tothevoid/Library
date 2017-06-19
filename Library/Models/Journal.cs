using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
   public class Journal
    {
        string User { get; set; }
        string Book { get; set; }
        string DateStart { get; set; }
        string DateEnd { get; set; }
        bool IsEnded { get; set; }
    }
}
