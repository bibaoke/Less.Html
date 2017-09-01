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
            //构造数据源
            dynamic[] data = new dynamic[]
            {
                new{id = 1, name = "A", pid = 0, _float = "" },
                new{id = 2, name = "B", pid = 1, _float = "left" },
                new{id = 3, name = "C", pid = 1, _float = "right" },
                new{id = 4, name = "D", pid = 2, _float = "left" }
            };

            //视图模板
            string template = "<div><span>文字内容</span></div>";

            //绘制视图
            string html = this.RenderNode(data, 1, template);

            //输出
            Console.WriteLine(html);

            return true;
        }

        private string RenderNode(dynamic[] data, int id, string template)
        {
            //用 Less.Html 解析视图模板
            Document document = HtmlParser.Parse(template);

            //绑定查询器
            var q = Selector.Bind(document);

            //非空节点
            if (id > 0)
            {
                //获取数据
                dynamic item = data.Where(i => i.id == id).First();

                //绘制本节点
                q("span").html("用户" + item.name);

                //绘制子节点
                q("div").after("<ul></ul>");

                //左边节点
                dynamic left = data.Where(
                    i =>
                    i.pid == id &&
                    i._float == "left").FirstOrDefault();

                q("ul:first").append(
                    q(
                        "<li>" +
                        this.RenderNode(
                            data,
                            left != null ? left.id : -1,
                            template) +
                        "</li>").attr("style", "float:left"));

                //右边节点
                dynamic right = data.Where(
                    i =>
                    i.pid == id &&
                    i._float == "right").FirstOrDefault();

                q("ul:first").append(
                    q(
                        "<li>" +
                        this.RenderNode(
                            data,
                            right != null ? right.id : -1,
                            template) +
                        "</li>").attr("style", "float:right"));
            }
            //空节点
            else
            {
                //绘制添加操作节点
                q("span").html("添加");
            }

            return document.ToString();
        }
    }
}