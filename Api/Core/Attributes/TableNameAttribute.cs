using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        public string NameValue { get; }

        public TableNameAttribute(string nameValue)
        {
            NameValue = nameValue;
        }
    }
}
