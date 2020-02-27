using System;
using System.Collections.Generic;

namespace MeioMundo.API
{
    public class Tools
    {
        public class Stock
        {
            public static bool CheckIfExit(string ref_sage, string ref_site)
            {
                bool m_exits = false;
                if (ref_sage == ref_site)
                {
                    m_exits = true;
                    Console.WriteLine(ref_sage);
                }
                return m_exits;

            }

            public static Classes.Produtos[] UpdateStock(Classes.Produtos[] m_sage, Classes.Produtos[] m_website, out int[] arrayMod)
            {
                List<int> arrayToMod = new List<int>();
                for (int i = 0; i < m_sage.Length; i++)
                {
                    for (int z = 0; z < m_website.Length; z++)
                    {
                        bool exits = CheckIfExit(m_sage[i]._Ref, m_website[z]._Ref);
                        if (exits)
                        {
                            arrayToMod.Add(z);
                            m_website[z]._Stock = m_sage[i]._Stock;
                            m_website[z]._Preço = m_sage[i]._Preço;
                        }
                        // if (exits == false)
                        // {
                        //     m_website[z]._Stock = 0;
                        // }
                    }
                }
                arrayMod = arrayToMod.ToArray();
                return m_website;
            }
        }
    }
}
