using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralDesignPatterns.DecoratorPattern
{
    class DecoratorPattern
    {
    }



    #region Decorator Pattern
    public class MyStringBuilder
    {
        private StringBuilder builder = new StringBuilder();


         public static implicit operator MyStringBuilder(string s)
        {
            MyStringBuilder myStringBuilder = new MyStringBuilder();
             myStringBuilder.builder.Append(s);
            return myStringBuilder;
        }

        public static MyStringBuilder operator + (MyStringBuilder myStringBuilder,string s)
        {
             myStringBuilder.builder.Append(s);
            return myStringBuilder;
        }

        public override string ToString()
        {
            return builder.ToString();
        }


    }
    #endregion

    #region Dynamic Decorator Composition

     public interface IShape
    {
        string AsString();
    }


    public class Cirlce : IShape
    {
        private float _radius;

        public Cirlce(float radius)
        {
            _radius = radius;
        }


        public void Resize(float factor)
        {
            _radius *= factor;
        }

        public string AsString() => $"A circle with radius {_radius}";
    }

    public class Square : IShape
    {
        private float _side;

        public Square(float side)
        {
            _side = side;
        }


       

        public string AsString() => $"A circle with side {_side}";
    }

    public class ColoredShape : IShape
    {
        private IShape _shape;
        private string _color;

        public ColoredShape(IShape shape, string color)
        {
            _shape = shape ?? throw new ArgumentNullException(nameof(shape));
            _color = color ?? throw new ArgumentNullException(nameof(color));
        }

        public string AsString() => $"{_shape.AsString()} has the color {_color}";   
    }

    public class TransperentShape : IShape
    {
        private IShape _shape;
        private float _transperency;

        public TransperentShape(IShape shape, float transperency)
        {
            _shape = shape ?? throw new ArgumentNullException(nameof(shape));
            _transperency = transperency;
        }

        public string AsString() => $"{_shape.AsString()} has {_transperency*100.0}%  transperency";
    }

     

    #endregion








}
