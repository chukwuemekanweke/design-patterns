using DesignPatterns.BuilderPattern;
using DesignPatterns.FactoryMethodAndAbstractFactoryPattern;
using DesignPatterns.PrototypeDesignPattern;
using DesignPatterns.SOLIDDesignPrinciple;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RunPrototypeWithObjectSerialization();
            Console.ReadLine();
        }



        #region SOLID Runner

        static void RunSingleResponsibility()
        {
            Journal journal = new Journal();
            journal.AddENtry("Text 1");
            journal.AddENtry("Text 2");

            Persistence persistence = new Persistence();

            string fileName = @"c:\temp\journal.txt";
            persistence.Save(journal, fileName, true);
            //Process.Start(fileName);
            Console.WriteLine(journal);



        }

        static void RunOpenClosed()
        {
            IEnumerable<Product> products = new List<Product>
            {
                new Product("Apple", Color.Green, Size.Small),
                new Product("Tree", Color.Green, Size.Large),
                new Product("House", Color.Blue, Size.Large),

            };


            ProductFilter productFilter = new ProductFilter();
            IEnumerable<Product> filteredProducts = Enumerable.Empty<Product>();

            filteredProducts = productFilter.FilterByColor(products,Color.Green);
            Console.WriteLine("Green Products (old):");
            foreach (var product in filteredProducts)
            {
                Console.WriteLine($"- {product.Name} is {Color.Green.ToString()}");

            }

            filteredProducts = Enumerable.Empty<Product>();

            BetterFilter betterFilter = new BetterFilter();
            ColorSpecification colorSpecification = new ColorSpecification(Color.Green);
            Console.WriteLine("Green Products (new):");
            filteredProducts = betterFilter.Filter(products, colorSpecification);
            foreach (var product in filteredProducts)
            {
                Console.WriteLine($"- {product.Name} is {Color.Green.ToString()}");

            }


            filteredProducts = Enumerable.Empty<Product>(); 
            SizeSpecification sizeSpecification = new SizeSpecification(Size.Large);      
            Console.WriteLine("Large Products (new):");                           
            filteredProducts = betterFilter.Filter(products, sizeSpecification);
            foreach (var product in filteredProducts)
            {
                Console.WriteLine($"- {product.Name} is {Size.Large.ToString()}");

            }

            filteredProducts = Enumerable.Empty<Product>();
            SizeAndColorSpecification<Product> sizeAndCOlorSpecification = new SizeAndColorSpecification<Product>(colorSpecification,sizeSpecification);
            Console.WriteLine("Large Products (new):");
            filteredProducts = betterFilter.Filter(products, sizeAndCOlorSpecification);
            foreach (var product in filteredProducts)
            {
                Console.WriteLine($"- {product.Name} is {Size.Large.ToString()}");

            }

        }

        static void RunLiskovPrinciple()
        {
            Func<Rectangle, int> Area = (rectangle) => rectangle.Width * rectangle.Height;

            Rectangle rectangle = new Rectangle(2, 3);
            Console.WriteLine($"{rectangle} has area   {Area(rectangle)} ");

            Rectangle square = new Square();
            square.Width = 4;
            Console.WriteLine($"{square} has area   {Area(square)} ");
        }

        static void RunDependecyInversionPrinciple()
        {
            SOLIDDesignPrinciple.Person parent = new SOLIDDesignPrinciple.Person { Name = "John" };
            SOLIDDesignPrinciple.Person child1 = new SOLIDDesignPrinciple.Person { Name = "Chris" };
            SOLIDDesignPrinciple.Person child2 = new SOLIDDesignPrinciple.Person { Name = "Mary" };

            Relationships relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);

        }
        #endregion



        #region BuilderPaternRunner
        static void   RunBuilderAntiPattern()
        {
            BuilderAntiPattern builder = new BuilderAntiPattern();
            builder.BuildTag();
        }
        #endregion


        static void RunBuilder()
        {
            HtmlBuilder builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello").AddChild("li", "world");
            Console.WriteLine(builder.ToString());

        }

        static void RunFacetedBuilder()
        {
            PersonBuilder builder = new PersonBuilder();
            BuilderPattern.Person person = builder.Works.At("M-Kopa")
                         .AsA("Software Engineer")
                         .Earning(20000)
                    .Lives.At("Enugu").InCity("Enugu").WithPostalCode("");
            Console.WriteLine(person);
        }

        static void RunFactory()
        {
            Point point = Point.NewPolarPoint(1, 2);
            
            Console.WriteLine(point);
        }

        static void RunInnerFactory()
        {
            Point point = Point.Factory.NewCartersianPoint(1, 2);
            point.ToString();
           
            Console.WriteLine(point);
        }

        static void RunAbstractFactory()
        {
            HotDrinkMachine machine = new HotDrinkMachine();
            IHotDrink drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);

            drink.Consume();
        
        }

        static void RunAbstractFactoryWithOCP()
        {
            HotDrinkMachineOCP machine = new HotDrinkMachineOCP();
            IHotDrink hotDrink = machine.MakeDrink();
            hotDrink.Consume();
        }

        static void RunPrototypeWithObjectSerialization()
        {
            var lucy = new PrototypePersonDoneRight
            {
                Names = new[] { "lucy", "jacobs" },
                Address = new PrototypeAddressDoneRight
                {
                    StreetName = "Olusalo close",
                    HouseNumber = 123
                }
            };

            var jane = lucy.DeepCopyXml();
            jane.Address.HouseNumber = 1996;

            Console.WriteLine(lucy);
            Console.WriteLine(jane);

        }



    }
}
