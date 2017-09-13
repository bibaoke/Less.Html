//bibaoke.com

using System.Collections.Generic;
using System.Linq;

namespace Less.Html
{
    /// <summary>
    /// 元素扩展方法
    /// </summary>
    internal static class ElementExtensions
    {
        internal static Document GetOwnerDocument(this IEnumerable<Element> elements)
        {
            Document ownerDocument = null;

            foreach (Element i in elements)
            {
                if (ownerDocument.IsNull())
                {
                    ownerDocument = i.ownerDocument;
                }
                else
                {
                    if (ownerDocument != i.ownerDocument)
                    {
                        return null;
                    }
                }
            }

            return ownerDocument;
        }

        /// <summary>
        /// 获取子元素
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        internal static IEnumerable<Element> GetChildElements(this IEnumerable<Element> elements)
        {
            return elements.SelectMany(i => i.ChildNodeList.GetElements());
        }
    }
}
