using Less.Html;
using Less.Windows;
using System;
using System.Linq;

namespace Test
{
    public class Test2 : Function
    {
        public override string Description
        {
            get
            {
                return "示例二：以 Less.Html 做视图引擎 http://bibaoke.com/post/76";
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

            dynamic data = new[]
            {
                new { name = "Dizzy", num = "202001018", score = "80" },
                new { name = "Mira", num = "202002016", score = "85" }
            };

            Document document = HtmlParser.Parse(testHtml);

            var q = Selector.Bind(document);

            var template = q(q("tbody tr").remove()[0]);

            foreach (dynamic i in data)
            {
                var clone = template.clone();

                clone.find("td:eq(0)").text(i.name);
                clone.find("td:eq(1)").text(i.num);
                clone.find("td:eq(2)").text(i.score);

                q("tbody").append(clone);
            }

            this.WriteLine(document);

            return true;
        }
    }
}