﻿//bibaoke.com

using Less.Text;
using Less.Collection;
using Less.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace Less.Html
{
    /// <summary>
    /// 节点
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// 是否开启自检程序
        /// </summary>
        private static bool SelfChecking
        {
            get;
            set;
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        protected static Regex Pattern
        {
            get;
            set;
        }

        /// <summary>
        /// 本节点和所有的后代节点的总数
        /// </summary>
        internal int AllNodesCount
        {
            get;
            set;
        }

        internal int Index
        {
            get;
            set;
        }

        internal int ChildIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 子节点
        /// </summary>
        internal List<Node> ChildNodeList
        {
            get;
            private set;
        }

        /// <summary>
        /// 节点内容
        /// </summary>
        protected string Content
        {
            get
            {
                int length = this.End - this.Begin + 1;

                return this.ownerDocument.Content.SubstringUnsafe(this.Begin, length);
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
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public Node parentNode
        {
            get;
            internal set;
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
                    int nextIndex = this.ChildIndex + 1;

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
#if DEBUG
            bool? self_checking = Config.GetAppSetting("Less.Html.SelfChecking").ToBool();

            if (self_checking.IsNotNull())
            {
                Node.SelfChecking = self_checking.Value;
            }
#endif

            Node.Pattern = @"\s+|&nbsp;|&nbsp".ToRegex(RegexOptions.Compiled);
        }

        internal Node(int begin, int end) : this()
        {
            this.Begin = begin;
            this.End = end;
        }

        internal Node()
        {
            this.Index = -1;

            this.ChildIndex = -1;

            this.AllNodesCount = 1;

            this.ChildNodeList = new List<Node>();
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
        /// <exception cref="ArgumentException">参数错误</exception>
        public Node removeChild(Node node)
        {
            this.CheckChildNode(node, "node 不是当前节点的子节点");

            Node[] removeNodes = node.GetAllNodes();

            //移除文档节点列表中的节点
            this.ownerDocument.AllNodes.RemoveRange(node.Index, node.AllNodesCount);

            foreach (Node i in this.ownerDocument.AllNodes.GetEnumerator(node.Index))
            {
                i.Index = i.Index - node.AllNodesCount;
            }

            this.AlterAllChildNodesCount(-node.AllNodesCount);

            this.OnRemoveChild(node);

            //节点长度
            int length = node.End - node.Begin + 1;

            //复制节点内容
            string content = node.Content;

            //修改原文档内容
            this.ownerDocument.Content.Remove(node.Begin, length);

            int offset = -length;

            node.ShiftNext(offset);

            node.ShiftParent(offset);

            //针对 node.ChildIndex 为默认值 0 的判断处理
            if (this.ChildNodeList.Count > 0)
            {
                this.ChildNodeList.RemoveAt(node.ChildIndex);
            }

            foreach (Node i in this.ChildNodeList.GetEnumerator(node.ChildIndex))
            {
                i.ChildIndex = i.ChildIndex - 1;
            }

            node.SetIndex(-node.Begin);

            //把移除的节点放到一个新创建的文档
            Document document = new Document(content, this.ownerDocument.Parse);

            node.parentNode = document;

            node.SetOwnerDocument(document);

            removeNodes.Each((index, item) =>
            {
                item.Index = index + 1;
            });

            document.AllNodes.AddRange(removeNodes);

            document.AllNodesCount += node.AllNodesCount;

            this.OnRemovedChild(document, removeNodes);

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
        /// <exception cref="ArgumentException">参数错误</exception>
        public Node insertBefore(Node newItem, Node existingItem)
        {
            this.CheckChildNode(existingItem, "existingItem 不是当前节点的子节点");

            bool checking = false;

            //如果节点已经有所属文档
            if (newItem.ownerDocument.IsNotNull())
            {
                checking = true;

                Node[] nodes = newItem.GetAllNodes();

                this.OnInsertBefore(newItem, existingItem);

                //把节点添加到文档的节点列表
                this.ownerDocument.AllNodes.InsertRange(existingItem.Index, nodes);

                this.AlterAllChildNodesCount(newItem.AllNodesCount);

                //在原文档中删除节点
                newItem.parentNode.removeChild(newItem);

                //重新编排节点索引
                nodes.Each((index, item) =>
                {
                    item.Index = existingItem.Index + index;
                });

                foreach (Node i in this.ownerDocument.AllNodes.GetEnumerator(existingItem.Index + nodes.Length))
                {
                    i.Index += nodes.Length;
                }

                //设置父节点
                newItem.parentNode = this;

                //修改此文档的内容
                this.ownerDocument.Content.Insert(existingItem.Begin, newItem.Content);

                //设置新的索引
                newItem.SetIndex(existingItem.Begin - newItem.Begin);

                //索引偏移量
                int offset = newItem.End - newItem.Begin + 1;

                //添加新节点
                this.ChildNodeList.Insert(existingItem.ChildIndex, newItem);

                //重新编排节点的子节点索引
                newItem.ChildIndex = existingItem.ChildIndex;

                foreach (Node i in this.ChildNodeList.GetEnumerator(existingItem.ChildIndex + 1))
                {
                    i.ChildIndex += 1;
                }

                //偏移下一个节点的索引
                newItem.ShiftNext(offset);

                //偏移父节点的索引
                newItem.ShiftParent(offset);
            }
            else
            {
                newItem.Index = this.ownerDocument.AllNodes.Count;

                //把节点添加到文档的节点列表
                this.ownerDocument.AllNodes.Add(newItem);

                this.AlterAllChildNodesCount(newItem.AllNodesCount);

                this.OnInsertBefore(newItem, existingItem);

                //设置父节点
                newItem.parentNode = this;

                //添加新节点
                this.ChildNodeList.Insert(existingItem.ChildIndex, newItem);

                //重新编排节点的子节点索引
                newItem.ChildIndex = existingItem.ChildIndex;

                foreach (Node i in this.ChildNodeList.GetEnumerator(existingItem.ChildIndex + 1))
                {
                    i.ChildIndex += 1;
                }
            }

            //设置所属文档
            newItem.SetOwnerDocument(this.ownerDocument);

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

                Node[] nodes = node.GetAllNodes();

                if (nextNode.IsNotNull())
                {
                    this.ownerDocument.AllNodes.InsertRange(nextNode.Index, nodes);
                }
                else
                {
                    this.ownerDocument.AllNodes.AddRange(nodes);
                }

                this.AlterAllChildNodesCount(node.AllNodesCount);

                this.OnAppendChild(node);

                //在原文档中删除节点
                node.parentNode.removeChild(node);

                //重新编排节点索引
                if (nextNode.IsNotNull())
                {
                    nodes.Each((index, item) =>
                    {
                        item.Index = nextNode.Index + index;
                    });

                    foreach (Node i in this.ownerDocument.AllNodes.GetEnumerator(nextNode.Index + nodes.Length))
                    {
                        i.Index += nodes.Length;
                    }
                }
                else
                {
                    nodes.Each((index, item) =>
                    {
                        item.Index = this.ownerDocument.AllNodes.Count + index - nodes.Length;
                    });
                }

                //设置父节点
                node.parentNode = this;

                //插入位置 父节点不是元素就是文档
                int begin = this.GetAppendIndex();

                //修改此文档的内容
                this.ownerDocument.Content.Insert(begin, node.Content);

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
                node.Index = this.ownerDocument.AllNodes.Count;

                this.ownerDocument.AllNodes.Add(node);

                this.AlterAllChildNodesCount(node.AllNodesCount);

                this.OnAppendChild(node);

                //设置父节点
                node.parentNode = this;
            }

            //设置所属文档
            node.SetOwnerDocument(this.ownerDocument);

            //添加新节点
            node.ChildIndex = this.ChildNodeList.Count;

            this.ChildNodeList.Add(node);

            //自检
            if (checking)
            {
                this.ownerDocument.SelfCheck();
            }

            return node;
        }

        internal void DiscardNode(Node node)
        {
            this.ownerDocument.AllNodes.RemoveAt(node.Index);

            this.AlterAllChildNodesCount(-node.AllNodesCount);

            this.ChildNodeList.RemoveAt(node.ChildIndex);

            this.OnRemoveChild(node);
        }

        internal abstract Node Clone(Node parent);

        internal virtual void OnAddToNamedNodeMap()
        {
            //
        }

        internal virtual void OnRemoveFromNamedNodeMap()
        {
            //
        }

        internal virtual void OnChangeNamedItem(Node reference, Node replace)
        {
            //
        }

        internal virtual void OnAddNamedItem(Node reference)
        {
            //
        }

        internal virtual void OnRemoveNamedItem()
        {
            //
        }

        internal void SelfCheck()
        {
            if (Node.SelfChecking)
            {
                this.ownerDocument.Content.ExecInSelfCheck(() =>
                {
                    try
                    {
                        string content = this.Content;
                    }
                    catch (Exception ex)
                    {
                        throw new SelfCheckingException(ex);
                    }

                    int collectionIndex = this.ownerDocument.AllNodes.IndexOf(this);

                    if (this.Index != collectionIndex)
                    {
                        throw new SelfCheckingException();
                    }

                    this.OnSelfCheck();

                    foreach (Node i in this.ChildNodeList)
                    {
                        i.SelfCheck();
                    }
                });
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

        /// <summary>
        /// 枚举本节点和所有的后代节点
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<Node> EnumerateAllNodes()
        {
            return this.ownerDocument.AllNodes.GetEnumerator(this.Index, this.AllNodesCount);
        }

        /// <summary>
        /// 获取本节点和所有的后代节点
        /// </summary>
        /// <returns></returns>
        protected Node[] GetAllNodes()
        {
            Node[] nodes = new Node[this.AllNodesCount];

            this.ownerDocument.AllNodes.CopyTo(this.Index, nodes, 0, this.AllNodesCount);

            return nodes;
        }

        /// <summary>
        /// 获取节点的插入索引
        /// </summary>
        /// <returns></returns>
        protected virtual int GetAppendIndex()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 在移除子节点之后调用 此时子节点会属于一个新的文档
        /// </summary>
        /// <param name="document">移除节点的所属文档</param>
        /// <param name="nodes">已经移除的节点</param>
        protected virtual void OnRemovedChild(Document document, Node[] nodes)
        {
            //
        }

        /// <summary>
        /// 在移除子节点时调用
        /// </summary>
        /// <param name="node"></param>
        protected virtual void OnRemoveChild(Node node)
        {
            //
        }

        /// <summary>
        /// 在插入子节点时调用
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="existingItem"></param>
        protected virtual void OnInsertBefore(Node newItem, Node existingItem)
        {
            //
        }

        /// <summary>
        /// 在添加子节点时调用
        /// </summary>
        /// <param name="node"></param>
        protected virtual void OnAppendChild(Node node)
        {
            //
        }

        /// <summary>
        /// 自检函数
        /// </summary>
        protected virtual void OnSelfCheck()
        {

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
            string replaced = Node.Pattern.Replace(text.Trim(), Symbol.Space);

            return replaced.HtmlDecode();
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
                if (!this.parentNode.nodeName.CompareIgnoreCase("#DOCUMENT"))
                {
                    this.parentNode.End += offset;
                }

                this.parentNode.OnShiftParent(offset);

                //偏移父节点索引时 要偏移父节点的下一个节点的索引
                this.parentNode.ShiftNext(offset);

                this.parentNode.ShiftParent(offset);
            }
        }

        private void CheckChildNode(Node node, string exceptionMessage)
        {
            if (node.parentNode != this)
            {
                throw new ArgumentException(exceptionMessage);
            }
        }

        private void AlterAllChildNodesCount(int difference)
        {
            this.AllNodesCount += difference;

            if (this.parentNode.IsNotNull())
            {
                this.parentNode.AlterAllChildNodesCount(difference);
            }
        }

        private Node GetNextNode()
        {
            int index = this.Index + this.AllNodesCount;

            if (this.ownerDocument.AllNodes.Count > index)
            {
                return this.ownerDocument.AllNodes[index];
            }
            else
            {
                return null;
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
