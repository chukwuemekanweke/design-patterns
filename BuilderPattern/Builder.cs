using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BuilderPattern
{
    /// <summary>
    /// When piecewise object constructon is complicated, provide an API for doing it succinctly
    /// </summary>
    class Builder
    {
    }


    public class HtmlBuilder
    {
        private readonly string  _rootName;
        private HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            _rootName = rootName;
            root.Name = rootName;
        }



        /// <summary>
        /// this implmentation uses a fluent interface
        /// A fluent interface allows yu to chain several calls by returning a reference to the object you are working with
        /// </summary>
        /// <param name="childName"></param>
        /// <param name="childText"></param>
        /// <returns></returns>
        public HtmlBuilder AddChild(string childName, string childText)
        {
            HtmlElement element = new HtmlElement(childName, childText);
            root.Elements.Add(element);
            return this;
        }


        //public void AddChild(string childName, string childText)
        //{
        //    HtmlElement element = new HtmlElement(childName, childText);
        //    root.Elements.Add(element);
        //}

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement();
            root.Name = _rootName;
        }
    }

    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();

        private const int indentSize = 2;

        public HtmlElement()
        { }
        

        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }


        private string ToStringImpl(int indent)
        {
            StringBuilder builder = new StringBuilder();
            string indentSymbol = new string(' ', indentSize * indent);

            builder.AppendLine($"{indentSymbol}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                string textIndentSymbol = new string(' ', indentSize * (indent+1));  
                builder.AppendLine($"{textIndentSymbol}<{Text}>");
            }

            foreach (var childElement in Elements)
            {
                builder.AppendLine($"{childElement.ToStringImpl(indent+1)}");

            }

            builder.AppendLine($"{indentSymbol}<{Name}>");
            return builder.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

    }



    #region AntiPattern
    public class BuilderAntiPattern
    {
        public BuilderAntiPattern()
        {

        }


        public void BuildTag()
        {
            string hello = "hello";
            StringBuilder builder = new StringBuilder();
            builder.Append("<p>");
            builder.Append(hello);
            builder.Append("<p>");

            Console.WriteLine(builder);



            builder.Clear();
            string[] words = new[] { "hello", "world" };
            builder.Append("<ul>");
            foreach (string word in words)
            {
                builder.AppendFormat($"<li>{word}</li>");
            }

            builder.Append("</ul>");
            Console.WriteLine(builder);

        }
    }
    #endregion
}
