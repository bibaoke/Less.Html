using System.Windows.Forms;
using Less.Html;
using System;
using System.Threading;
using Less.Text;
using System.Collections.Generic;
using Less;
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

            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.webBrowser1.Navigate("http://list.jd.com/list.html?cat=9987,653,655");
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Asyn.Exec(() =>
            {
                int times = 0;

                while (times < 20)
                {
                    Thread.Sleep(1000);

                    Document document = null;

                    this.Invoke(new Action(() =>
                    {
                        document = HtmlParser.Parse(this.webBrowser1.Document.Body.InnerHtml);
                    }));

                    var q = Selector.Bind(document);

                    var sku = q(".j-sku-item");

                    List<ValueSet<string, string>> list = new List<ValueSet<string, string>>();

                    foreach (Element i in sku)
                    {
                        string name = q(i).find(".p-name a em").text();

                        string price = q(i).find(".p-price .J_price:first").text();

                        if (name.IsNotWhiteSpace() && price.IsNotWhiteSpace())
                        {
                            list.Add(new ValueSet<string, string>(name, price));
                        }
                        else
                        {
                            list.Clear();

                            break;
                        }
                    }

                    if (list.Count > 0)
                    {
                        foreach (var i in list)
                            Console.WriteLine(i.Value1.Combine(Symbol.Tab, i.Value2));

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
