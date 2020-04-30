using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticDomain
{
    public class BaseDocument<T>
    {
        public T Id { get; set; }
    }
}
