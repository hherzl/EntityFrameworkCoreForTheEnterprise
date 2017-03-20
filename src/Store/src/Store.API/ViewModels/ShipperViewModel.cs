using System;
using Store.Core.EntityLayer.Sales;

namespace Store.API.ViewModels
{
    public class ShipperViewModel
    {
        public ShipperViewModel()
        {

        }

        public ShipperViewModel(Shipper entity)
        {
            ShipperID = entity.ShipperID;
            CompanyName = entity.CompanyName;
        }

        public Int32? ShipperID { get; set; }

        public String CompanyName { get; set; }
    }
}
