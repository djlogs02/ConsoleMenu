﻿using System;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu subMenu1 = new Menu("SubMenu1");
            subMenu1.AddAction("Option1", () => { Console.Out.WriteLine("Option1 Selected"); });

            Menu subMenu2 = new Menu("SubMenu2");
            subMenu2.AddAction("Option A", () => { Console.Out.WriteLine("Option A Selected"); });

            Menu mainMenu = new Menu("Main");
            mainMenu.AddAction("SubMenu1", () => { subMenu1.Prompt(mainMenu); });
            mainMenu.AddAction("SubMenu2", () => { subMenu2.Prompt(mainMenu); });

            mainMenu.Prompt();
        }
    }
}
