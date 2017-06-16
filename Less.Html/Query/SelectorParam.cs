//bibaoke.com

using Less.Collection;

namespace Less.Html
{
    /// <summary>
    /// 选择器参数
    /// </summary>
    public class SelectorParam
    {
        /// <summary>
        /// 节点值
        /// </summary>
        internal Node[] NodesValue
        {
            get;
            set;
        }

        /// <summary>
        /// 字符串值
        /// </summary>
        internal string StringValue
        {
            get;
            set;
        }

        /// <summary>
        /// 查询器值
        /// </summary>
        internal Query QueryValue
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="value"></param>
        protected SelectorParam(string value)
        {
            this.StringValue = value.IsNull("");
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nodes"></param>
        protected SelectorParam(Node[] nodes)
        {
            this.NodesValue = nodes;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="query"></param>
        protected SelectorParam(Query query)
        {
            this.QueryValue = query;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected SelectorParam()
        {

        }

        /// <summary>
        /// 从 string 到 SelectorParam 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator SelectorParam(string value)
        {
            return new SelectorParam(value);
        }

        /// <summary>
        /// 从 Node[] 到 SelectorParam 的隐式转换
        /// </summary>
        /// <param name="nodes"></param>
        public static implicit operator SelectorParam(Node[] nodes)
        {
            return new SelectorParam(nodes);
        }

        /// <summary>
        /// 从 Node 到 SelectorParam 的隐式转换
        /// </summary>
        /// <param name="node"></param>
        public static implicit operator SelectorParam(Node node)
        {
            return node.ConstructArray();
        }

        /// <summary>
        /// 从 Query 到 SelectorParam 的隐式转换
        /// </summary>
        /// <param name="query"></param>
        public static implicit operator SelectorParam(Query query)
        {
            return new SelectorParam(query);
        }
    }
}
