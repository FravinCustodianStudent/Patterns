using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Patterns.Prototype;

public static class ExtensionMethods
{
    public static T DeepCopy<T>(this T self)
    {
        using (var stream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            return (T) copy;
        }
    }

    public static T DeepCopyXml<T>(this T self)
    {
        using (var ms = new MemoryStream())
        {
            XmlSerializer s = new XmlSerializer(typeof(T));
            s.Serialize(ms, self);
            ms.Position = 0;
            return (T) s.Deserialize(ms);
        }
    }
}

class MyClass
{
    
}

//[Serializable] // this is, unfortunately, required
public class Foo
{
    public uint Stuff;
    public string Whatever;
    
    public override string ToString()
    {
        return $"{nameof(Stuff)}: {Stuff}, {nameof(Whatever)}: {Whatever}";
    }
}