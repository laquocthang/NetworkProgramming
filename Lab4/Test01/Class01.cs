using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test01
{
    public class Class01
    {
        private string str1;

        public string Str1
        {
            get
            {
                return str1;
            }

            set
            {
                str1 = value;
            }
        }
        public Class01() { }
        public void ShowOn()
        {
            Console.WriteLine(Str1);
        }
    }
}
