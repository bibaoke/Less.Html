using Less.Windows;
using Less.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Less.Windows;
using System.Text;

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

            return true;
        }
    }
}