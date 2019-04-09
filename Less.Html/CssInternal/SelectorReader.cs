//bibaoke.com

using System.Text.RegularExpressions;
using Less.Text;

namespace Less.Html.CssInternal
{
    internal class SelectorReader : ReaderBase
    {
        private static Regex Pattern
        {
            get;
            set;
        }

        private static Regex CloseBracePattern
        {
            get;
            set;
        }

        static SelectorReader()
        {
            SelectorReader.Pattern = @"
                (\s*(?<close>}))|
                (\s*/\*(?<comment>.*?)\*/)|
                (\s*@(?<at>.*?)\s*{)|
                (\s*(?<selector>.*?)\s*{)".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.Singleline |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);

            SelectorReader.CloseBracePattern = "}".ToRegex(
                RegexOptions.Singleline |
                RegexOptions.Compiled);
        }

        internal override ReaderBase Read()
        {
            Match match = SelectorReader.Pattern.Match(this.Content, this.Position);

            if (match.Success)
            {
                this.Ascend(match);

                Group close = match.Groups["close"];

                if (close.Success)
                {
                    if (this.CurrentBlock.IsNotNull())
                    {
                        this.Blocks.Add(this.CurrentBlock);

                        this.CurrentBlock = null;
                    }

                    return this.Pass<SelectorReader>();
                }
                else
                {
                    Group comment = match.Groups["comment"];

                    if (comment.Success)
                    {
                        return this.Pass<SelectorReader>();
                    }
                    else
                    {
                        Group at = match.Groups["at"];

                        if (at.Success)
                        {
                            if (at.Value.IsNotEmpty())
                            {
                                string prefix = at.Value.Split('-')[0];

                                if (prefix.CompareIgnoreCase("media"))
                                {
                                    this.CurrentBlock = new Block(at.Value);

                                    return this.Pass<SelectorReader>();
                                }
                                else
                                {
                                    this.CurrentStyle = new Style(at.Value);

                                    return this.Pass<PropertyReader>();
                                }
                            }
                            else
                            {
                                return this.Ignore();
                            }
                        }
                        else
                        {
                            string selector = match.GetValue("selector");

                            if (selector.IsNotEmpty())
                            {
                                this.CurrentStyle = new Style(selector);

                                return this.Pass<PropertyReader>();
                            }
                            else
                            {
                                return this.Ignore();
                            }
                        }
                    }
                }
            }
            else
            {
                return null;
            }
        }

        private ReaderBase Ignore()
        {
            Match matchCloseBrace = SelectorReader.CloseBracePattern.Match(this.Content, this.Position);

            if (matchCloseBrace.Success)
            {
                this.Ascend(matchCloseBrace);

                return this.Pass<SelectorReader>();
            }
            else
            {
                return null;
            }
        }
    }
}
