using Less.Windows;
using Less.Html;

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

                foreach (Element i in document.getElementsByTagName("p"))
                    this.WriteLine(i.textContent);
            }

            return true;
        }
    }
}