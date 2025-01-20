using Models.Response;

namespace Logic.ShipperLogic
{
    public interface IShipperLogic
    {
        public Task<IEnumerable<ShipperModel>> GetShippersAsync();
    }
}
