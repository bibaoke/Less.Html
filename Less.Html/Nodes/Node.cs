//bibaoke.com

using Less.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Less.Collection;
using System;
using System.Web;

namespace Less.Html
{
    /// <summary>
    /// 节点
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        protected static Regex Pattern
        {
            get;
            set;
        }

        private int Index
        {
            get
            {
                return this.ownerDocument.AllNodes.IndexOf(this);
            }
        }

        private int AllChildNodesCount
        {
            get;
            set;
        }

        /// <summary>
        /// 是否开启自检程序
        /// </summary>
        private bool SelfChecking
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 子节点
        /// </summary>
        protected List<Node> ChildNodeList
        {
            get;
            set;
        }

        /// <summary>
        /// 节点内容
        /// </summary>
        protected string Content
        {
            get
            {
                return this.ownerDocument.Content.Substring(this.Begin, this.End - this.Begin + 1);
            }
        }

        /// <summary>
        /// 开始位置
        /// </summary>
        internal virtual int Begin
        {
            get;
            set;
        }

        /// <summary>
        /// 结束位置
        /// </summary>
        internal virtual int End
        {
            get;
            set;
        }

        /// <summary>
        /// 所属文档
        /// </summary>
        public Document ownerDocument
        {
            get;
            internal set;
        }

        /// <summary>
        /// 节点名称
        /// </summary>
        public virtual string nodeName
        {
            get;
        }

        /// <summary>
        /// 节点值
        /// </summary>
        public virtual string nodeValue
        {
            get;
            set;
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public Node parentNode
        {
            get;
            private set;
        }

        /// <summary>
        /// 子节点
        /// </summary>
        public Node[] childNodes
        {
            get
            {
                return this.ChildNodeList.ToArray();
            }
            internal set
            {
                this.childNodes.EachDesc(node => this.removeChild(node));

                foreach (Node i in value)
                {
                    this.appendChild(i);
                }
            }
        }

        /// <summary>
        /// 第一个子节点
        /// </summary>
        public Node firstChild
        {
            get
            {
                return this.ChildNodeList.FirstOrDefault();
            }
        }

        /// <summary>
        /// 最后一个子节点
        /// </summary>
        public Node lastChild
        {
            get
            {
                return this.ChildNodeList.LastOrDefault();
            }
        }

        /// <summary>
        /// 同层级的下一个节点
        /// </summary>
        public Node nextSibling
        {
            get
            {
                if (this.parentNode.IsNotNull())
                {
                    int myIndex = this.parentNode.ChildNodeList.IndexOf(this);

                    int nextIndex = myIndex + 1;

                    if (this.parentNode.ChildNodeList.Count > nextIndex)
                    {
                        return this.parentNode.ChildNodeList[nextIndex];
                    }
                }

                return null;
            }
        }

        static Node()
        {
            Node.Pattern = @"\s+".ToRegex(RegexOptions.Compiled);
        }

        /// <summary>
        /// 是否有子节点
        /// </summary>
        /// <returns></returns>
        public bool hasChildNodes()
        {
            return this.ChildNodeList.Count > 0;
        }

        /// <summary>
        /// 输出字面量
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Content;
        }

        internal Node(int begin, int end) : this()
        {
            this.Begin = begin;
            this.End = end;
        }

        internal Node()
        {
            this.AllChildNodesCount = 1;

            this.ChildNodeList = new List<Node>();
        }

        /// <summary>
        /// 克隆节点
        /// </summary>
        /// <returns></returns>
        public Node cloneNode()
        {
            return this.cloneNode(false);
        }

        /// <summary>
        /// 克隆节点
        /// </summary>
        /// <param name="deep"></param>
        /// <returns></returns>
        public abstract Node cloneNode(bool deep);

        /// <summary>
        /// 从元素中移除子节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Node removeChild(Node node)
        {
            if (!this.ChildNodeList.Contains(node))
            {
                return node;
            }

            int length = node.End - node.Begin + 1;

            //复制节点内容
            string content = node.Content;

            //修改原文档内容
            this.ownerDocument.Content = this.ownerDocument.Content.Remove(node.Begin, length);

            int offset = -length;

            node.ShiftNext(offset);

            node.ShiftParent(offset);

            this.ChildNodeList.Remove(node);

            //移除文档节点列表中的节点
            this.ownerDocument.AllNodes.RemoveRange(node.Index, node.AllChildNodesCount);

            this.AlterAllChildNodesCount(-node.AllChildNodesCount);

            this.OnRemoveChild(node);

            node.SetIndex(-node.Begin);

            //把移除的节点放到一个新创建的文档
            Document document = new Document(content, this.ownerDocument.Parse);

            node.parentNode = document;

            node.SetOwnerDocument(document);

            this.ownerDocument.SelfCheck();
            node.ownerDocument.SelfCheck();

            return node;
        }

        /// <summary>
        /// 在指定的已有的子节点之前插入新节点。
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="existingItem"></param>
        /// <returns></returns>
        public Node insertBefore(Node newItem, Node existingItem)
        {
            if (!this.ChildNodeList.Contains(existingItem))
            {
                return newItem;
            }

            bool checking = false;

            //如果节点已经有所属文档
            if (newItem.ownerDocument.IsNotNull())
            {
                checking = true;

                //在原文档中删除节点
                newItem.parentNode.removeChild(newItem);

                //设置父节点
                newItem.parentNode = this;

                //修改此文档的内容
                this.ownerDocument.Content = this.ownerDocument.Content.Insert(existingItem.Begin, newItem.Content);

                //设置新的索引
                newItem.SetIndex(existingItem.Begin - newItem.Begin);

                //索引偏移量
                int offset = newItem.End - newItem.Begin + 1;

                //添加新节点
                this.ChildNodeList.Insert(this.ChildNodeList.IndexOf(existingItem), newItem);

                //偏移下一个节点的索引
                newItem.ShiftNext(offset);

                //偏移父节点的索引
                newItem.ShiftParent(offset);
            }
            else
            {
                //设置父节点
                newItem.parentNode = this;

                //添加新节点
                this.ChildNodeList.Insert(this.ChildNodeList.IndexOf(existingItem), newItem);
            }

            //设置所属文档
            newItem.SetOwnerDocument(this.ownerDocument);

            //把节点添加到文档的节点列表
            Node nextNode = existingItem.GetNextNode();

            if (nextNode.IsNotNull())
            {
                this.ownerDocument.AllNodes.InsertRange(nextNode.Index, newItem.GetAllNodes());
            }
            else
            {
                this.ownerDocument.AllNodes.AddRange(newItem.GetAllNodes());
            }

            this.AlterAllChildNodesCount(newItem.AllChildNodesCount);

            this.OnInsertBefore(newItem, existingItem);

            if (checking)
            {
                this.ownerDocument.SelfCheck();
            }

            return newItem;
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Node appendChild(Node node)
        {
            //是否需要自检
            bool checking = false;

            //如果节点已经有所属文档
            if (node.ownerDocument.IsNotNull())
            {
                //如果添加的节点有所属文档，则需要自检
                checking = true;

                //把节点添加到文档的节点列表
                Node nextNode = this.GetNextNode();

                if (nextNode.IsNotNull())
                {
                    this.ownerDocument.AllNodes.InsertRange(nextNode.Index, node.GetAllNodes());
                }
                else
                {
                    this.ownerDocument.AllNodes.AddRange(node.GetAllNodes());
                }

                this.AlterAllChildNodesCount(node.AllChildNodesCount);

                this.OnAppendChild(node);

                //在原文档中删除节点
                node.parentNode.removeChild(node);

                //设置父节点
                node.parentNode = this;

                //插入位置 父节点不是元素就是文档
                int begin = this.GetAppendIndex();

                //修改此文档的内容
                this.ownerDocument.Content = this.ownerDocument.Content.Insert(begin, node.Content);

                //设置新的索引
                node.SetIndex(begin - node.Begin);

                //索引偏移量
                int offset = node.End - node.Begin + 1;

                //偏移父节点的索引
                node.ShiftParent(offset);
            }
            else
            {
                //把节点添加到文档的节点列表
                this.ownerDocument.AllNodes.Add(node);

                this.AlterAllChildNodesCount(node.AllChildNodesCount);

                this.OnAppendChild(node);

                //设置父节点
                node.parentNode = this;
            }

            //设置所属文档
            node.SetOwnerDocument(this.ownerDocument);

            //添加新节点
            this.ChildNodeList.Add(node);

            //自检
            if (checking)
            {
                this.ownerDocument.SelfCheck();
            }

            return node;
        }

        internal virtual void OnChangeNamedItem(Node reference, Node replace)
        {

        }

        internal virtual void OnAddNamedItem(Node reference)
        {

        }

        internal virtual void OnRemoveNamedItem()
        {

        }

        internal void SelfCheck()
        {
            if (this.SelfChecking)
            {
                try
                {
                    string content = this.Content;
                }
                catch (Exception ex)
                {
                    throw new SelfCheckingException(ex);
                }

                this.OnSelfCheck();

                foreach (Node i in this.ChildNodeList)
                {
                    i.SelfCheck();
                }
            }
        }

        /// <summary>
        /// 设置新的索引
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        internal void SetIndex(int offset)
        {
            this.Begin += offset;
            this.End += offset;

            this.OnSetIndex(offset);

            foreach (Node i in this.ChildNodeList)
            {
                i.SetIndex(offset);
            }
        }

        protected Node[] GetAllNodes()
        {
            Node[] nodes = new Node[this.AllChildNodesCount];

            this.ownerDocument.AllNodes.CopyTo(this.Index, nodes, 0, this.AllChildNodesCount);

            return nodes;
        }

        protected virtual int GetAppendIndex()
        {
            throw new NotImplementedException();
        }

        protected virtual void OnRemoveChild(Node node)
        {
            //
        }

        protected virtual void OnInsertBefore(Node newItem, Node existingItem)
        {
            //
        }

        protected virtual void OnAppendChild(Node node)
        {
            //
        }

        /// <summary>
        /// 自检函数
        /// </summary>
        protected virtual void OnSelfCheck()
        {
            //
        }

        /// <summary>
        /// 设置新的索引时调用
        /// </summary>
        /// <param name="offset"></param>
        protected virtual void OnSetIndex(int offset)
        {
            //
        }

        /// <summary>
        /// 作为下一个节点偏移索引时调用
        /// </summary>
        /// <param name="offset"></param>
        protected virtual void OnShiftNext(int offset)
        {
            //
        }

        /// <summary>
        /// 作为父节点偏移索引时调用
        /// </summary>
        /// <param name="offset"></param>
        protected virtual void OnShiftParent(int offset)
        {
            //
        }

        /// <summary>
        /// 设置节点的所属文档时调用
        /// </summary>
        /// <param name="document"></param>
        protected virtual void OnSetOwnerDocument(Document document)
        {
            //
        }

        /// <summary>
        /// 解码文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string Decode(string text)
        {
            return HttpUtility.HtmlDecode(
                Node.Pattern.Replace(text.Trim(), Symbol.Space).Replace("&nbsp;", Symbol.Space));
        }

        /// <summary>
        /// 同层级下一个节点索引偏移
        /// </summary>
        /// <param name="offset"></param>
        protected void ShiftNext(int offset)
        {
            if (this.nextSibling.IsNotNull())
            {
                this.nextSibling.Begin += offset;
                this.nextSibling.End += offset;

                this.nextSibling.OnShiftNext(offset);

                this.nextSibling.ShiftNext(offset);
            }
        }

        /// <summary>
        /// 父节点索引偏移
        /// </summary>
        /// <param name="offset"></param>
        protected void ShiftParent(int offset)
        {
            if (this.parentNode.IsNotNull())
            {
                this.parentNode.End += offset;

                this.parentNode.OnShiftParent(offset);

                //偏移父节点索引时 要偏移父节点的下一个节点的索引
                this.parentNode.ShiftNext(offset);

                this.parentNode.ShiftParent(offset);
            }
        }

        private void AlterAllChildNodesCount(int difference)
        {
            this.AllChildNodesCount += difference;

            if (this.parentNode.IsNotNull())
            {
                this.parentNode.AlterAllChildNodesCount(difference);
            }
        }

        private Node GetNextNode()
        {
            if (this.nextSibling.IsNotNull())
            {
                return this.nextSibling;
            }
            else
            {
                if (this.parentNode.IsNotNull())
                {
                    return this.parentNode.GetNextNode();
                }
                else
                {
                    return null;
                }
            }
        }

        private void SetOwnerDocument(Document document)
        {
            this.ownerDocument = document;

            this.OnSetOwnerDocument(document);

            foreach (Node i in this.ChildNodeList)
            {
                i.SetOwnerDocument(document);
            }
        }
    }
}
