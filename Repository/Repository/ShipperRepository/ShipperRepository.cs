using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository.ShipperRepository
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly AplicationDbContext _context;

        public ShipperRepository(AplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Shipper>> GetShippersAsync()
        {
            return await this._context.Set<Shipper>()
                .ToListAsync();
        }
    }
}
