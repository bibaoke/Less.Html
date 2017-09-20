//bibaoke.com

using System.Collections;
using System.Collections.Generic;
using Less.Text;
using Less.Collection;

namespace Less.Html
{
    /// <summary>
    /// 节点集合
    /// 命名节点映射表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NamedNodeMap<T> : IEnumerable<T> where T : Node
    {
        private List<T> Value
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
                    if (i.nodeName.IsNotNull() && i.nodeName.CompareIgnoreCase(name))
                    {
                        return i;
                    }
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
            {
                node.OnRemoveNamedItem();

                node.OnRemoveFromNamedNodeMap();
            }

            return node;
        }

        /// <summary>
        /// 设置节点
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public T setNamedItem(T item)
        {
            //有些属性的 nodeName 是空引用，比如 !DOCTYPE 里面的属性
            T node = item.nodeName.IsNotNull() ? this[item.nodeName] : null;

            if (node.IsNull())
            {
                item.OnAddNamedItem(this.Reference);

                item.OnAddToNamedNodeMap();

                return null;
            }
            else
            {
                T original = node;

                node.OnRemoveFromNamedNodeMap();

                item.OnChangeNamedItem(this.Reference, node);

                item.OnAddToNamedNodeMap();

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

        internal void Remove(T item)
        {
            this.Value.RemoveAt(item.ChildIndex);

            foreach (Node i in this.Value.GetEnumerator(item.ChildIndex))
            {
                i.ChildIndex = i.ChildIndex - 1;
            }

            item.OnRemoveFromNamedNodeMap();
        }

        internal void Insert(int index, T item)
        {
            this.Value.Insert(index, item);

            item.ChildIndex = index;

            foreach (Node i in this.Value.GetEnumerator(index + 1))
            {
                i.ChildIndex += 1;
            }

            item.OnAddToNamedNodeMap();
        }

        internal void Add(T item)
        {
            item.ChildIndex = this.Value.Count;

            this.Value.Add(item);

            item.OnAddToNamedNodeMap();
        }

        internal void AddItem(T item)
        {
            item.ChildIndex = this.Value.Count;

            this.Value.Add(item);
        }
    }
}
