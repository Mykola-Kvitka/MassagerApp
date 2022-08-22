using AutoMapper;
using MassagerApp.BLL.Interfaces;
using MassagerApp.BLL.Models;
using MassagerApp.DAL.Interfaces;
using MassagerApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MassagerApp.BLL.Services
{
    public class MassageService : IMassageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MassageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(Massages request)
        {
            var requestEntity = _mapper.Map<Massages, MassageEntity>(request);

            request.MassegeId = Guid.NewGuid();
            request.ISDeleted = false;

            await _unitOfWork.Massages.CreateAsync(requestEntity);

        }

        public async Task DeleteAsync(Guid id)
        {
            var request = await GetAsync(id);

            await _unitOfWork.Massages.DeleteAsync(id);
        }

        public async Task<Massages> GetAsync(Guid id)
        {
            var result = await _unitOfWork.Massages.GetAsync(id);

            return _mapper.Map<MassageEntity, Massages>(result);
        }

        public async Task<IEnumerable<Massages>> GetPagedAsync(Guid ChatId, string userId, int page = 1, int pageSize = 20)
        {
            var requst = await FindAsync(a => a.ChatId == ChatId);

            requst.OrderByDescending(a => a.CreateDate);

            var requstR = requst.Where(a => !a.ISDeleted && a.OwnerId == userId);

            var amountToSkip = (page - 1) * pageSize;
            return requst.Skip(amountToSkip).Take(pageSize);
        }

        public async void SoftDeleteAsync(Guid id)
        {
            var requst = await GetAsync(id);

            requst.ISDeleted = true;

            UpdateAsync(requst);
        }

        public void UpdateAsync(Massages request)
        {
            _unitOfWork.Massages.UpdateAsync(_mapper.Map<Massages,MassageEntity>(request));
        }

        private async Task<IEnumerable<Massages>> FindAsync(Expression<Func<MassageEntity, bool>> predicate)
        {

            var replay = await _unitOfWork.Massages.FindAsync(predicate);

            return _mapper.Map<List<MassageEntity>, List<Massages>>(replay);
        }
    }
}
