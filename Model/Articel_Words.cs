using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Model
{
   public class Articel_Words
    {
       public string WordPattern { get; set; }
       public bool IsForbid { get; set; }
       public bool IsMod { get; set; }
       public string ReplaceWord { get;set;}
    }
}
