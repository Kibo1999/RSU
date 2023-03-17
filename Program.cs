// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Random rand = new Random();

for (int i = 0; i < 10; i++)
{
    int int1 = rand.Next();
    int int2 = rand.Next();

    if (int1 > int2)
    {
        Console.WriteLine($"int1 maior! {int1}");
    }
    else
    {
        Console.WriteLine($"int2 maior! {int2}");
    }

    Console.WriteLine($"int1: {int1} int2: {int2}");

    Console.WriteLine("");

}