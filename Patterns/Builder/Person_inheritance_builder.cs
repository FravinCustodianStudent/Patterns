namespace HeritanceBuilder;

public class Person_inheritance_builder
{
    public string Name;

    public string Position;

    public DateTime DateOfBirth;
    
    public class Builder : PersonBirthDateBuilder<Builder>
    {
        internal Builder() {}
    }
    
    public static Builder New => new Builder();

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public abstract class PersonBuilder
{
    protected Person_inheritance_builder person = new Person_inheritance_builder();

    public Person_inheritance_builder Build()
    {
        return person;
    }
}

public class PersonInfoBuilder<SELF> : PersonBuilder
    where SELF : PersonInfoBuilder<SELF>
{
    public SELF Called(string name)
    {
        person.Name = name;
        return (SELF) this;
    }
}

public class PersonJobBuilder<SELF> 
    : PersonInfoBuilder</*PersonJobBuilder<SELF>*/SELF>
    where SELF : PersonJobBuilder<SELF>
{
    public SELF WorksAsA(string position)
    {
        person.Position = position;
        return (SELF) this;
    }
}

// here's another inheritance level
// note there's no PersonInfoBuilder<PersonJobBuilder<PersonBirthDateBuilder<SELF>>>!

public class PersonBirthDateBuilder<SELF> 
    : PersonJobBuilder<SELF>
    where SELF : PersonBirthDateBuilder<SELF>
{
    public SELF Born(DateTime dateOfBirth)
    {
        person.DateOfBirth = dateOfBirth;
        return (SELF)this;
    }
}