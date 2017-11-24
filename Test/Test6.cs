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

            //
            {
                string testHtml = Application.SetupDir.CombinePath("testHtml/wsbuluo.com.html").ReadString(Encoding.UTF8);

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("title:last").html() == "纹身图案大全图片_纹身图案大全_纹身图片 - 纹身部落");
            }

            //
            {
                string testHtml = "<head></head>";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(!q("head #icon").hasElement);
            }

            //
            {
                string testHtml = "<html><head><title></title></head></html>";

                Document document = HtmlParser.Parse(testHtml);

                Assert.IsTrue(document.getElementsByTagName("head")[0].cloneNode().ToString() == "<head></head>");
            }

            //
            {
                string testHtml = "deadspin-quote-carrot-aligned-w-bgr-2<\\/title><path d=\"M10,…";

                Document document = HtmlParser.Parse(testHtml);

                Assert.IsTrue(document.childNodes.Length == 1);
                Assert.IsTrue(document.childNodes[0].nodeValue == "deadspin-quote-carrot-aligned-w-bgr-2<\\/title>");
            }

            //
            {
                string testHtml = "<div style='height:100px; width:100px'></div>";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("div").css("width") == "100px");
            }

            //
            {
                string testHtml = "<td class='ip'><div style=' display:inline-bloc";

                var q = HtmlParser.Query(testHtml);

                string text = q("td").text();
            }

            //
            {
                string testHtml = Application.SetupDir.CombinePath("testHtml/www.darc.de.html").ReadString(Encoding.UTF8);

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("[value='Spall']").html() == "");
            }

            //
            {
                string testHtml = Application.SetupDir.CombinePath("testHtml/killedbypolice.net.html").ReadString(Encoding.UTF8);

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("center")[2].textContent == "St.");
            }

            return true;
        }
    }
}