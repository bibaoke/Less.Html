//bibaoke.com

using System.Text.RegularExpressions;
using Less.Text;

namespace Less.Html
{
    /// <summary>
    /// 标签阅读器
    /// </summary>
    internal class TagReader : ReaderBase
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
        /// 初始化
        /// </summary>
        static TagReader()
        {
            TagReader.Pattern = @"
                (?<comment><!--.*?-->)|
                <(?<open>[!a-zA-Z].*?)((?<space>\s)|(?<single>/>)|(?<double>>))|
                </(?<close>.*?)((?<space>\s)|(?<single>/>)|(?<double>>))
                ".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.Singleline |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);
        }

        /// <summary>
        /// 阅读
        /// </summary>
        /// <returns></returns>
        internal override ReaderBase Read()
        {
            //匹配标签 或注释
            Match match = TagReader.Pattern.Match(this.Content, this.Position);

            //如果匹配成功
            if (match.Success)
            {
                //提升阅读位置
                this.Ascend(match);

                //捕获的注释
                Group comment = match.Groups["comment"];

                //如果是注释
                if (comment.Success)
                {
                    //加入文本节点
                    this.AddText(match);

                    //加入注释节点
                    this.CurrentNode.appendChild(new Comment(match.Index, this.Position - 1));

                    //结束标签
                    return this.EndTag(Comment.NodeName);
                }

                //捕获的开标签
                Group open = match.Groups["open"];

                //如果是开标签
                if (open.Success)
                {
                    //捕获的空白结束
                    Group space = match.Groups["space"];
                    //捕获的开双标签结束
                    Group xdouble = match.Groups["double"];

                    //加入文本节点
                    this.AddText(match);

                    //创建元素实例
                    Element element = this.Document.createElement(open.Value.ToLower());

                    //设置元素起始位置
                    element.Begin = match.Index;

                    //加入节点
                    this.CurrentNode.appendChild(element);

                    //如果标签未结束 
                    if (space.Success)
                    {
                        //读取属性
                        return this.Pass<AttributeReader>().Set(element);
                    }
                    //如果标签已结束
                    else
                    {
                        element.InnerBegin = this.Position;

                        //且是双标签
                        if (xdouble.Success)
                        {
                            //不是单标签元素
                            if (!element.IsSingle)
                            {
                                //开标签
                                this.OpenTag(element);
                            }
                        }
                        //如果是单标签
                        else
                        {
                            //设置元素结束位置
                            element.End = this.Position - 1;
                        }

                        //结束标签
                        return this.EndTag(element.Name);
                    }
                }

                //如果是闭标签
                //捕获的闭标签名
                string name = match.Groups["close"].Value.ToLower();
                //捕获的空白结束
                Group closeSpace = match.Groups["space"];

                //加入文本节点
                this.AddText(match);

                //如果闭标签未结束
                if (closeSpace.Success)
                {
                    //读取属性
                    return this.Pass<AttributeReader>().Set(name, match.Index);
                }
                //如果闭标签已结束
                else
                {
                    //关闭标签
                    return this.CloseTag(name, match.Index - 1);
                }
            }

            return this.Pass<EndingReader>();
        }
    }
}
