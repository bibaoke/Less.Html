//bibaoke.com

using System.Linq;
using Less.Text;
using System;

namespace Less.Html
{
    /// <summary>
    /// 文档
    /// </summary>
    public class Document : Node
    {
        internal override int Begin
        {
            get
            {
                return 0;
            }
            set
            {
                //不能设置文档的索引
            }
        }

        internal override int End
        {
            get
            {
                return this.Content.Length - 1;
            }
            set
            {
                //不能设置文档的索引
            }
        }

        /// <summary>
        /// 文档内容
        /// </summary>
        internal new string Content
        {
            get;
            set;
        }

        /// <summary>
        /// html 解析委托
        /// </summary>
        internal Func<string, Document> Parse;

        /// <summary>
        /// 节点名称
        /// </summary>
        public override string nodeName
        {
            get
            {
                return "#document".ToUpper();
            }
        }

        private ElementCollection allCache;

        /// <summary>
        /// 返回对文档中所有 area 和 a 元素
        /// </summary>
        public Element[] links
        {
            get
            {
                return this.all.Where(i => i.Name.In("area", "a") && i.attributes["href"].IsNotNull()).ToArray();
            }
        }

        /// <summary>
        /// 所有元素
        /// </summary>
        /// <exception cref="DocumentException">文档异常</exception>
        public ElementCollection all
        {
            get
            {
                if (this.allCache.IsNull())
                {
                    this.allCache = new ElementCollection(this.GetAllElements());
                }

                return this.allCache;
            }
        }

        internal Document(string content, Func<string, Document> parse)
        {
            //设置文档内容
            this.Content = content;

            //文档的文档元素是本身
            this.ownerDocument = this;

            this.Parse = parse;
        }

        /// <summary>
        /// 克隆节点
        /// </summary>
        /// <param name="deep"></param>
        /// <returns></returns>
        public override Node cloneNode(bool deep)
        {
            Document document = this.Parse(this.Content);

            if (!deep)
            {
                foreach (Node i in document.ChildNodeList)
                    document.removeChild(i);
            }

            return document;
        }

        /// <summary>
        /// 重置文档 all 属性的缓存
        /// </summary>
        internal void ResetAllCache()
        {
            this.allCache = null;
        }

        /// <summary>
        /// 返回对拥有指定 id 的第一个元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        public Element getElementById(string id)
        {
            return this.all.Where(i => i.id.CompareIgnoreCase(id)).FirstOrDefault();
        }

        /// <summary>
        /// 返回带有指定名称的元素
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        public Element[] getElementsByName(string name)
        {
            return this.all.Where(i => i.Name.CompareIgnoreCase(name)).ToArray();
        }

        /// <summary>
        /// 返回带有指定标签名的元素
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        /// <exception cref="DocumentException">文档异常</exception>
        public Element[] getElementsByTagName(string tagName)
        {
            return this.all.Where(i => i.Name.CompareIgnoreCase(tagName)).ToArray();
        }

        /// <summary>
        /// 创建元素
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Element createElement(string name)
        {
            return new Element(name);
        }
    }
}
