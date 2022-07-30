// See https://aka.ms/new-console-template for more information

using System.Text;
using Patterns;

var builder = HtmlElement.Create("ul")
    .AddChildFluent("li","pizdez")
    .AddChildFluent("Yob","PIZDEZ2")
    .Build();
    