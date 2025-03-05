using Quimica.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    public class Auditable : DbEntity
    {

        //public DateTime? UpdatedAt { get; set; }

        [ColumnName("Fecha")]
        public DateTime? Fecha { get; set; }

        [ColumnName("delete_at")]
        public DateTime? delete_at { get; set; }

        [SoftDeleteColumnAttribute]
        [ColumnName("is_deleted")]
        public bool is_deleted { get; set; }
    }
}
