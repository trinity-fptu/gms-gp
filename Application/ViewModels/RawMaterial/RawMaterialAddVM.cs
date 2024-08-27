using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.RawMaterialEnum;

namespace Application.ViewModels.RawMaterial
{
    public class RawMaterialAddVM
    {
        public string Name { get; set; }
        public RawMaterialColor? Color { get; set; }
        public RawMaterialShapeEnum? Shape { get; set; }
        public RawMaterialUnitEnum? Unit { get; set; }
        public PackageUnitEnum Package { get; set; }

        public ThreadRatioEnum? ThreadRatio { get; set; }

        public double? Weight { get; set; }
        public double? MinToleranceWeight { get; set; }
        public double? MaxToleranceWeight { get; set; }
        public WeightUnitEnum? WeightUnit { get; set; }

        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public LengthUnitEnum? SizeUnitEnum { get; set; }

        public double? Diameter { get; set; }
        public LengthUnitEnum? DiameterUnit { get; set; }

        public string? MainIngredient { get; set; }

        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public double? EstimatePrice { get; set; }
        public int? MaterialCategoryId { get; set; }
    }
}
