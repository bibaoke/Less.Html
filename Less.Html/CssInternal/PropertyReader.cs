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
                    string propertyString = match.GetValue("property");
                    string ending = match.GetValue("ending");

                    Match matchNameValue = PropertyReader.NameValuePattern.Match(propertyString);

                    if (matchNameValue.Success)
                    {
                        string name = matchNameValue.GetValue("name");
                        string value = matchNameValue.GetValue("value");

                        Property property = new Property(name, value);

                        this.CurrentStyle.Add(property);
                    }

                    if (ending == ";")
                    {
                        return this.Pass<PropertyReader>();
                    }
                    else
                    {
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
