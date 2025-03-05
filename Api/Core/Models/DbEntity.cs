using Quimica.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    public class DbEntity
    {
        [PrimaryKey]
        [ColumnName("Id")]
        public int  Id { get; set; }
    }
}
