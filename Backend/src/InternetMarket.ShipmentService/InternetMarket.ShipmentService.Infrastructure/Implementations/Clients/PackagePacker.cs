using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.DTOs;
using InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Requests;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients
{
    public class PackagePacker
    {
        public List<CdekPackage> FormPackage(IEnumerable<OrderItemDto> orderItems, bool isCashPayment)
        {
            List<CdekPackage> packages = new List<CdekPackage>();
            CdekPackage? currentPackage = null;
            int packageCounter = 1;
            foreach (var item in orderItems)
            {
                if (item.IsLargeSizeProduct)
                {
                    for (int i = 0; i < item.Quantity; i++)
                    {
                        var heavyPackage = new CdekPackage();
                        heavyPackage.Number = (packageCounter++).ToString();
                        heavyPackage.AddItem(
                            item.ProductName,
                            item.ProductId.ToString(),
                            item.Weight,
                            1,
                            item.UnitPrice,
                            item.Height,
                            item.Width,
                            item.Length,
                            isCashPayment);
                        packages.Add(heavyPackage);
                    }
                    continue;
                }

                int itemSumOfSides = item.Height + item.Width + item.Length;

                for (int i = 0; i < item.Quantity; i++)
                {
                    if (currentPackage is null)
                    {
                        currentPackage = new CdekPackage();
                        currentPackage.Number = (packageCounter++).ToString();
                    }

                    if (currentPackage.CanAccommodate(itemSumOfSides, item.Weight, 1))
                    {
                        currentPackage.AddItem(
                            item.ProductName,
                            item.ProductId.ToString(),
                            item.Weight,
                            1,
                            item.UnitPrice,
                            item.Height,
                            item.Width,
                            item.Length,
                            isCashPayment);
                    }
                    else
                    {
                        packages.Add(currentPackage);

                        currentPackage = new CdekPackage();
                        currentPackage.Number = (packageCounter++).ToString();
                        currentPackage.AddItem(
                            item.ProductName,
                            item.ProductId.ToString(),
                            item.Weight,
                            1,
                            item.UnitPrice,
                            item.Height,
                            item.Width,
                            item.Length,
                            isCashPayment);
                    }
                }

            }

            if (currentPackage?.PackageItems is not null)
            {
                packages.Add(currentPackage);
            }

            return packages;
        }
    }
}