//bibaoke.com

using Less.Text;

namespace Less.Html
{
    /// <summary>
    /// css 文档
    /// </summary>
    public class Css
    {
        internal string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 样式集合
        /// </summary>
        public StyleCollection Styles
        {
            get;
            private set;
        }

        /// <summary>
        /// 样式块集合
        /// </summary>
        public BlockCollection Blocks
        {
            get;
            private set;
        }

        internal Css(string content)
        {
            this.Content = content;

            this.Styles = new StyleCollection();
            this.Blocks = new BlockCollection();
        }

        /// <summary>
        /// 替换文档内容
        /// </summary>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="replacement"></param>
        internal void Replace(int index, int length, string replacement)
        {
            string before = this.Content.SubstringUnsafe(0, index);

            int afterIndex = index + length;

            string after = this.Content.SubstringUnsafe(afterIndex, this.Content.Length - afterIndex);

            this.Content = before + replacement + after;

            int offset = replacement.Length - length;

            this.Shift(index, offset);
        }

        /// <summary>
        /// 输出字面量
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Content;
        }

        private void Shift(int index, int offset)
        {
            for (int i = this.Blocks.Count - 1; i >= 0; i--)
            {
                Block block = this.Blocks[i];

                if (block.Begin > index)
                {
                    this.ShiftAfterBlock(block, offset);
                }
                else
                {
                    if (block.End >= index)
                    {
                        this.ShiftInsideBlock(index, block, offset);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            for (int i = this.Styles.Count - 1; i >= 0; i--)
            {
                Style style = this.Styles[i];

                if (!this.ShiftStyle(index, style, offset))
                {
                    break;
                }
            }
        }

        private void ShiftInsideBlock(int index, Block block, int offset)
        {
            block.End += offset;

            if (block.NameEnd >= index)
            {
                block.NameEnd += offset;
            }

            for (int i = block.Styles.Count - 1; i >= 0; i--)
            {
                Style style = block.Styles[i];

                if (!this.ShiftStyle(index, style, offset))
                {
                    break;
                }
            }
        }

        private bool ShiftStyle(int index, Style style, int offset)
        {
            if (style.Begin >= index)
            {
                this.ShiftAfterStyle(style, offset);

                return true;
            }
            else
            {
                if (style.End >= index)
                {
                    this.ShiftInsideStyle(index, style, offset);

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void ShiftInsideStyle(int index, Style style, int offset)
        {
            style.End += offset;

            if (style.SelectorEnd >= index)
            {
                style.SelectorEnd += offset;
            }

            for (int i = style.Properties.Count - 1; i >= 0; i--)
            {
                Property property = style.Properties[i];

                if (property.Begin > index)
                {
                    property.ShiftAfterProperty(offset);
                }
                else
                {
                    if (property.End >= index)
                    {
                        property.ShiftInsideProperty(index, offset);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void ShiftAfterBlock(Block block, int offset)
        {
            block.Begin += offset;
            block.End += offset;
            block.NameEnd += offset;

            foreach (Style i in block.Styles)
            {
                this.ShiftAfterStyle(i, offset);
            }
        }

        private void ShiftAfterStyle(Style style, int offset)
        {
            style.Begin += offset;
            style.End += offset;
            style.SelectorEnd += offset;

            foreach (Property i in style.Properties)
            {
                i.ShiftAfterProperty(offset);
            }
        }
    }
}
