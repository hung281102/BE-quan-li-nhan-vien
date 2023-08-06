using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.BaseDL
{
    public interface IBaseDL<T>
    {
        public object Get();

        public object GetById(Guid id);

        public object DeleteById(Guid id);

        public object Post(T record);
        public object Put(T record, Guid id);
        //public object PagingAndSearch(int pageIndex, int pageSize, string? keyWord);
    }
}
