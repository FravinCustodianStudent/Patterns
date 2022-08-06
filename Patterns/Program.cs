// See https://aka.ms/new-console-template for more information

using System.Text;
using Patterns;

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