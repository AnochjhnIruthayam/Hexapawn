using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexapawnLogic
{
    public class LegalMove
    {
        public int FromX, FromY, ToX, ToY;
        public bool casualty;


        public LegalMove(int FromX, int FromY, int ToX, int ToY, bool casualty)
        {
            this.FromX = FromX;
            this.FromY = FromY;
            this.ToX = ToX;
            this.ToY = ToY;

            this.casualty = casualty;
        }
        public UInt32 HashCode(int row, int col)
        {
            UInt32 resultHash = 0;

            resultHash += (UInt32)Math.Pow(row, 0) * (UInt32)FromX;
            resultHash += (UInt32)Math.Pow(col, 1) * (UInt32)FromY;
            resultHash += (UInt32)Math.Pow(row, 2) * (UInt32)ToX;
            resultHash += (UInt32)Math.Pow(col, 3) * (UInt32)ToY;

            return resultHash;
        }
    }
}
