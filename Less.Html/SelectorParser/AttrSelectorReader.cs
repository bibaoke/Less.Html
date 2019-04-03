//bibaoke.com

using System.Text.RegularExpressions;
using Less.Text;

namespace Less.Html.SelectorParser
{
    internal class AttrSelectorReader : ReaderBase
    {
        private static Regex Pattern
        {
            get;
            set;
        }

        private static Regex ValuePattern
        {
            get;
            set;
        }

        static AttrSelectorReader()
        {
            AttrSelectorReader.Pattern = @"
                (?<name>.*?)='(?<value>\S*?)'\]|
                (?<name>.*?)=""(?<value>\S*?)""\]|
                (?<name>.*?)\]
                ".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.Multiline |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);
        }

        internal override ReaderBase Read()
        {
            Match match = AttrSelectorReader.Pattern.Match(this.Param, this.Position);

            if (match.Success)
            {
                this.Ascend(match);

                bool next = !this.PreSpace;

                string name = match.GetValue("name");

                string value = null;

                Group group = match.Groups["value"];

                if (group.Success)
                {
                    value = group.Value;
                }

                this.AddFilter(new FilterByAttr(name, value), next);

                return this.Pass<SelectorReader>();
            }

            throw new SelectorParamException(this.Position, this.Param[this.Position].ToString());
        }
    }
}