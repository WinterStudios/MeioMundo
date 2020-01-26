using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
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
        



        public class CODE_39
        {
            public static Font Font
            {
                get
                {

                    byte[] fontData = Properties.Resources.Code_39;
                    IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
                    System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
                    var fonts = new PrivateFontCollection();
                    fonts.AddMemoryFont(fontPtr, Properties.Resources.Code_39.Length);
                    return new Font(fonts.Families[0], 13.0F);
                }
            }
            public static System.Windows.Media.FontFamily _FONT 
            {
                get 
                { 
                    return new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/Tools;Component/"), "./Barcode/Fonts/#Code 39"); 
                }
            }
            public static string GetCode(string data)
            {
                if (data.Length > 0)
                    return "*" + data + "*";
                else
                    return "";
            }
        }
        public static class EAN
        {
            public static System.Windows.Media.FontFamily _Font { get { return new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/Tools;Component/"), "./Barcode/Fonts/#EAN 13"); } }
            public struct EAN_8
            {
                /// <summary>
                /// Returna o codigo completo se não tiver o 8 elemento, ou seja calcula o 8º numero (Codigo de confirmaçao)
                /// </summary>
                /// <param name="input"></param>
                /// <returns>Codigo completo</returns>
                public static string GetCode(string input) => input + _CheckSum(input).ToString();

                /// <summary>
                /// Calcula o ultimo digito para o codigo
                /// </summary>
                /// <param name="data">Codigo para criar de 7 digitos </param>
                /// <returns>Return o check_digit (Codigo de confirmação)</returns>
                public static int _CheckSum(string data)
                {
                    if (data.Length != 7 && data.Length != 8)
                        return 1;

                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] < 0x30 || data[i] > 0x39)
                            return -1;
                    }

                    int sum = 0;

                    for (int i = 6; i >= 0; i--)
                    {
                        int digit = data[i] - 0x30;
                        if ((i & 0x01) == 1)
                            sum += digit;
                        else
                            sum += digit * 3;
                    }

                    int mod = sum % 10;

                    return mod == 0 ? 0 : 10 - mod;
                }
            }
            public struct EAN_13
            {
                /// <summary>
                /// Returna o codigo completo se não tiver o 13 elemento, ou seja calcula o 13º numero (Codigo de confirmaçao)
                /// </summary>
                /// <param name="input"></param>
                /// <returns>Codigo completo</returns>
                public static string GetCode(string input)
                {
                    if (input.Length == 13)
                    {
                        string data = input.Remove(12);
                        int _checkum = _CheckSum(data);
                        if (_checkum == input[12])
                            return input;
                        else
                            return data + _checkum;
                    }
                    else
                        return input + _CheckSum(input).ToString();
                }

                /// <summary>
                /// Calcula o ultimo digito para o codigo
                /// </summary>
                /// <param name="data">Codigo para criar de 12 digitos </param>
                /// <returns>Return o check_digit (Codigo de confirmação)</returns>
                public static int _CheckSum(string data)
                {
                    if (data.Length != 12 && data.Length != 13)
                        return 1;

                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] < 0x30 || data[i] > 0x39)
                            return -1;
                    }

                    int sum = 0;

                    for (int i = 11; i >= 0; i--)
                    {
                        int digit = data[i] - 0x30;
                        if ((i & 0x01) == 1)
                            sum += digit;
                        else
                            sum += digit * 3;
                    }

                    int mod = sum % 10;

                    return mod == 0 ? 0 : 10 - mod;
                }
            }

            public static string _ConvertTextToBarcode(string input)
            {
                return "";
            }

            public class Print
            {
                public void PrintLayout()
                {

                }
            }
        }
        public class ISBN
        {
            public struct ISBN_10
            {

            }

            public struct ISBN_13
            {
                public string GetFullCode(string data)
                {
                    return "";
                }
                private int CheckDig()
                {
                    return 0;
                }
            }
        }

    }
}
