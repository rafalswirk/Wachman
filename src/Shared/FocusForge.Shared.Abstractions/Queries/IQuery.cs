using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Shared.Abstractions.Queries
{
    public interface IQuery
    {
    }

    public interface IQuery<T> : IQuery
    {
    }
}
