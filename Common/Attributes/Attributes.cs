using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnExcel : Attribute
    {
        public string Name { get; set; }
        public ColumnExcel(string name)
        {
            Name = name;
        }
    }
    public class MaxLength : Attribute
    {
        public int Length { get; set; }
        public string Name { get; set; }

        public MaxLength(int length, string name)
        {
            Length = length;
            Name = name;
        }
    }
    public class NotEmpty : Attribute
    {
        public string Name { get; set; }
        public NotEmpty(string name)
        {
            Name = name;
        }
    }
}
