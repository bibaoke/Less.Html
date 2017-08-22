using System.Windows.Forms;
using Less.Html;
using System;
using System.Threading;
using Less.Text;
using System.Collections.Generic;
using Less.MultiThread;

namespace Test4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Width = 1100;
            this.Height = 768;

            this.Opacity = 0;

            this.ShowInTaskbar = false;

            this.StartPosition = FormStartPosition.CenterScreen;

            this.Load += Form1_Load;

            this.webBrowser1.ScriptErrorsSuppressed = true;

            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.webBrowser1.Navigate("http://list.jd.com/list.html?cat=9987,653,655");
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //启动一个等待线程
            Asyn.Exec(() =>
            {
                int times = 0;

                while (times < 20)
                {
                    Thread.Sleep(1000);

                    Document document = null;

                    //交还 UI 线程获取这个时候的文档
                    this.Invoke(new Action(() =>
                    {
                        document = HtmlParser.Parse(this.webBrowser1.Document.Body.InnerHtml);
                    }));

                    //绑定选择器
                    var q = Selector.Bind(document);

                    //获取页面所有的商品
                    var sku = q(".j-sku-item");

                    List<Tuple<string, string>> list = new List<Tuple<string, string>>();

                    foreach (Element i in sku)
                    {
                        //商品名称
                        string name = q(i).find(
                            ".p-name a em").text();

                        //商品价格
                        string price = q(i).find(
                            ".p-price .J_price:first").text();

                        if (name.IsNotWhiteSpace() && price.IsNotWhiteSpace())
                        {
                            list.Add(new Tuple<string, string>(name, price));
                        }
                        //如果找不到商品名称或商品价格
                        //说明 ajax 请求还没完成
                        else
                        {
                            list.Clear();

                            break;
                        }
                    }

                    if (list.Count > 0)
                    {
                        foreach (var i in list)
                            Console.WriteLine(i.Item1.Combine(Symbol.Tab, i.Item2));

                        break;
                    }

                    times++;
                }

                this.Invoke(new Action(() =>
                {
                    this.Close();
                }));
            });
        }
    }
}
