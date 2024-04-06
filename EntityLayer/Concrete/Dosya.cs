using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Dosya
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public byte[] Boyut { get; set; }
        public string DosyaTipi { get; set; }
    }
}
