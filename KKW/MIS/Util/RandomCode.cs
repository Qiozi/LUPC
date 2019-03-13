using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public class RandomCode
    {
        public static int SixCode
        {
            get
            {
                System.Threading.Thread.Sleep(10);
                Random rnd = new Random();               
                return rnd.Next(100000, 999999);
            }
        }
        public static int EightCode
        {
            get
            {
                System.Threading.Thread.Sleep(10);
                Random rnd = new Random();
                return rnd.Next(10000000, 99999999);
            }
        }

        
    }
}
