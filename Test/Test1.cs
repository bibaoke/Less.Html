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
                return "示例一：获取嵌套元素中的正确内容 http://bibaoke.com/post/75";
            }
        }

        public override bool Execute(params string[] args)
        {
            string testHtml =
@"<table>
  <tr>
    <td>姓名</td>
    <td>学号</td>
    <td>学分</td>
  </tr>
  <tr>
    <td>张三</td>
    <td>
        <table>
            <tr>
              <td>201505047</td>
            </tr>
          </table>
      </td>
    <td>52</td>
  </tr>
  <tr>
    <td>李四</td>
    <td>
        <table>
            <tr>
              <td>201502072</td>
            </tr>
          </table>
    </td>
    <td>65</td>
  </tr>
</table>";

            var q = HtmlParser.Query(testHtml);

            foreach (Element i in q("td"))
            {
                if (!q(i).find("table").hasElement)
                    Console.WriteLine(i.textContent);
            }

            return true;
        }
    }
}
