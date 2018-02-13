using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCproject.Core
{
    public interface IGetDataRepository
    {
        string GetNameById(int id);
    }
}
