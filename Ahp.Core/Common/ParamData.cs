using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahp.Core.Common
{
    public class ParamHeader<T>
    {
        public T header { get; set; }
    }

    public class ParamDetail<T>
    {
        public List<T> detail { get; set; }
    }

    public class ParamHeaderDetail<T, T2>
    {
        public T header { get; set; }
        public List<T2> detail { get; set; }
    }

    public class ParamHeader2<T, T2>
    {
        public T header { get; set; }
        public T2 header2 { get; set; }
    }
}
