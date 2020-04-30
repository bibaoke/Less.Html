using Less.Html;
using Less.Windows;
using System;

namespace Test
{
    public class Test1 : Function
    {
        public override string Description
        {
            get
            {
                return "示例一：获取元素中的内容 http://bibaoke.com/post/75";
            }
        }

        public override bool Execute(params string[] args)
        {
            string testHtml =
@"
<table>
    <thead>
        <tr>
            <th>姓名</th>
            <th>学号</th>
            <th>学分</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>张三</td>
            <td>201505047</td>
            <td>52</td>
        </tr>
        <tr>
            <td>李四</td>
            <td>201502072</td>
            <td>65</td>
        </tr>
    </tbody>
</table>
";

            var q = HtmlParser.Query(testHtml);

            foreach (Element i in q("td"))
            {
                Console.WriteLine(i.textContent);
            }

            return true;
        }
    }
}
