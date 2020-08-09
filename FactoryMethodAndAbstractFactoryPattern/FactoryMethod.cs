using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.FactoryMethodAndAbstractFactoryPattern
{

    public enum CoordinateSystem
    {
        Cartesian, Polar
    }

    public class Point
    {

        private double x, y;

        #region Factory Method
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }


        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion;

        #region Anti Pattern
        /// <summary>
        /// Initializes a point from either CARTESIAN or POLAR
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="system"></param>
        public Point(double a, double b, CoordinateSystem system = CoordinateSystem.Cartesian)
        {

            switch (system)
            {
                case CoordinateSystem.Cartesian:
                    x = a;
                    y = b;
                    break;
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = a * Math.Sin(b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(system), system, null);
            }


        }

        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this); 
        }

    }
}
