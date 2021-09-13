using ProductsAdmin.Database.Models;
using ProductsAdmin.Models.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAdmin.Models.Api
{
    public class ConvertDatabaseToRest
    {

        public static ProductRestResponse ConvertProductToProductRestResponse(Product target)
        {
            var result = new ProductRestResponse();
            result.Id = target.Id;
            result.Name = target.Name;
            result.Description = target.Description;
            result.Status = target.StatusNavigation.Name;
            result.StatusId = target.StatusNavigation.Id;
            result.Prices = target.ProductPrices.Select(ConvertPriceToPriceRestResponse).ToList();

            return result;
        }

        public static PriceRestResponse ConvertPriceToPriceRestResponse(ProductPrice target)
        {
            var result = new PriceRestResponse();
            result.Id = target.Id;
            result.IsDefaultPrice = target.IsDefaultPrice;
            result.Price = target.Price;
            result.ProductId = target.ProductId;
            result.Status = target.StatusNavigation.Name;
            result.StatusId = target.StatusNavigation.Id;
            result.ColorName = target.Color.Name;
            result.ColorValue = target.Color.Value;
            result.ColorFormat = target.Color.Format;
            result.ColorId = target.Color.Id;

            return result;
        }
    }
}
