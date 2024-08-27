using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public class RawMaterialEnum
    {
        public enum RawMaterialColor
        {
            Red = 0,
            Green,
            Blue,
            Yellow,
            Black,
            White,
            Orange,
            Purple,
            Pink,
            Brown,
            Gray,
            Cyan,
            Magenta,
            Gold,
            Silver,
            Bronze,
            Copper,
            None,
            Other,
        }

        public enum RawMaterialShapeEnum
        {
            None = 0,
            Circle,
            Square,
            Box,
            Cube,
            Cylinder,
            Straight,
            Curved,
        }

        public enum RawMaterialUnitEnum
        {
            Piece = 0,
            Kilogram,
            Gram,
            Meter,
            Liter,
            Squaremeter,
        }

        public enum PackageUnitEnum
        {
            Bag = 0,
            Roll,
            Can,
            Carton,
        }
        public enum WeightUnitEnum
        {
            Tonne = 0,
            Kilogram,
            Gram,
        }
        public enum LengthUnitEnum
        {
            Meter,
            Centimeter,
            Millimeter,
        }
        public enum AreaUnitEnum
        {
            Squaremeter,
            Squarecentimeter,
        }

        public enum ProductUnitEnum
        {
            Set,
            Piece,
            Roll,
        }

        public enum ThreadRatioEnum
        {
            None,
            Other,
            _60_3,
            _40_2,
            _20_6,
            _40_3,
        }

        public enum ProductSizeEnum
        {
            None,
            Other,
            XS,
            S,
            M,
            L,
            XL,
            XXL,
        }
    }
}
