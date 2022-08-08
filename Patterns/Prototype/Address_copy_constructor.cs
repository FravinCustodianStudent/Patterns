namespace Patterns.Prototype;

public class Address
{
    public string StreetAddress, City, Country;

    public Address(string streetAddress, string city, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        Country = country;
    }

    public Address(Address other)
    {
        StreetAddress = other.StreetAddress;
        City = other.City;
        Country = other.Country;
    }

    public override string ToString()
    {
        return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(City)}: {City}, {nameof(Country)}: {Country}";
    }
}

public class Person
{
    public string Name;
    public Address Address;

    public Person(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public Person(Person other)
    {
        Name = other.Name;
        Address = new Address(other.Address);
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
    }
}
