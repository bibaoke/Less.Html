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

                string html = document.getElementsByTagName("p")[0].innerHTML;

                string cls = document.getElementsByTagName("p")[0].getAttribute("class");
            }

            //1.1 jQuery 方法
            {
                string testHtml = "<p class='description'>段落</p>";

                var q = HtmlParser.Query(testHtml);

                string html = q("p").html();

                string cls = q("p").attr("class");
            }

            return true;
        }
    }
}