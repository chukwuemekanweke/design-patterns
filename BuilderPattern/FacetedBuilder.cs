using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BuilderPattern
{
    class FacetedBuilder
    {
    }

    public class Person
    {
        //address
        public string StreetAddress, Postcode, City;


        //employment
        public string CompanyName, Position;

        public decimal AnnualIncome;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    public class PersonBuilder   //facade

    {
        protected Person Person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(Person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(Person);


        public static implicit operator Person(PersonBuilder builder)
        {
            return builder.Person;
        }
    }


    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            Person = person;
        }


        public PersonAddressBuilder At(string address)
        {
            Person.StreetAddress = address;
            return this;
        }

        public PersonAddressBuilder InCity(string city)
        {
            Person.CompanyName = city;
            return this;
        }

        public PersonAddressBuilder WithPostalCode(string postalCode)
        {
            Person.Postcode = postalCode;
            return this;
        }

    }


    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            Person = person;
        }


        public PersonJobBuilder At(string companyName)
        {
            Person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            Person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(decimal amount)
        {
            Person.AnnualIncome = amount;
            return this;
        }

    }



}
