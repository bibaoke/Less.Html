using Less.Windows;
using Less.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;

namespace Test
{
    public class Test6 : Function
    {
        public override string Description
        {
            get
            {
                return "断言测试";
            }
        }

        public override bool Execute(params string[] args)
        {
            new Test1().Execute();
            new Test2().Execute();
            new Test3().Execute();
            new Test4().Execute();
            new Test5().Execute();

            //
            {
                string testHtml = "<p>段落1</p><p>段落2</p>";

                Document document = HtmlParser.Parse(testHtml);

                Assert.IsTrue(document.getElementsByTagName("p")[1].textContent == "段落2");
            }

            //
            {
                string testHtml = @"<a href=""http://bibaoke.com"">链接</a>";

                Document document = HtmlParser.Parse(testHtml);

                Assert.IsTrue(document.links[0].textContent == "链接");
            }

            //
            {
                string testHtml = Application.SetupDir.CombinePath("265.com.html").ReadString(Encoding.UTF8);

                Document document = HtmlParser.Parse(testHtml);

                Assert.IsTrue(testHtml == document.ToString());
            }

            //
            {
                string testHtml = Application.SetupDir.CombinePath("youpinai.com.html").ReadString(Encoding.UTF8);

                var q = HtmlParser.Query(testHtml);

                foreach (Element i in q("a, area"))
                {
                    string href = q(i).href();
                }
            }

            //
            {
                string testHtml = Application.SetupDir.CombinePath("auto.qq.com.html").ReadString(Encoding.UTF8);

                var q = HtmlParser.Query(testHtml);

                q("*").ToArray();
            }

            //
            {
                string testHtml = "<div></div>";

                var q = HtmlParser.Query(testHtml);

                q("div").html(1);

                Assert.IsTrue(q("div").html() == "1");
            }

            return true;
        }
    }
}