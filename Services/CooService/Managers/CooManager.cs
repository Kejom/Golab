using AutoMapper;
using AutoMapper.QueryableExtensions;
using MassTransit;
using Models.Contracts;
using Models.Db;
using Models.DTO;
using MongoDB.Entities;
using System.Security.Claims;

namespace CooService.Managers
{
    public class CooManager : ICooManager
    {
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IHttpContextAccessor _contextAccessor;
        public CooManager(IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }


        public List<CooDto> Get()
        {
            var coos = DB.Queryable<Coo>().OrderByDescending(c => c.Created);
            return coos.ProjectTo<CooDto>(_mapper.ConfigurationProvider).ToList();
        }

        public async Task<List<CooDto>> Get(int page, int pageSize)
        {
            var result = await DB.PagedSearch<Coo>()
                .Sort(c => c.Created, Order.Descending)
                .PageNumber(page)
                .PageSize(pageSize)
                .ExecuteAsync();

            return _mapper.Map<List<CooDto>>(result.Results.ToList());
        }

        public async Task<CooDto> GetById(string id)
        {
            var coo = await DB.Find<Coo>().OneAsync(id);

            if (coo is null) return null;

            return _mapper.Map<CooDto>(coo);
        }

        public async Task<List<CooDto>> GetByUserId(string userId)
        {
            var coos = await DB.Find<Coo>().ManyAsync(c => c.UserId == userId);
            return _mapper.Map<List<CooDto>>(coos);
        }

        public async Task<CooDto> SaveCoo(CooDto cooDto)
        {
            var coo = _mapper.Map<Coo>(cooDto);
            coo.UserId = GetCurrentUserId();
            await coo.SaveAsync();
            return _mapper.Map<CooDto>(coo);
        }

        public async Task<CooDto> EditCoo(CooDto cooDto)
        {
            var coo = await DB.Find<Coo>().OneAsync(cooDto.Id);

            if (coo is null) throw new Exception("Coo with given id doesnt exist");

            if (coo.UserId != GetCurrentUserId()) throw new Exception("Coo can only be edited by its creator");

            coo.Content = cooDto.Content;
            await coo.SaveAsync();

            return _mapper.Map<CooDto>(coo);
        }

        public async Task RemoveCoo(string id)
        {
            var coo = await DB.Find<Coo>().OneAsync(id);

            if (coo is null) return;

            if (coo.UserId != GetCurrentUserId())
                throw new UnauthorizedAccessException("Coo can only be removed by its creator");

            await DB.DeleteAsync<Coo>(id);

            var cooRemoved = new CooRemoved { CooId = id };
            await _publishEndpoint.Publish(cooRemoved);
        }

        private string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
