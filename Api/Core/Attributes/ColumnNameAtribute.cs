using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        public string NameValue { get; }

        public ColumnNameAttribute(string nameValue)
        {
            NameValue = nameValue;
        }
    }
}
