using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopGunLab_P3.Models
{
    public class Product
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public uint Count { get; set; }

        [EnumDataType(typeof(UnitVariables))]
        public UnitVariables Unit { get; set; }

    }
}
