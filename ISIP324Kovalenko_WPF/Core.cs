using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324Kovalenko_WPF
{
    internal class Core
    {
        public static pr10Entities Context = new pr10Entities();

        public static User CurrentUser { get; set; }
    }
}
