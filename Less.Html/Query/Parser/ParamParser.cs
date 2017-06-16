//bibaoke.com

namespace Less.Html.SelectorParamParser
{
    internal static class ParamParser
    {
        internal static ElementFilter[] Parse(string param)
        {
            ReaderBase reader = new CommonReader();

            Context context = new Context(param);

            reader.Context = context;

            while (true)
            {
                reader = reader.Read();

                if (reader.IsNull())
                    break;
            }

            return context.FilterList.ToArray();
        }
    }
}
