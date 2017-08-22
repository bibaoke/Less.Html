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
                string testHtml = Application.SetupDir.CombinePath("testHtml/265.com.html").ReadString(Encoding.UTF8);

                Document document = HtmlParser.Parse(testHtml);

                Assert.IsTrue(testHtml == document.ToString());
            }

            //
            {
                string testHtml = Application.SetupDir.CombinePath("testHtml/youpinai.com.html").ReadString(Encoding.UTF8);

                var q = HtmlParser.Query(testHtml);

                foreach (Element i in q("a, area"))
                {
                    string href = q(i).href();
                }
            }

            //
            {
                string testHtml = Application.SetupDir.CombinePath("testHtml/auto.qq.com.html").ReadString(Encoding.UTF8);

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

            //
            {
                string testHtml = "<div></div>";

                var q = HtmlParser.Query(testHtml);

                q("div").html(null);

                int? i = null;

                q("div").html(i);

                q(null).html("test");

                string s = null;

                q(s).html("test");

                Assert.IsTrue(q("div").html() == "");
            }

            //
            {
                string testHtml = Application.SetupDir.CombinePath("testHtml/auto.qq.com.html").ReadString(Encoding.UTF8);

                var q = HtmlParser.Query(testHtml);

                string title = q("title:last").html();

                this.WriteLine(title);

                string description = q("meta[name='description']").attr("content");

                this.WriteLine(description);
            }

            //
            {
                string testHtml = Application.SetupDir.CombinePath("testHtml/lrcmyanmar.org.html").ReadString(Encoding.UTF8);

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("title:last").html() == "lrc myanmar");
            }

            return true;
        }
    }
}