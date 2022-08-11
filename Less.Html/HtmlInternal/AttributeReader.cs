//bibaoke.com

using System.Text.RegularExpressions;
using System.Linq;
using Less.Text;

namespace Less.Html.HtmlInternal
{
    /// <summary>
    /// 属性阅读器
    /// </summary>
    internal class AttributeReader : ReaderBase
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        private static Regex Pattern
        {
            get;
            set;
        }

        /// <summary>
        /// 正在读取的闭标签
        /// </summary>
        private string CloseTagName
        {
            get;
            set;
        }

        private int CloseTagIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 正在读取属性的元素
        /// </summary>
        private Element Element
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        static AttributeReader()
        {
            AttributeReader.Pattern = @"
                ((?<single>/>)|(?<double>>))|
                (?<name>\S+?)\s*=\s*(?<mark>[""'])\s*(?<value>.*?)\s*\k<mark>|
                (?<name>\S+?)\s*=\s*(?<value>.*?)((?<space>\s)|(?<single>/>)|(?<double>>))|
                (?<mark>[""'])\s*(?<value>.*?)\s*\k<mark>((?<space>\s)|(?<single>/>)|(?<double>>))|
                (?<name>\S+?)((?<space>\s)|(?<single>/>)|(?<double>>)|$)
                ".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.Singleline |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);
        }

        /// <summary>
        /// 设置正在读取的闭标签
        /// </summary>
        /// <param name="closeTagName"></param>
        /// <param name="closeTagIndex"></param>
        /// <returns></returns>
        internal AttributeReader Set(string closeTagName, int closeTagIndex)
        {
            this.CloseTagName = closeTagName;
            this.CloseTagIndex = closeTagIndex;

            return this;
        }

        /// <summary>
        /// 设置正在读取的开标签
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        internal AttributeReader Set(Element element)
        {
            this.Element = element;

            return this;
        }

        /// <summary>
        /// 阅读
        /// </summary>
        /// <returns></returns>
        internal override ReaderBase Read()
        {
            //匹配属性或标签结束
            Match match = AttributeReader.Pattern.Match(this.Content, this.Position);

            //如果匹配成功
            if (match.Success)
            {
                //提升阅读位置
                this.Ascend(match);

                //捕获的单标签结束
                Group single = match.Groups["single"];
                //捕获的双标签结束
                Group xdouble = match.Groups["double"];

                //如果是双标签结束
                if (xdouble.Success)
                {
                    //如果标签对应的元素不为空 即当前位置在开标签内
                    if (this.Element.IsNotNull())
                    {
                        //标记标签结束前是否存在空白字符
                        if (char.IsWhiteSpace(this.Content[xdouble.Index - 1]))
                        {
                            this.Element.IsWhiteSpaceBeforeTagClosed = true;
                        }

                        this.Element.InnerBegin = this.Position;

                        //如果是单标签元素
                        if (this.Element.IsSingle)
                        {
                            this.Element.InnerEnd = this.Position - 1;

                            this.Element.End = this.Element.InnerEnd;
                        }
                        //如果不是单标签元素
                        else
                        {
                            //开标签
                            this.OpenTag(this.Element);
                        }

                        //设置属性
                        this.SetAttribute(match, -xdouble.Length);

                        //结束标签
                        return this.EndTag(this.Element.Name);
                    }
                    //如果当前位置在闭标签内
                    else
                    {
                        //关闭标签
                        return this.CloseTag(this.CloseTagName, this.CloseTagIndex - 1);
                    }
                }
                //如果是单标签结束
                else if (single.Success)
                {
                    //如果当前位置在开标签内
                    if (this.Element.IsNotNull())
                    {
                        this.Element.IsSingle = true;

                        //标记元素的标签结束方法
                        this.Element.IsSingleTagClosed = true;

                        //标记标签结束前是否存在空白字符
                        if (char.IsWhiteSpace(this.Content[single.Index - 1]))
                        {
                            this.Element.IsWhiteSpaceBeforeTagClosed = true;
                        }

                        this.Element.End = this.Position - 1;

                        //设置属性
                        this.SetAttribute(match, -single.Length);

                        //结束
                        return this.EndTag(this.Element.Name);
                    }
                    //如果当前位置在闭标签内
                    else
                    {
                        //关闭标签
                        return this.CloseTag(this.CloseTagName, this.CloseTagIndex - 1);
                    }
                }
                //如果标签未结束
                else
                {
                    //如果当前位置在开标签内
                    if (this.Element.IsNotNull())
                    {
                        Group space = match.Groups["space"];

                        if (space.Success)
                        {
                            this.SetAttribute(match, -space.Length);
                        }
                        else
                        {
                            this.SetAttribute(match, 0);
                        }
                    }

                    //继续读取下一个属性
                    return this;
                }
            }
            else
            {
                return this.Pass<EndingReader>();
            }
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        ///<param name="match"></param>
        ///<param name="offset"></param>
        private void SetAttribute(Match match, int offset)
        {
            //属性名
            Group name = match.Groups["name"];

            //如果属性名匹配失败
            if (!name.Success)
            {
                //返回 方法结束
                return;
            }

            //属性值
            Group value = match.Groups["value"];

            //属性名索引
            int nameBegin = name.Index;
            //属性名长度
            int nameLength = name.Length;

            //属性值索引
            int valueBegin = -1;
            //属性值长度
            int valueLength = -1;

            if (value.Success)
            {
                valueBegin = value.Index;
                valueLength = value.Length;
            }

            Attr attr = new Attr(
                this.Element,
                match.Index, match.Index + match.Length - 1 + offset,
                nameBegin, nameLength,
                valueBegin, valueLength);

            //标记属性前面是否有空白字符分隔
            if (char.IsWhiteSpace(this.Content[name.Index - 1]))
            {
                attr.IsWhiteSpaceBefore = true;
            }

            //添加属性
            this.Element.attributes.Add(attr);
        }
    }
}
