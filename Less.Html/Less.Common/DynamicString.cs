//bibaoke.com

using Less.Text;
using System.Collections.Generic;

namespace Less
{
    /// <summary>
    /// 动态字符串
    /// </summary>
    public class DynamicString
    {
        /// <summary>
        /// 字符串长度
        /// </summary>
        public int Length
        {
            get
            {
                return this.ToString().Length;
            }
        }

        /// <summary>
        /// 包含数据的列表
        /// </summary>
        protected List<string> List
        {
            get;
            set;
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        public DynamicString()
        {
            this.List = new List<string>();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="value"></param>
        public DynamicString(string value) : this()
        {
            this.List.Add(value);
        }

        /// <summary>
        /// 是空白字符串
        /// </summary>
        /// <returns></returns>
        public bool IsWhiteSpace()
        {
            return this.ToString().IsWhiteSpace();
        }

        /// <summary>
        /// 不是空白字符串
        /// </summary>
        /// <returns></returns>
        public bool IsNotWhiteSpace()
        {
            return !this.IsWhiteSpace();
        }

        /// <summary>
        /// 是空字符串
        /// </summary>
        public bool IsEmpty()
        {
            return this.ToString().IsEmpty();
        }

        /// <summary>
        /// 不是空字符串
        /// </summary>
        /// <returns></returns>
        public bool IsNotEmpty()
        {
            return !this.IsEmpty();
        }

        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string concat = string.Concat(this.List);

            this.List = new List<string>();

            this.List.Add(concat);

            return concat;
        }

        /// <summary>
        /// 从 string 隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator DynamicString(string value)
        {
            return value.IsNotNull() ? new DynamicString(value) : null;
        }

        /// <summary>
        /// 隐式转换成 string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(DynamicString value)
        {
            return value.IsNotNull() ? value.ToString() : null;
        }

        /// <summary>
        /// 连接操作
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static DynamicString operator +(DynamicString l, DynamicString r)
        {
            return l.Append(r);
        }

        /// <summary>
        /// 比较两个对象是否相等
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(DynamicString l, DynamicString r)
        {
            if (l.IsNotNull() && r.IsNotNull())
                return l.ToString() == r.ToString();

            return l.IsNull() && r.IsNull();
        }

        /// <summary>
        /// 比较两个对象是否不相等
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(DynamicString l, DynamicString r)
        {
            return !(l == r);
        }

        /// <summary>
        /// 比较是否相同
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is DynamicString)
                return this == (DynamicString)obj;

            return false;
        }

        /// <summary>
        /// 重写获取哈希码方法
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// 换行
        /// </summary>
        /// <returns></returns>
        public DynamicString AppendLine()
        {
            return this.Append(Symbol.NewLine);
        }

        /// <summary>
        /// 拼接值并换行
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString AppendLine(object value)
        {
            return this.AppendLine(value.ToString());
        }

        /// <summary>
        /// 拼接值并换行
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString AppendLine(string value)
        {
            return this.Append(value).Append(Symbol.NewLine);
        }

        /// <summary>
        /// 拼接值并换行
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString AppendLine(DynamicString value)
        {
            return this.Append(value).Append(Symbol.NewLine);
        }

        /// <summary>
        /// 拼接值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString Append(object value)
        {
            return this.Append(value.ToString());
        }

        /// <summary>
        /// 拼接值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString Append(string value)
        {
            this.List.Add(value);

            return this;
        }

        /// <summary>
        /// 拼接值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString Append(DynamicString value)
        {
            foreach (string i in value.List)
                this.List.Add(i);

            return this;
        }
    }
}
