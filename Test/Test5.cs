using Less.Windows;
using Less.Html;

namespace Test
{
    public class Test5 : Function
    {
        public override string Description
        {
            get
            {
                return "示例五：使用方法详解 http://bibaoke.com/post/79";
            }
        }

        public override bool Execute(params string[] args)
        {
            //0. 从解析开始
            {
                string testHtml = "<p>段落</p>";

                Document document = HtmlParser.Parse(testHtml);

                var q = Selector.Bind(document);
            }

            //0.1 快捷的方法
            {
                string testHtml = "<p>段落</p>";

                var q = HtmlParser.Query(testHtml);
            }

            //1. 获取文档的内容
            {
                string testHtml = "<p class='description'>段落</p>";

                Document document = HtmlParser.Parse(testHtml);

                var p = document.getElementsByTagName("p")[0];

                string html = p.innerHTML;

                string cls = p.getAttribute("class");
            }

            //1.1 jQuery 方法
            {
                string testHtml = "<p class='description'>段落</p>";

                var q = HtmlParser.Query(testHtml);

                string html = q("p").html();

                string cls = q("p").attr("class");
            }

            //2. 修改文档的内容
            {
                string testHtml = "<p class='description'>段落</p>";

                Document document = HtmlParser.Parse(testHtml);

                var p = document.getElementsByTagName("p")[0];

                p.removeAttribute("class");

                p.setAttribute("style", "color:red");

                p.textContent = "修改的文本";
            }

            //2.1 jQuery 方法
            {
                string testHtml = "<p class='description'>段落</p>";

                var q = HtmlParser.Query(testHtml);

                q("p").removeAttr("class");

                q("p").attr("style", "color:red");

                q("p").text("修改的文本");
            }

            return true;
        }
    }
}