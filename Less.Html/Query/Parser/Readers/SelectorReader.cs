//bibaoke.com

using System.Text.RegularExpressions;
using Less.Text;

namespace Less.Html.SelectorParamParser
{
    internal class SelectorReader : ReaderBase
    {
        private static Regex Pattern
        {
            get;
            set;
        }

        static SelectorReader()
        {
            SelectorReader.Pattern = @"
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
            Match match = SelectorReader.Pattern.Match(this.Param, this.Position);

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
                    {
                        throw new SelectorParamException(symbol.Index, symbol.Value);
                    }

                    switch (symbol.Value)
                    {
                        case ",":
                            if (this.CurrentFilter.IsNull())
                            {
                                throw new SelectorParamException(symbol.Index, symbol.Value);
                            }

                            this.CurrentSymbol = null;
                            this.CurrentFilter = null;

                            break;
                        case "*":
                            if (this.CurrentFilter.IsNotNull() && !this.PreSpace)
                            {
                                throw new SelectorParamException(symbol.Index, symbol.Value);
                            }

                            this.AddFilter(new FilterByAll(), false);

                            break;
                        case "[":
                            return this.Pass<AttrSelectorReader>();
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
                        {
                            throw new SelectorParamException(match.Index, match.Value);
                        }

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
                        this.AddFilter(new FilterByTagName(name.Value), false);
                    }

                    this.CurrentSymbol = null;
                }

                return this.Pass<SelectorReader>();
            }

            return null;
        }
    }
}