using AutoMapper;
using Models.Response;
using Repository.Repository.ShipperRepository;

namespace Logic.ShipperLogic
{
    public class ShipperLogic : IShipperLogic
    {
        private readonly IMapper _mapper;

        private readonly IShipperRepository _shipperRepository;

        public ShipperLogic(IMapper mapper, IShipperRepository shipperRepository)
        {
            this._mapper = mapper;
            this._shipperRepository = shipperRepository;
        }
        public async Task<IEnumerable<ShipperModel>> GetShippersAsync()
        {
            var entities = await this._shipperRepository.GetShippersAsync();

            var result = _mapper.Map<List<ShipperModel>>(entities);

            return result;
        }
    }
}
