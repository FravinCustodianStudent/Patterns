// See https://aka.ms/new-console-template for more information

using Patterns;
using Patterns.Builder;
using Patterns.Builder.Email_BuilderParametr;
using Patterns.Factory.Class;
using Point = Patterns.Factory.Point;

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

void FabricsExamples()
{
    var p = Point.NewCartesianPoint(1, 3);
    var p2 = PointFabric.NewCartesianPoint(1, 3);
}
FabricsExamples();