using System;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu subMenu1 = new Menu("SubMenu1");
            subMenu1.AddAction("Whats My Name", () => { Console.Out.WriteLine("Aaron is My name"); });

            Menu subMenu2 = new Menu("SubMenu2");
            subMenu2.AddAction("Option A", () => { Console.Out.WriteLine("Derrick is My name"); });

            Menu mainMenu = new Menu("Main");
            mainMenu.AddAction("Aaron", () => { subMenu1.Prompt(mainMenu); });
            mainMenu.AddAction("Derrick", () => { subMenu2.Prompt(mainMenu); });

            mainMenu.Prompt();
        }
    }
}
