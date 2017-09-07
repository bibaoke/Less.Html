//bibaoke.com

using System;
using System.Collections.Generic;

namespace Less.Html.SelectorParamParser
{
    internal static class ParamParser
    {
        private static Dictionary<string, ElementFilter[]> Cache
        {
            get;
            set;
        }

        static ParamParser()
        {
            ParamParser.Cache = new Dictionary<string, ElementFilter[]>();
        }

        internal static ElementFilter[] Parse(string param)
        {
            ElementFilter[] filters;

            if (ParamParser.Cache.TryGetValue(param, out filters))
            {
                return filters;
            }
            else
            {
                ReaderBase reader = new SelectorReader();

                Context context = new Context(param);

                reader.Context = context;

                while (true)
                {
                    reader = reader.Read();

                    if (reader.IsNull())
                    {
                        break;
                    }
                }

                filters = context.FilterList.ToArray();

                try
                {
                    ParamParser.Cache.Add(param, filters);
                }
                catch (ArgumentException)
                {
                    //
                }

                return filters;
            }
        }
    }
}
