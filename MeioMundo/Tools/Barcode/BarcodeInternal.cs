using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Tools.Barcode
{
    public class BarcodeInternal
    {
        public enum TypesOfCodes
        {
            Codebar,
            Code_39,
            ISBN_10,
            ISNB_13,
            EAN_8,
            EAN_13
        }
        public string GetTypeOfCode(TypesOfCodes type)
        {
            string str = string.Empty;
            switch (type)
            {
                case TypesOfCodes.Codebar:
                    str = "Codebar";
                    break;
                case TypesOfCodes.Code_39:
                    str = "Code 39";
                    break;
                case TypesOfCodes.ISBN_10:
                    str = "ISBN 10";
                    break;
                case TypesOfCodes.ISNB_13:
                    str = "ISBN 13";
                    break;
                case TypesOfCodes.EAN_8:
                    str = "EAN 8";
                    break;
                case TypesOfCodes.EAN_13:
                    str = "EAN 13";
                    break;
            }
            return str;
        }
        /// <summary>
        /// Ter todos os tipos de codigos
        /// </summary>
        /// <returns>Faz o return em string[]</returns>
        public string[] GetTypesOfCodes()
        {
            List<string> list = new List<string>();
            foreach (var type in Enum.GetNames(typeof(TypesOfCodes)))
            {
                list.Add(type);
            }
            return list.ToArray();
        }

        /// <summary>
        /// Class que contem o essencial do tipo de codigo
        /// </summary>
        public struct Code
        {
            /// <summary>
            /// Numero do codigo
            /// </summary>
            public static string _Code { get; set; }
            /// <summary>
            /// Breve drescriçao do produto
            /// </summary>
            public static string _Descrição { get; set; }
        }
        public struct TypeCode
        {
            
        }

        /// <summary>
        /// Layout para imprimir
        /// </summary>
        public void Print()
        {
            FlowDocument fd = new FlowDocument();
            
        }
        public class CODE_39
        {
            public static string GetCode(string data)
            {
                if (data.Length > 0)
                    return "*" + data + "*";
                else
                    return "";
            }
        }
    }
}
