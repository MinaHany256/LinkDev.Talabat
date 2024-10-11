using LinkDev.Talabat.Core.Application.Abstraction.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Common
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex, int pageSize, IEnumerable<T> data, int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Count = count;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
