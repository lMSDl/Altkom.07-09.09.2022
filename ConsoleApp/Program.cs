
namespace ConsoleApp
{
    internal class Program
    {
        static int SomeInt { get; set; }
        static int? SomeNullableInt { get; set; }
        static Program SomeProgram { get; set; } = new Program();
        static Program? SomeNullProgram { get; set; }

        static void Main(string[] args)
        {


            Console.WriteLine("Hello, World!");


            SomeInt = 0;

            SomeNullableInt = null;

            SomeProgram = new Program();

            SomeNullProgram = null;

            SomeProgram = GetProgram()!;

            SomeProgram = SomeNullProgram;
            SomeNullProgram = SomeProgram;



        }

        static Program? GetProgram()
        {
            return new Program();
        }
    }
}