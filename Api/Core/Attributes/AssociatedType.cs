using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Attributes
{
    public class AssociatedType
    {
        public Type Type { get;}
        public PropertyInfo ForeignKeyProperty { get;}

        public AssociatedType(Type type, PropertyInfo foreignKeyProperty)
        {
            Type = type;
            ForeignKeyProperty = foreignKeyProperty;
        }
    }

}
