//bibaoke.com

using System.Collections.Generic;

namespace Less.Html
{
    internal abstract class IndexBase
    {
        protected void Insert(List<Element> list, Element element)
        {
            if (list.Count > 0)
            {
                int end = list.Count - 1;

                for (int i = end; i >= 0; i--)
                {
                    if (element.Index > list[i].Index)
                    {
                        if (i == end)
                        {
                            list.Add(element);
                        }
                        else
                        {
                            list.Insert(i + 1, element);
                        }

                        break;
                    }
                    else
                    {
                        if (i == 0)
                        {
                            list.Insert(0, element);
                        }
                    }
                }
            }
            else
            {
                list.Add(element);
            }
        }
    }
}
