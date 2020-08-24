using DesignPatterns.BuilderPattern;
using DesignPatterns.FactoryMethodAndAbstractFactoryPattern;
using DesignPatterns.PrototypeDesignPattern;
using DesignPatterns.Singleton;
using DesignPatterns.SOLIDDesignPrinciple;
using DesignPatterns.StructuralDesignPatterns.BridgePattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static DesignPatterns.StructuralDesignPatterns.CompositeDesignPattern.CompositeDesignPattern;
using SquareComposite = DesignPatterns.StructuralDesignPatterns.CompositeDesignPattern.Square;
using CircleComposite = DesignPatterns.StructuralDesignPatterns.CompositeDesignPattern.Circle;
using CORCreature = DesignPatterns.BehaviouralPatterns.ChainOfResponsibilityPattern.Creature;
using CORCreatureModifier = DesignPatterns.BehaviouralPatterns.ChainOfResponsibilityPattern.CreatureModifier;
using CORDoubleAttackModifier = DesignPatterns.BehaviouralPatterns.ChainOfResponsibilityPattern.DoubleAttackModifier;
using CORIncreaseDefenseModifier = DesignPatterns.BehaviouralPatterns.ChainOfResponsibilityPattern.IncreaseDefenseModifier;
using CORNoBonusesModifier = DesignPatterns.BehaviouralPatterns.ChainOfResponsibilityPattern.NoBonusesModifier;
using ChainOfResponsibility = DesignPatterns.BehaviouralPatterns.ChainOfResponsibilityPattern;
using DesignPatterns.StructuralDesignPatterns.CompositeDesignPattern;
using DesignPatterns.StructuralDesignPatterns.DecoratorPattern;
using DesignPatterns.StructuralDesignPatterns.FacadePattern;
using DesignPatterns.StructuralDesignPatterns.FlyWeightDesignPattern;
using DesignPatterns.StructuralDesignPatterns.ProxyPattern;
using DesignPatterns.BehaviouralPattern.CommandPattern;
using Action = DesignPatterns.BehaviouralPattern.CommandPattern.Action;
using DesignPatterns.BehaviouralPattern.InterpreterPattern;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RunInterpreterPattern();
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

            Rectangle square = new SOLIDDesignPrinciple.Square();
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

        static void RunSingleton()
        {
            SingletonDatabase database = SingletonDatabase.Instance;
            string city = "Tokyo";
            Console.WriteLine($"{city} has population {database.GetPopulation(city)}"); 
        }

        static void RunMonoState()
        {

            CEO ceo1 = new CEO
            {
              Age = 24,
              Name = "Emeka Nweke"
            };

            CEO ceo2 = new CEO
            {
                Age = 26,
                Name = "Onyeka Nweke"
            };


            Console.WriteLine($"{nameof(ceo1)}: {ceo1}");
            Console.WriteLine($"{nameof(ceo2)}: {ceo2}");
        }


        static void RunBridgePattern()
        {
            /*
            
                    the IRenderer implementation would be better passed in with dependency injection

             */
            IRenderer renderer = new VectorRenderer();
            StructuralDesignPatterns.BridgePattern.Circle circle = new StructuralDesignPatterns.BridgePattern.Circle(renderer,5);
            circle.Draw();
            circle.Resize(2);
            circle.Draw();
        }


        static void RunCompositeDesignPattern ()
        {
            GraphicObject drawing = new GraphicObject { Name = "My Drawing" };
            drawing.Children.Add(new SquareComposite { Color = "Red" });
            drawing.Children.Add(new CircleComposite { Color = "Yellow" });

            GraphicObject group = new GraphicObject ();
            group.Children.Add(new SquareComposite { Color = "Blue" });
            group.Children.Add(new CircleComposite { Color = "Brown" });

            drawing.Children.Add(group);

            Console.WriteLine(drawing);
        }

        static void RunCompositeDesignPattern2()
        {
            Neuron neuron1 = new Neuron();
            Neuron neuron2 = new Neuron();

            neuron1.ConnectTo(neuron2);

            NeuronLayer neuronLayer1 = new NeuronLayer();
            NeuronLayer neuronLayer2 = new NeuronLayer();

            neuronLayer1.ConnectTo(neuronLayer2);
         }

        static void RunDecoratorPattern()
        {
            MyStringBuilder builder = "Hello ";
            builder += "World";

            Console.WriteLine(builder);



        }

        static void RunCompositeDecoratorPattern()
        {
            StructuralDesignPatterns.DecoratorPattern.Square square = new StructuralDesignPatterns.DecoratorPattern.Square(1.23f);
            Console.WriteLine(square.AsString());

            ColoredShape redSquare = new ColoredShape(square, "red");
            Console.WriteLine(redSquare.AsString());

            TransperentShape transperentShape = new TransperentShape(redSquare, 0.2f);
             Console.WriteLine(transperentShape.AsString());

        }

        static void RunFacade()
        {
            HotelKeeper keeper = new HotelKeeper();

            VegMenu v = keeper.getVegMenu();
            NonVegMenu nv = keeper.getNonVegMenu();
            Both both = keeper.getVegNonMenu();
        }

        static void RunFlyWeightPattern()
        {
            FormattedText text = new FormattedText("All things pass, All things must decay");
            text.Capitalize(10, 15);
            Console.WriteLine(text);

            BetterFormattedText text2 = new BetterFormattedText("All things pass, All things must decay");
            text2.GetRange(10, 15).Capitalize = true;
            Console.WriteLine(text2);
        }

        static void RunProtectionProxy()
        {
            ICar car = new CarProxy(new Driver(12));   
            car.Drive();

            car = new CarProxy(new Driver(17));
            car.Drive();


        }

        static void RunPropertyProxy()
        {
            Creature creature = new Creature();
            creature.Agility = 10;  // c.Agility = new Property<int>(10)
                                    // If the private field acessor wasn't used

            creature.Agility = 10;

        }


        static void RunChainOfResponsibility()
        {
            CORCreature goblin = new CORCreature("goblin",2,2);
            Console.WriteLine(goblin);


            CORCreatureModifier root = new CORCreatureModifier(goblin);

            CORNoBonusesModifier noBonusesModifier = new CORNoBonusesModifier(goblin);
            root.Add(noBonusesModifier);

            CORDoubleAttackModifier doubleAttackModifier = new CORDoubleAttackModifier(goblin);



            root.Add(doubleAttackModifier);

            CORIncreaseDefenseModifier increaseDefenseModifier = new CORIncreaseDefenseModifier(goblin);
            root.Add(increaseDefenseModifier);


            root.Handle();

            Console.WriteLine(goblin);



        }

        static void RunChainOfResponsibilityWithMediator()
        {
            ChainOfResponsibility.Game game = new ChainOfResponsibility.Game();
            ChainOfResponsibility.Creature2 goblin = new ChainOfResponsibility.Creature2(game, "Strong Goblin", 3, 3);
            
            Console.WriteLine(goblin);
             using (new ChainOfResponsibility.DoubleAttackModifier2(game,goblin))
            {
                Console.WriteLine(goblin);
                using(new ChainOfResponsibility.IncreaseDefenseModifier2(game, goblin))
                {
                    Console.WriteLine(goblin);

                }
                Console.WriteLine(goblin);

            }

            Console.WriteLine(goblin);


        }

        static void RunCommandPattern()
        {
            BankAccount bankAccount = new BankAccount();
            List<BankAccountCommand> commands = new List<BankAccountCommand>
            {
                new BankAccountCommand(bankAccount, Action.Deposit,100),
                new BankAccountCommand(bankAccount, Action.Withdraw, 50 ) ,
                new BankAccountCommand(bankAccount, Action.Withdraw, 1000 )

            };

            Console.WriteLine(bankAccount);
            foreach (var command in commands)
            {
                command.Call();
            }
            Console.WriteLine(bankAccount);


            //reason we used Enumerable.Reverse not list.Reverse. is cause the list.Reverse is a mutating operation
            foreach (var command in Enumerable.Reverse(commands))
            {
                command.Undo();

            }
            Console.WriteLine(bankAccount);


        }

        static void RunInterpreterPattern()
        {
            string input = "(13+4)-(12+1)";
            var tokens = Interpreter.Lex(input);
            Console.WriteLine( string.Join("\t", tokens));

           BinaryOperation binaryOperation =   Interpreter.Parse(tokens);
            Console.WriteLine($"{input} = {binaryOperation.Value}");

        }


    }
}
