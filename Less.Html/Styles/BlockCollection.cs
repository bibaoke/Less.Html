//bibaoke.com

using System.Collections;
using System.Collections.Generic;

namespace Less.Html
{
    /// <summary>
    /// 样式块集合
    /// </summary>
    public class BlockCollection : IEnumerable<Block>
    {
        private List<Block> List
        {
            get;
            set;
        }

        /// <summary>
        /// 样式块计数
        /// </summary>
        public int Count
        {
            get
            {
                return this.List.Count;
            }
        }

        /// <summary>
        /// 获取指定索引的样式块
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Block this[int index]
        {
            get
            {
                return this.List[index];
            }
        }

        internal BlockCollection()
        {
            this.List = new List<Block>();
        }

        internal void Add(Block block)
        {
            this.List.Add(block);
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Block> GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.List.GetEnumerator();
        }
    }
}
