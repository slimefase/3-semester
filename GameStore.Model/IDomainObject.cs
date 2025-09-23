using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model
{
    /// <summary>
    /// Интерфейс для доменных объектов, обеспечивающий наличие уникального идентификатора.
    /// </summary>
    public interface IDomainObject
    {
        int Id { get; set; }
    }
}
