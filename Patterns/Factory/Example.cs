namespace Patterns.Factory;

public class Person
{
    public Person(int _Id, string _Name)
    {
        Id = _Id;
        Name = _Name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}

public class PersonFactory
{
    public int CurrentMaxId { get; set; }

    public PersonFactory()
    {
        CurrentMaxId = 0;
    }

    public Person CreatePerson(string Name)
    {
        return new Person(this.CurrentMaxId++, Name);
    }

}