//bibaoke.com

using System.Collections;
using System.Collections.Generic;
using Less.Text;

namespace Less.Html
{
    /// <summary>
    /// 节点集合
    /// 命名节点映射表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NamedNodeMap<T> : IEnumerable<T> where T : Node
    {
        internal List<T> Value
        {
            get;
            set;
        }

        internal Node Reference
        {
            get;
            set;
        }

        internal int Count
        {
            get
            {
                return this.Value.Count;
            }
        }

        internal int IndexOf(T item)
        {
            return this.Value.IndexOf(item);
        }

        /// <summary>
        /// 指定位置的节点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return this.Value[index]; }
            set { this.Value[index] = value; }
        }

        /// <summary>
        /// 指定名称的节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T this[string name]
        {
            get
            {
                foreach (T i in this.Value)
                {
                    if (i.nodeName.CompareIgnoreCase(name))
                        return i;
                }

                return null;
            }
            set
            {
                this[name] = value;
            }
        }

        internal NamedNodeMap(Node reference)
        {
            this.Value = new List<T>();

            this.Reference = reference;
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T getNamedItem(string name)
        {
            return this[name];
        }

        /// <summary>
        /// 移除指定的节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T removeNamedItem(string name)
        {
            T node = this[name];

            if (node.IsNotNull())
                node.OnRemoveNamedItem();

            return node;
        }

        /// <summary>
        /// 设置节点
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public T setNamedItem(T item)
        {
            T node = this[item.nodeName];

            if (node.IsNull())
            {
                item.OnAddNamedItem(this.Reference);

                return null;
            }
            else
            {
                T original = node;

                item.OnChangeNamedItem(this.Reference, node);

                return original;
            }
        }

        /// <summary>
        /// 迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Value.GetEnumerator();
        }
    }
}
