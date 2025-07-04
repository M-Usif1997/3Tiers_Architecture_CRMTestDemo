using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utils
{
    public class BaseSearchResult<T>
    {
        public int TotalCount { get; set; }

        public T? Results { get; set; }
    }
}
