using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Barcode
{
    public class CodeInfo
    {
        private string _pro;
        private string _ref;
        public string _reference { get { return _ref; } set { _ref = value; } }
        public string _produt { get { return _pro; } set { _pro = value; } }
    }
}

