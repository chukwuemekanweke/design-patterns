using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.StructuralDesignPatterns.FlyWeightDesignPattern
{
    class FlyWeightDesignPattern
    {
    }

    public class User
    {
        private string fullName;

        public User(string fullName)
        {
            this.fullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        }



    }



    #region Flyweight Pattern
    public class User2
    {
        static List<string> strings = new List<string>();
        private int[] names;

        public User2(string fullName)
        {

            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }

            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();

            //This is the alternative approach
            //names = fullName.Split(' ').Select(x=>getOrAdd(x)).ToArray();

        }

        public string FulName => string.Join(" ", names.Select(i => strings[i]).ToArray());

    }
    #endregion


    public class FormattedText
    {
        private readonly string plainText;
        private bool[] capitalize;
        public FormattedText(string _plainText)
        {
            plainText = _plainText;
            capitalize = new bool[plainText.Length];
        }

        public void Capitalize(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                capitalize[i] = true;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < plainText.Length; i++)
            {
                char character = plainText[i];
                stringBuilder.Append(capitalize[i] ? char.ToUpper(character) : character);
            }
            return stringBuilder.ToString();
        }



    }

    #region Flyweight Pattern

    public class BetterFormattedText
    {
        private string plainText;
        private List<TextRange> formattingRange = new List<TextRange>();

        public BetterFormattedText(string _plainText)
        {
            plainText = _plainText;
        }

        public TextRange GetRange(int start, int end)
        {
            TextRange textRange = new TextRange
            {
                Start = start,
                End = end
            };
            formattingRange.Add(textRange);
            return textRange;
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < plainText.Length; i++)
            {
                char character = plainText[i];

                foreach (var range in formattingRange)
                {
                    if (range.Covers(i) && range.Capitalize)
                        character = char.ToUpper(character);
                }

                stringBuilder.Append(character);
            }
            return stringBuilder.ToString();
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;

            public bool Covers(int position)
            {
                return position >= Start && position <= End;
            }
        }

    }
    #endregion



    [TestFixture]
    public class Demo
    {
        //static void Main(string[] args)
        //{

        //}

        [Test]
        public void TestUser()
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User>();
            foreach (var firstName in firstNames)
            {
                foreach (var lastName in lastNames)
                {
                    users.Add(new User($"{firstName} {lastName}"));
                }
            }

            ForceGC();

            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });
        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        private object RandomString()
        {
            Random rand = new Random();
            return new string(Enumerable.Range(0, 10).Select(i => (char)('a' + rand.Next(26))).ToArray());


        }
    }



}
