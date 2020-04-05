using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TopGunLab_P3.Models;

namespace TopGunLab_P3.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        [Required]
        public int ForCount { get; set; }

        [EnumDataType(typeof(UnitVariables))]
        public UnitVariables Unit { get; set; }

        public bool IsAdded { get; set; }
    }
}
