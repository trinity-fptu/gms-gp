using Application.ViewModels.WarehouseMaterial;
using Domain.Entities.Warehousing;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.WarehouseForm
{
    public class UpdateStatusImportWarehouseForm
    {
        public int Id { get; set; }

        public List<UpdateWarehouseFormMaterial> WarehouseFormMaterials { get; set; }
    }
}

