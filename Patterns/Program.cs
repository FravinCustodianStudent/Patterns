// See https://aka.ms/new-console-template for more information

using MoreLinq.Extensions;
using Patterns;
using Patterns.Adapter;
using Patterns.Bridge;
using Patterns.Builder;
using Patterns.Builder.Email_BuilderParametr;
using Patterns.Factory.Class;
using Patterns.Prototype.Factory;
using Circle = Patterns.Factory.Circle;
using Person = Patterns.Person;
using Point = Patterns.Adapter.Point;

void BuilderExamples(){
    var builder = HtmlElement.Create("ul")
        .AddChildFluent("li","pizdez")
        .AddChildFluent("Yob","PIZDEZ2")
        .Build();
    Console.WriteLine(builder);
    
    var pb = new PersonBuilder();
    Person person = pb
        .Lives
        .At("Losharovo 123 road")
        .In("London")
        .WithPostcode("SW12BC")
        .Works
        .At("fabricam")
        .AsA("engineer")
        .Earning(12300);
    Console.WriteLine(person);
//Builder param
    var ms = new MailService();
    ms.SendEmail(builder => 
        builder.From("abc@xyz.com")
            .To("qwer@zxc.com")
            .Body("Hello")
    );

    var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
    Console.WriteLine(cb.ToString());
}

// void FabricsExamples()
// {
//     var p = Point.NewCartesianPoint(1, 3);
//     var p2 = PointFabric.NewCartesianPoint(1, 3);
//
//     var f = new PersonFactory();
//     var u = f.CreatePerson("Pidor");
//     Console.WriteLine(f.CurrentMaxId+ " " + u.Id);
//
// }
//FabricsExamples();
// void PrototypeExamples()
// {
//     var main = new Employee(null, new Address("123 East Dr", "London", 0));
//     var aux = new Employee(null, new Address("123B East Dr", "London", 0));
//       
//       
//     var john = new Employee("John", new Address("123 London Road", "London", 123));
//
//     //var chris = john;
//     var jane = new Employee(john);
//
//     jane.Name = "Jane";
//       
//     Console.WriteLine(john); // oop s, john is called chris
//     Console.WriteLine(jane);
// }
// PrototypeExamples();

void AdapterExamples()
{
    
 List<VectorObject> vectorObjects 
    = new List<VectorObject>
    {
        new VectorRectangle(1, 1, 10, 10),
        new VectorRectangle(3, 3, 6, 6)
    };

// the interface we have
void DrawPoint(Point p)
{
    Console.Write(".");
}


  List<Point> points = new List<Point>();
bool prepared = false;
void Prepare()
{
    if (prepared) return;
    foreach (var vo in vectorObjects)
    {
        foreach (var line in vo)
        {
            var adapter = new LineToPointAdapter(line);
            adapter.ForEach(p => points.Add(p));
        }
    }
    prepared = true;
}

void DrawPointsLazy()
{
    Prepare();
    points.ForEach(DrawPoint);
}

void DrawPoints()
{
    foreach (var vo in vectorObjects)
    {
        foreach (var line in vo)
        {
            var adapter = new LineToPointAdapter(line);
            adapter.ForEach(DrawPoint);
        }
    }
}
DrawPoints();
DrawPoints();
}
