using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DeliveryStage
{
    public class DeliveryStageUpdateQuantityVM
    {
        public int Id { get; set; }
        public List<PurchaseMaterialUpdateQuantityVM> PurchaseMaterials { get; set; }
    }
}
