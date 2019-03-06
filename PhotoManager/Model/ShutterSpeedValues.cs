using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoManager.Model
{
    public struct ShutterSpeedValues
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public ShutterSpeedValues(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
    }
}
