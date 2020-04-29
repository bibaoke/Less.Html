using Less.Windows;
using Less.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;
using Less.Text;

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
                string testCss = Application.SetupDir.CombinePath("testCss/app.cb1b40f2.css").ReadString(Encoding.UTF8);

                Css css = CssParser.Parse(testCss);

                Style style = css.Styles[".gd_news_box_title_text"].Single();

                Property property = style.Properties["background"].Single();

                Assert.IsTrue(style.Background.Url == "/img/ico_bg.dd1a022e.png");

                style.Background.Url = "../img/ico_bg.dd1a022e.png";

                Assert.IsTrue(((Background)property).Url == "../img/ico_bg.dd1a022e.png");
            }

            //
            {
                string testCss = Application.SetupDir.CombinePath("testCss/haidilao.com.css").ReadString(Encoding.UTF8);

                Css css = CssParser.Parse(testCss);

                Style style = css.Styles["@font-face"].First();

                SrcValue value = style.Src.Values[0];

                value.Url = "../../OpenSans-Light.ttf";

                Assert.IsTrue(style.Src.Values[0].Url == "../../OpenSans-Light.ttf");

                Assert.IsTrue(
                    css.Styles.Where(i => i.Selector == ".r_share .bds_fbook").Single().BackgroundImage.Url == "../images/facebook.png");
            }

            //
            {
                string testHtml = Application.SetupDir.CombinePath("testHtml/js.html").ReadString(Encoding.UTF8);

                Document doc = HtmlParser.Parse(testHtml);
            }

            //
            {
                string testStyle = "width:100px; height:100px; display:none; background:url(\"../test/test.png\")";

                Style style = StyleParser.Parse(testStyle);

                Assert.IsTrue(style.Background.Url == "../test/test.png");
            }

            //
            {
                string testStyle = "width:100px; height:100px; display:none; background:url(../test/test.png)";

                Style style = StyleParser.Parse(testStyle);

                Assert.IsTrue(style.Properties.Single(i => i.Name == "display").Value == "none");

                Assert.IsTrue(style.Background.Url == "../test/test.png");
            }

            //
            {
                string testCss = Application.SetupDir.CombinePath("testCss/bibaoke.com.css").ReadString(Encoding.UTF8);

                Css css = CssParser.Parse(testCss);

                Assert.IsTrue(
                    css.Styles.Where(i => i.Selector == ".test_background_image").Single().BackgroundImage.Url == "../images/test_background_image.png");
            }

            //
            new Test1().Execute();
            new Test2().Execute();
            new Test3().Execute();
            new Test4().Execute();
            new Test5().Execute();

            //
            {
                string testHtml = @"<meta name=""description"" content =""测试
<meta name=""apple-itunes-app"" content=""app-id=527806786"" />";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("meta[name='description']").attr("content") == @"测试
<meta name=");

                Assert.IsTrue(q("meta[name=\"description\"]").attr("content") == @"测试
<meta name=");

                Assert.IsTrue(q("meta[name=description]").attr("content") == @"测试
<meta name=");
            }

            //
            {
                string testHtml = "<head><ud /></head>";

                Document document = HtmlParser.Parse(testHtml);

                var q = Selector.Bind(document);

                Assert.IsTrue(q("ud").html() == "");

                Assert.IsTrue(q("ud")[0].innerHTML == "");
            }

            //
            {
                string testHtml = "<head><base href='http://bibaoke.com/blog/'><script type='text/javascript' src='/js/jquery-1.7.2.min.js'></script></head>";

                Document document = HtmlParser.Parse(testHtml);

                var q = Selector.Bind(document);

                Assert.IsTrue(q("base").href() == "http://bibaoke.com/blog/");
            }

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

                Assert.IsTrue(q("td").text() == "");
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

            //
            {
                string testHtml = "<span>0                                &nbsp;-&nbsp;1</span>";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("span").text() == "0  - 1");
            }

            //
            {
                string testHtml = "<span>0                                &nbsp-&nbsp1</span>";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("span").text() == "0  - 1");
            }

            //
            {
                string testHtml = "<div style='width:100px; height:100px; display:none'></div>";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("div").css("display") == "none");

                Assert.IsTrue(q("div").css("display", "block").css("display") == "block");
            }

            //
            {
                string testHtml = "<div style='width:100px; height:100px'></div>";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("div").css("display", "block").css("display") == "block");
            }

            //
            {
                string testHtml = "<select></select>";

                var q = HtmlParser.Query(testHtml);

                var option = q("<option></option>");

                Assert.IsTrue(q("select").append(option).html() == "<option></option>");

                Assert.IsTrue(q("select").append(option).html() == "<option></option>");

                Assert.IsTrue(q("select").append(option.clone()).html() == "<option></option>".Repeat(2));
            }

            //
            {
                string testHtml = "<div></div>";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("div").append(q("<input type='hidden' />").val(1)).html() == "<input type='hidden' value=\"1\" />");
            }

            //
            {
                string testHtml = "<input></input>";

                Document document = HtmlParser.Parse(testHtml);

                var q = Selector.Bind(document);

                q("input").text("1");

                Assert.IsTrue(document.ToString() == "<input>1</input>");
            }

            //
            {
                string testHtml = "<input />";

                Document document = HtmlParser.Parse(testHtml);

                var q = Selector.Bind(document);

                q("input").text("1");

                Assert.IsTrue(document.ToString() == "<input />1");
            }

            //
            {
                string testHtml = "<input>";

                Document document = HtmlParser.Parse(testHtml);

                var q = Selector.Bind(document);

                q("input").text("1");

                Assert.IsTrue(document.ToString() == "<input>1");
            }

            //
            {
                string testHtml = "<ol><li></li></ol>";

                var q = HtmlParser.Query(testHtml);

                var li = q("li").remove();

                q("ol").append(li);
            }

            //
            {
                string testHtml = "<ol><li></li><li></li></ol>";

                var q = HtmlParser.Query(testHtml);

                var li = q("li:first").remove();

                Assert.IsTrue(q("ol")[0].childNodes.Length == 1);

                q("ol").append(li);

                Assert.IsTrue(q("ol")[0].childNodes.Length == 2);
            }

            //
            {
                string testHtml = "<ol><li>0</li><li>1</li><li>2</li><li>3</li><li>4</li></ol>";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("li:eq(2)").text() == "2");

                Assert.IsTrue(q("li:gt(2)").text() == "34");

                Assert.IsTrue(q("li:lt(2)").text() == "01");
            }

            //
            {
                string testHtml = "<input type='checkbox' title='test' />";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q("input").attr("title", "测试").attr("title") == "测试");

                Assert.IsTrue(q(":checkbox").title() == "测试");
            }

            //
            {
                string testHtml = "<div><input type='checkbox' title='test' /></div>";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q(":checkbox").title() == "test");
            }

            //
            {
                string testHtml = "<div><input class='class1 class2 class3' type='checkbox' title='test' /></div>";

                var q = HtmlParser.Query(testHtml);

                Assert.IsTrue(q(":checkbox").removeClass("class2").attr("class") == "class1 class3");

                Assert.IsTrue(q(":checkbox").removeClass("class2 class1").attr("class") == "class3");
            }

            return true;
        }
    }
}