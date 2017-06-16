//bibaoke.com

using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Less.Html
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
        /// <param name="capture"></param>
        protected void Ascend(Capture capture)
        {
            //设置阅读位置
            this.Position = capture.Index + capture.Length;
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
                this.CurrentNode.appendChild(new Text(this.Previous.Position, end));
        }

        /// <summary>
        /// 开标签
        /// </summary>
        /// <param name="element">元素</param>
        protected void OpenTag(Element element)
        {
            //设置当前节点
            this.CurrentNode = element;

            //加入标记栈
            this.Push(element.Name, this.Position);
        }

        /// <summary>
        /// 结束标签
        /// </summary>
        /// <param name="name">标签名 小写</param>
        /// <returns></returns>
        protected ReaderBase EndTag(string name)
        {
            //设置上一个标签
            this.Previous = new TagMark(name, this.Position);

            //如果是 script 标签
            if (name == "script")
                //读取闭 script 标签
                return this.Pass<CloseScriptReader>();

            //如果不是 script 标签
            //读取下一个标签
            return this.Pass<TagReader>();
        }

        /// <summary>
        /// 关闭标签
        /// </summary>
        /// <param name="name">标签名 小写</param>
        /// <param name="innerEnd"></param>
        protected TagReader CloseTag(string name, int innerEnd)
        {
            //设置上一个标签
            this.Previous = new TagMark(name, this.Position);

            //标记栈中有对应的开标签
            //完成此双标签的读取
            if (this.MarkStack.Any(i => i.Name == name))
            {
                //从栈顶部开始找对应的开标签标记
                while (true)
                {
                    Element element = (Element)this.CurrentNode;

                    //设置元素结束位置
                    element.End = this.Position - 1;

                    element.InnerEnd = innerEnd;

                    //设置当前节点为上一级节点
                    this.CurrentNode = this.CurrentNode.parentNode;

                    //从栈顶部取出开标签 没有闭标签的双标签会被自动结束
                    //取出对应的开标签才跳出
                    if (this.MarkStack.Pop().Name == name)
                        break;
                }
            }

            //读取下一个标签
            return this.Pass<TagReader>();
        }
    }
}
