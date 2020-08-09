using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.SOLIDDesignPrinciple
{
    public class OpenClosedPrinciple
    {
    }


    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }


    /// <summary>
    ///  Open Closed Principle: Parts of a system should be open for extension but closed for modification
    /// </summary>
    public class Product
    {
        public string Name { get; }
        public Color Color { get; }
        public Size Size { get; }

        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }


    #region   Anti Pattern

    public class ProductFilter
    {

        public  IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (Product product in products)
            {
                if (product.Size == size) {
                    yield return product;
                }
            }
        }

        public  IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (Product product in products)
            {
                if (product.Color == color)
                {
                    yield return product;
                }
            }
        }

        public  IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Color color, Size size)
        {
            foreach (Product product in products)
            {
                if (product.Color == color && product.Size == size)
                {
                    yield return product;
                }
            }
        }

    }
    #endregion


    #region Specification Pattern

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> products, ISpecification<Product> spec)
        {
            foreach (Product product in products)
            {
                if (spec.IsSatisfied(product))
                {
                    yield return product;
                }
            }
        }
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private readonly Color _color ;

        public ColorSpecification(Color color)
        {
            _color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == _color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private readonly Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == _size;
        }
    }

    public class SizeAndColorSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> _first, _second;
        public SizeAndColorSpecification(ISpecification<T> first, ISpecification<T>  second)
        {
            _first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            _second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }
   

        public bool IsSatisfied(T t)
        {
            return _first.IsSatisfied(t) && _second.IsSatisfied(t);
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    #endregion


}
