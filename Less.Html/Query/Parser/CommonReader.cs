//bibaoke.com

using System.Text.RegularExpressions;
using Less.Text;

namespace Less.Html.SelectorParamParser
{
    internal class CommonReader : ReaderBase
    {
        private static Regex Pattern
        {
            get;
            set;
        }

        static CommonReader()
        {
            CommonReader.Pattern = @"
                (?<space>\s*)(?<symbol>[\.#:\[,*])|
                (?<space>\s*)(?<name>[^\.#:\[,*\s]+)
                ".ToRegex(
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.Multiline |
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture);
        }

        internal override ReaderBase Read()
        {
            Match match = CommonReader.Pattern.Match(this.Param, this.Position);

            if (match.Success)
            {
                this.Ascend(match);

                Group symbol = match.Groups["symbol"];
                Group space = match.Groups["space"];

                bool hasCurrentSymbol = this.CurrentSymbol.IsNotNull();

                if (symbol.Success)
                {
                    this.PreSpace = space.Length > 0;

                    if (hasCurrentSymbol)
                        throw new SelectorParamException(symbol.Index, symbol.Value);

                    switch (symbol.Value)
                    {
                        case ",":
                            if (this.CurrentFilter.IsNull())
                                throw new SelectorParamException(symbol.Index, symbol.Value);

                            this.CurrentSymbol = null;
                            this.CurrentFilter = null;

                            break;
                        case "*":
                            if (this.CurrentFilter.IsNotNull() && !this.PreSpace)
                                throw new SelectorParamException(symbol.Index, symbol.Value);

                            this.AddFilter(new FilterByAll(), false);

                            break;
                        default:
                            this.CurrentSymbol = symbol.Value;

                            break;
                    }
                }
                else
                {
                    Group name = match.Groups["name"];

                    if (hasCurrentSymbol)
                    {
                        if (space.Length > 0)
                            throw new SelectorParamException(match.Index, match.Value);

                        bool next = !this.PreSpace;

                        switch (this.CurrentSymbol)
                        {
                            case ".":
                                this.AddFilter(new FilterByClass(name.Value), next);
                                break;
                            case "#":
                                this.AddFilter(new FilterById(name.Value), next);
                                break;
                            case ":":
                                this.AddFilter(new FilterByOther(name.Value, match.Index), next);
                                break;
                            default:
                                throw new SelectorParamException(match.Index - this.CurrentSymbol.Length, this.CurrentSymbol);
                        }
                    }
                    else
                    {
                        this.AddFilter(new FilterByName(name.Value), false);
                    }

                    this.CurrentSymbol = null;
                }

                return this.Pass<CommonReader>();
            }

            return null;
        }

        private void AddFilter(ElementFilter filter, bool next)
        {
            if (next)
                this.SetNext(filter);
            else
                this.SetChild(filter);
        }
    }
}