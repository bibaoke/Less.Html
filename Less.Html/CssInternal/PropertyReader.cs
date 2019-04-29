//bibaoke.com

using Less.Text;
using System.Text.RegularExpressions;

namespace Less.Html.CssInternal
{
    internal class PropertyReader : ReaderBase
    {
        private static Regex Pattern
        {
            get;
            set;
        }

        private static Regex NameValuePattern
        {
            get;
            set;
        }

        static PropertyReader()
        {
            PropertyReader.Pattern = @"
                (\s*(?<comment>/\*.*?\*/))|
                (\s*(?<property>.*?)\s*(?<ending>;|}))".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.Singleline |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);

            PropertyReader.NameValuePattern = @"(?<name>[\w-]+)\s*:\s*(?<value>.+)\s*".ToRegex(
                RegexOptions.Singleline |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);
        }

        internal override ReaderBase Read()
        {
            Match match = PropertyReader.Pattern.Match(this.Content, this.Position);

            if (match.Success)
            {
                this.Ascend(match);

                Group comment = match.Groups["comment"];

                if (comment.Success)
                {
                    return this.Pass<PropertyReader>();
                }
                else
                {
                    Group property = match.Groups["property"];
                    Group ending = match.Groups["ending"];

                    Match matchNameValue = PropertyReader.NameValuePattern.Match(property.Value);

                    if (matchNameValue.Success)
                    {
                        Group name = matchNameValue.Groups["name"];
                        Group value = matchNameValue.Groups["value"];

                        int valueBegin = property.Index + value.Index;

                        Property p = new Property(this.Css,
                            property.Index,
                            property.Index + name.Length - 1,
                            valueBegin,
                            valueBegin + value.Length - 1,
                            property.Index + property.Length - 1);

                        this.CurrentStyle.Add(p);
                    }

                    if (ending.Value == ";")
                    {
                        return this.Pass<PropertyReader>();
                    }
                    else
                    {
                        this.CurrentStyle.End = ending.Index;

                        if (this.CurrentBlock.IsNull())
                        {
                            this.Styles.Add(this.CurrentStyle);
                        }
                        else
                        {
                            this.CurrentBlock.Styles.Add(this.CurrentStyle);
                        }

                        return this.Pass<SelectorReader>();
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}
