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
        /// 名称列表
        /// </summary>
        private static List<string> NameList
        {
            get;
            set;
        }

        /// <summary>
        /// 功能集合
        /// </summary>
        private static Dictionary<string, Function> Functions
        {
            get;
            set;
        }

        static ConsoleApp()
        {
            ConsoleApp.Functions = new Dictionary<string, Function>();
            ConsoleApp.NameList = new List<string>();

            ConsoleApp.AddFuntion(new Exit());
        }

        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            Console.WriteLine();
            Console.WriteLine("键入一下命令调用对应的程序：");
            Console.WriteLine();

            foreach (string i in ConsoleApp.NameList)
                Console.WriteLine("{0}： {1}".FormatString(i, ConsoleApp.Functions[i].Description));

            while (true)
            {
                string command = Console.ReadLine();

                string[] array = command.SplitByWhiteSpace();

                if (array.Length > 0)
                {
                    Function function;

                    if (ConsoleApp.Functions.TryGetValue(array[0].ToLower(), out function))
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
            ConsoleApp.Functions.Add(function.Name, function);

            ConsoleApp.NameList.Add(function.Name);
        }
    }
}
