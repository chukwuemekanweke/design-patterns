using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.SOLIDDesignPrinciple
{
    /// <summary>
    /// Don't put too much into an interface; split into sepreate interfaces. 
    ///   Also known as    YAGNI - You Ain't Going to Need It
    /// </summary>
    public class InterfaceSegregationPrinciple
    {
    }


    public class Document
    {

    }

    #region Anti Pattern

    /// <summary>
    ///  FOr a multifunction printer, yes you will find implementations for the method signatures specified in the interface, however for
    ///   Old fashioned printers, all they do is print. the fax and scan interface methods are useless to them
    /// </summary>
    public interface IMachine
    {


        void Print(Document document);
        void Scan(Document document);

        void Fax(Document document);

    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document document)
        {
            throw new NotImplementedException();
        }

        public void Print(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    public class OldFashionedPrinter : IMachine
    {
        public void Fax(Document document)
        {
            throw new NotImplementedException();
        }

        public void Print(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    #endregion



    public interface IPrinter
    {
        void Print(Document document);

    }

    public interface IScanner
    {
        void Scan(Document document);

    }

    public interface IMultiFunctionDevice : IScanner, IPrinter
    {

    }

    public class BetterMultiFunctionPrinter : IPrinter, IScanner, IFaxMachine
    {
        public void Fax(Document document)
        {
            throw new NotImplementedException();
        }

        public void Print(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiFunctinMachine : IMultiFunctionDevice
    {

        private IPrinter _printer;
        private IScanner _scanner;

        public MultiFunctinMachine(IPrinter printer, IScanner scanner)
        {
            _printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
            _scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(printer));

        }
        public void Print(Document document)
        {
            _printer.Print(document);
        }

        public void Scan(Document document)
        {
            //An example of decorator pattern
            _scanner.Scan(document);
        }
    }

    public interface IFaxMachine
    {
        void Fax(Document document);

    }


   

}
