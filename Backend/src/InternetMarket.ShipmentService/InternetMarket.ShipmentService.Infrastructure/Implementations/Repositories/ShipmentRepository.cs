using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.Abstractions.Repositories;
using InternetMarket.ShipmentService.Domain.Entities;
using InternetMarket.ShipmentService.Infrastructure.Persistence.DB;
using Microsoft.EntityFrameworkCore;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly ShipmentContext _context;

        public ShipmentRepository(ShipmentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shipment>> GetAllAsync()
        {
            var shipments = await _context.Shipments.ToListAsync();
            return shipments;
        }

        public async Task<Shipment?> GetByIdAsync(Guid id)
        {
            var shipment = await _context.Shipments.FirstOrDefaultAsync(s => s.Id == id);
            return shipment;
        }

        public async Task CreateAsync(Shipment shipment)
        {
            await _context.Shipments.AddAsync(shipment);
        }

        public async Task UpdateAsync(Shipment shipment)
        {
            _context.Shipments.Update(shipment);
        }


        public async Task DeleteAsync(Shipment shipment)
        {
            _context.Shipments.Remove(shipment);
        }

    }
}