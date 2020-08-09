using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.SOLIDDesignPrinciple
{


    /// <summary>
    /// You should be able to substitute a base type for a sub type
    /// </summary>
    class LiskovSubstitutionPrinciple
    {
    }



    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width},  {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Height { get => base.Height; set => base.Width =  base.Height = value; }
        public override int Width { get => base.Width; set => base.Height =  base.Width = value; }


    }


}
