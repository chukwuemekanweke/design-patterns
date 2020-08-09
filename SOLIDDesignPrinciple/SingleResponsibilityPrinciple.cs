using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesignPatterns.SOLIDDesignPrinciple
{
    public class SingleResponsibilityPrinciple
    {
    }


    /// <summary>
    /// Single Responsibility: A typical class is responsible for one thing, and has only one reason to change
    /// </summary>

    public class Journal
    {
        private readonly List<string> _entries;
        private static int count = 0;
        public Journal()
        {
            _entries = new List<string>();
        }

        public int AddENtry(string text)
        {
            _entries.Add($"{+count}: {text}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            _entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _entries); 
        }      
        
       


    }  


    public class Persistence
    {
        public void Save(Journal journal,  string fileName, bool overwrite=false)
        {
            if(overwrite || !File.Exists(fileName))
                 File.WriteAllText(fileName, journal.ToString());
        }

        public static Journal Load(string fileName)
        {
            //todo: implement this
            return null;
        }
    }

}

