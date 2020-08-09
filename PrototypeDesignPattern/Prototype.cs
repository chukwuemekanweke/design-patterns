using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace DesignPatterns.PrototypeDesignPattern
{

    #region AntiPattern
    public class NonPrototypePerson :ICloneable
    {
        public string[] Names;
        public NonPrototypeAddress Address;

        public object Clone()
        {
            // only bad things can happen with this
            return new NonPrototypePerson
            {
                Address = (NonPrototypeAddress)Address.Clone(),
                Names = Names
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class NonPrototypeAddress : ICloneable
    {
        public string StreetName;
        public int HouseNumber;

        public object Clone()
        {
            return new NonPrototypeAddress
            {
                HouseNumber = HouseNumber,
                StreetName = StreetName
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
    #endregion



    #region Prototype Pattern  using a copy constructor
    public class PrototypePerson 
    {
        public string[] Names;
        public PrototypeAddress Address;

        
        public PrototypePerson (PrototypePerson person)
        {                     
            Names = person.Names;
            Address = new PrototypeAddress(person.Address);            
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class PrototypeAddress 
    {
        public string StreetName;
        public int HouseNumber;

        public  PrototypeAddress(PrototypeAddress address)
        {     
            HouseNumber = address.HouseNumber;
            StreetName = address.StreetName;            
        }

       
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
    #endregion




    #region Prototype Pattern  using an explicit deep copy interface

    public interface IPrototype<T>
    {
        T DeepCopy();
    }


    public class PrototypePersonWithDeepCopy :IPrototype<PrototypePersonWithDeepCopy>
    {
        public string[] Names;
        public PrototypeAddress Address;

        public PrototypePersonWithDeepCopy()
        {

        }

        public PrototypePersonWithDeepCopy DeepCopy()
        {
            return new PrototypePersonWithDeepCopy
            {
                Address = Address,
                Names = Names
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class PrototypeAddressWithDeepCopy:IPrototype<PrototypeAddressWithDeepCopy>
    {
        public string StreetName;
        public int HouseNumber;

        public PrototypeAddressWithDeepCopy()
        {

        }

        public PrototypeAddressWithDeepCopy DeepCopy()
        {
            return new PrototypeAddressWithDeepCopy
            {
                HouseNumber = HouseNumber,
                StreetName = StreetName
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
    #endregion




    #region Prototype Pattern Done The Right Way With Object Serialization

    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream,self);
                stream.Seek(0, SeekOrigin.Begin);
                object copy = formatter.Deserialize(stream);
                stream.Close();
                return (T) copy;
            }
        }

        public static T DeepCopyXml<T>(this T self)
        {
           using(var stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, self);
                stream.Position = 0;
                return (T) serializer.Deserialize(stream);
            }
        }
    }

    /// <summary>
    /// Using the binary formatter for serialization requires that the object have that data annotation. Not necessary when using an xml serializer
    /// </summary>
    //[Serializable]
    public class PrototypePersonDoneRight
    {
        public string[] Names;
        public PrototypeAddressDoneRight Address;

        public PrototypePersonDoneRight()
        {

        }

     
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    /// <summary>
    /// Using the binary formatter for serialization requires that the object have that data annotation. Not necessary when using an xml serializer
    /// </summary>

    //[Serializable]

    public class PrototypeAddressDoneRight
    {
        public string StreetName;
        public int HouseNumber;

        public PrototypeAddressDoneRight()
        {

        }

      

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
    #endregion
}
