//bibaoke.com

using System.Text.RegularExpressions;
using Less.Text;

namespace Less.Html.HtmlInternal
{
    /// <summary>
    /// style 闭标签阅读器
    /// </summary>
    internal class CloseStyleReader : ReaderBase
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        private static Regex Pattern
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        static CloseStyleReader()
        {
            CloseStyleReader.Pattern = @"
                </(?<style>style)((?<space>\s)|(?<single>/>)|(?<double>>))
                ".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.IgnoreCase |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);
        }

        /// <summary>
        /// 阅读
        /// </summary>
        /// <returns></returns>
        internal override ReaderBase Read()
        {
            Match match = CloseStyleReader.Pattern.Match(this.Content, this.Position);

            //如果匹配成功
            if (match.Success)
            {
                //提升阅读位置
                this.Ascend(match);

                //加入文本节点
                this.AddText(match);

                //捕获的空白结束
                Group space = match.Groups["space"];

                //如果标签未结束
                if (space.Success)
                {
                    //读取属性
                    return this.Pass<AttributeReader>().Set("style", match.Index);
                }
                //如果标签已结束
                else
                {
                    //关闭标签
                    return this.CloseTag("style", match.Index - 1);
                }
            }
            else
            {
                return this.Pass<EndingReader>();
            }
        }
    }
}
