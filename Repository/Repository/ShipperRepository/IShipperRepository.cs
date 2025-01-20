using Entities;

namespace Repository.Repository.ShipperRepository
{
    public interface IShipperRepository
    {
        public Task<IEnumerable<Shipper>> GetShippersAsync();
    }
}
