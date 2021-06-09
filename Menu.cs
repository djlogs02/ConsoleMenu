using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public class Menu
    {
        private List<Tuple<string, Action>> _actionItems;
        public string MenuDescription { get; set; }
        public Menu(string menuDescription)
        {
            MenuDescription = menuDescription;
            _actionItems = new List<Tuple<string, Action>>();
        }
        public void AddAction(string actionName, Action action)
        {
            _actionItems.Add(new Tuple<string, Action>(actionName, action));
        }
        private void DisplayMenu(List<Tuple<string, Action>> actionItems)
        {
            for (int i = 0; i < actionItems.Count; i++)
            {
                Console.Out.WriteLine(GetMenuLine(i, actionItems[i].Item1));
            }
        }
        private void RunMenuItem(int itemIndex, List<Tuple<string, Action>> actionItems)
        {
            Console.Clear();
            Console.Out.WriteLine(string.Format("Beginning running: {0}", actionItems[itemIndex].Item1));
            actionItems[itemIndex].Item2();
            RunCompleteMessage(actionItems[itemIndex].Item1);
        }
        private void RunCompleteMessage(string actionName)
        {
            Console.Out.WriteLine(string.Format("Finished running: {0}", actionName));
        }
        private static string GetMenuLine(int actionIndex, string actionName)
        {
            return string.Format("{0}. {1}", actionIndex, actionName);
        }
        private List<Tuple<string, Action>> GetSystemActions(Menu previousMenu)
        {
            var systemActions = new List<Tuple<string, Action>>();
            if (previousMenu != null)
            {
                systemActions.Add(new Tuple<string, Action>("Previous Menu", () =>
                {
                    previousMenu.Prompt();
                }));
            }
            systemActions.Add(new Tuple<string, Action>("Clear", () =>
            {
                Console.Clear();
            }));
            systemActions.Add(new Tuple<string, Action>("Exit", () =>
            {
                Environment.Exit(0);
            }));
            return systemActions;
        }
        public void Prompt(Menu previousMenu = null)
        {
            var allActionItems = new List<Tuple<string, Action>>();
            allActionItems.AddRange(_actionItems);
            allActionItems.AddRange(GetSystemActions(previousMenu));
            while (true)
            {
                Console.Out.WriteLine(string.Format("{0} Menu", MenuDescription));
                int menuSelection = -1;
                DisplayMenu(allActionItems);
                var userInput = Console.In.ReadLine();
                if (IsValidInput(userInput, out menuSelection, allActionItems.Count))
                {
                    RunMenuItem(menuSelection, allActionItems);
                }
                else
                {
                    Console.Out.WriteLine("Invalid Selection: Try Again");
                }
            }
        }
        private static bool IsValidInput(string userInput, out int menuSelection, int numberOfItems)
        {
            return (Int32.TryParse(userInput, out menuSelection) && menuSelection >= 0 && menuSelection < numberOfItems);
        }
        public static string SelectFromList(string[] values)
        {
            while (true)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    Console.Out.WriteLine(GetMenuLine(i, values[i]));
                }
                var userInput = Console.In.ReadLine();
                int menuSelection = -1;
                if (IsValidInput(userInput, out menuSelection, values.Length))
                {
                    return values[menuSelection];
                }
            }
        }
        public static string PromptForValue(string prompt)
        {
            Console.Out.WriteLine(prompt);
            return Console.In.ReadLine();
        }
        public static long PromptForLongValue(string prompt, string invalidInputMessage, Action failAction = null)
        {
            long longValue = -1;
            while (true)
            {
                string result = PromptForValue(prompt);
                if (long.TryParse(result, out longValue) && longValue > 0)
                {
                    return longValue;
                }
                else
                {
                    Console.Out.WriteLine(invalidInputMessage);
                    if (failAction != null)
                    {
                        failAction();
                    }
                }
            }
        }
        public static int PromptForIntValue(string prompt, string invalidInputMessage)
        {
            int intValue = -1;
            while (true)
            {
                string result = PromptForValue(prompt);
                if (int.TryParse(result, out intValue) && intValue > 0)
                {
                    return intValue;
                }
                else
                {
                    Console.Out.WriteLine(invalidInputMessage);
                }
            }
        }
    }
}