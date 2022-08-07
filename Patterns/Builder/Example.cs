using System.Text;

namespace Patterns.Builder;

public class CodeClass{
    public string Name;
    public List<Property> _properties = new List<Property>();

    public class Property{
        public string _type;
        public string _name;

        public Property( string Name,string type)
        {
            _type = type;
            _name = Name;
        }
    }
}
public class CodeBuilder
{
    protected CodeClass root = new CodeClass();

    public CodeBuilder(string CodeClassName)
    {
        root.Name = CodeClassName;
    }

    public CodeBuilder AddField(string name, string type)
    {
        var prop = new CodeClass.Property(name, type);
        root._properties.Add(prop);
        return this;
    }

    public override string ToString()
    {
        StringBuilder str = new StringBuilder();
        str.Append($"public class {root.Name}\n")
            .Append("{\n");
        foreach (var prop in root._properties)
        {
            str.Append($"pubic {prop._type} {prop._name};\n");
        }

        str.Append("}");
        return str.ToString();
    }
}