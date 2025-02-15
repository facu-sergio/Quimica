using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotUpdatableAttribute : Attribute
    {
    }
}
