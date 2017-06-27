using Less.Windows;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleApp.AddFuntion(new Test1());
            ConsoleApp.AddFuntion(new Test2());
            ConsoleApp.AddFuntion(new Test3());

            ConsoleApp.Start();
        }
    }
}
