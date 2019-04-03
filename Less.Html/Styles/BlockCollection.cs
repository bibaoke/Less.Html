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
