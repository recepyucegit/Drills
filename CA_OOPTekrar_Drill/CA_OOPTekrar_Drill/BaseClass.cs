using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_OOPTekrar_Drill
{
    public class BaseClass
    {
        public BaseClass()
        {
            CreateDate = DateTime.Now;
        }
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
