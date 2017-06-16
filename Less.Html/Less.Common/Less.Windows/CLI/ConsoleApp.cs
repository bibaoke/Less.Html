//bibaoke.com

using System;
using System.Collections.Generic;
using Less.Text;
using Less.Collection;

namespace Less.Windows
{
    /// <summary>
    /// 控制台应用
    /// </summary>
    public static class ConsoleApp
    {
        /// <summary>
        /// 功能集合
        /// </summary>
        public static Dictionary<string, Function> Funtions
        {
            get;
            private set;
        }

        static ConsoleApp()
        {
            ConsoleApp.Funtions = new Dictionary<string, Function>();

            ConsoleApp.AddFuntion(new Exit());
        }

        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            while (true)
            {
                string command = Console.ReadLine();

                string[] array = command.SplitByWhiteSpace();

                if (array.Length > 0)
                {
                    Function function;

                    if (ConsoleApp.Funtions.TryGetValue(array[0].ToLower(), out function))
                    {
                        if (!function.Execute(array.SubArray(1)))
                            break;
                    }
                    else
                    {
                        Console.WriteLine("输入错误");
                    }
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// 添加功能
        /// </summary>
        /// <param name="function"></param>
        public static void AddFuntion(Function function)
        {
            ConsoleApp.Funtions.Add(function.Name, function);
        }
    }
}
