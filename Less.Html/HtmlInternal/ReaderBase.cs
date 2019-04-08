//bibaoke.com

using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Less.Text;
using Less.Collection;

namespace Less.Html.HtmlInternal
{
    /// <summary>
    /// 阅读器 抽象类
    /// </summary>
    internal abstract class ReaderBase
    {
        /// <summary>
        /// 当前正在阅读的节点
        /// </summary>
        internal Node CurrentNode
        {
            get
            {
                return this.Context.CurrentNode;
            }
            set
            {
                this.Context.CurrentNode = value;
            }
        }

        /// <summary>
        /// 标签标记栈 只包括开标签
        /// </summary>
        internal Stack<TagMark> MarkStack
        {
            get
            {
                return this.Context.MarkStack;
            }
        }

        /// <summary>
        /// 上一个标签标记 包括开标签和闭标签
        /// </summary>
        internal TagMark Previous
        {
            get
            {
                return this.Context.Previous;
            }
            set
            {
                this.Context.Previous = value;
            }
        }

        /// <summary>
        /// 当前正在阅读的位置
        /// </summary>
        internal int Position
        {
            get
            {
                return this.Context.Position;
            }
            private set
            {
                this.Context.Position = value;
            }
        }

        /// <summary>
        /// 文档
        /// </summary>
        internal Document Document
        {
            get { return this.Context.Document; }
        }

        /// <summary>
        /// 文档内容
        /// </summary>
        internal string Content
        {
            get
            {
                return this.Document.Content;
            }
        }

        /// <summary>
        /// 阅读器上下文
        /// </summary>
        internal Context Context
        {
            get;
            set;
        }

        /// <summary>
        /// 执行阅读
        /// </summary>
        /// <returns></returns>
        internal abstract ReaderBase Read();

        /// <summary>
        /// 当前阅读器任务完成 把工作交给下一个阅读器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T Pass<T>() where T : ReaderBase, new()
        {
            T t = new T();

            t.Context = this.Context;

            return t;
        }

        /// <summary>
        /// 提升阅读位置
        /// </summary>
        /// <param name="match"></param>
        protected void Ascend(Match match)
        {
            //设置阅读位置
            this.Position = match.Index + match.Length;
        }

        /// <summary>
        /// 把开标签加入标记栈
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        protected void Push(string name, int position)
        {
            this.MarkStack.Push(new TagMark(name, position));
        }

        /// <summary>
        /// 加入文本节点
        /// </summary>
        /// <param name="tag">现匹配的标签</param>
        protected void AddText(Match tag)
        {
            int end = tag.Index - 1;

            if (end >= this.Previous.Position)
            {
                this.CurrentNode.appendChild(new Text(this.Previous.Position, end));
            }
        }

        protected void OpenTag(Element element)
        {
            //设置当前节点
            this.CurrentNode = element;

            //加入标记栈
            this.Push(element.Name, this.Position);
        }

        protected ReaderBase EndTag(string name)
        {
            //设置上一个标签
            this.Previous = new TagMark(name, this.Position);

            //script 标签的 innerHTML 都作为纯文本处理
            if (name.CompareIgnoreCase("script"))
            {
                return this.Pass<CloseScriptReader>();
            }

            //style 标签的 innerHTML 都作为纯文本处理
            if (name.CompareIgnoreCase("style"))
            {
                return this.Pass<CloseStyleReader>();
            }

            //读取下一个标签
            return this.Pass<TagReader>();
        }

        protected TagReader CloseTag(string name, int innerEnd)
        {
            return this.CloseTag(name, innerEnd, this.Position - 1);
        }

        protected TagReader CloseTag(string name, int innerEnd, int end)
        {
            //设置上一个标签
            this.Previous = new TagMark(name, this.Position);

            //标记栈中有对应的开标签
            //完成此双标签的读取
            if (this.MarkStackExists(name))
            {
                //从栈顶部开始找对应的开标签标记
                while (true)
                {
                    Element element = (Element)this.CurrentNode;

                    //设置元素结束位置
                    element.End = end;

                    element.InnerEnd = innerEnd;

                    //设置当前节点为上一级节点
                    this.CurrentNode = this.CurrentNode.parentNode;

                    //从栈顶部取出开标签 没有闭标签的双标签会被自动结束
                    //取出对应的开标签才跳出
                    if (this.MarkStack.Pop().Name.CompareIgnoreCase(name))
                    {
                        break;
                    }
                }
            }

            //读取下一个标签
            return this.Pass<TagReader>();
        }

        protected bool MarkStackLast(string name)
        {
            if (this.MarkStack.Count > 0)
            {
                TagMark mark = this.MarkStack.Peek();

                if (mark.Name.CompareIgnoreCase(name))
                {
                    return true;
                }
            }

            return false;
        }

        protected bool MarkStackExists(string name)
        {
            bool exists = false;

            this.MarkStack.ToArray().EachDesc(item =>
            {
                if (item.Name.CompareIgnoreCase(name))
                {
                    exists = true;

                    return false;
                }
                else
                {
                    return true;
                }
            });

            return exists;
        }
    }
}
