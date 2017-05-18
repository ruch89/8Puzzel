using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _8Puzzel
{
    public class DrawRect
    {
        public static bool IsInRect(int x, int y, Rectangle rect)
        {
            if (x >= rect.X && x <= rect.X + rect.Width && y >= rect.Y && y <= rect.Y + rect.Height)
            {
                return true;
            }
            return false;
        }
    }
}
