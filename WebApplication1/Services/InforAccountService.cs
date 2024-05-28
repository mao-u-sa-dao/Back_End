using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class InforAccountService
    {
        private readonly IInforAccountRepository<InforAccountUser> _repository;
        public InforAccountService(IInforAccountRepository<InforAccountUser> repository)
        {
            _repository = repository;
        }
        public async Task<InforAccountUser> GetInforAccountById(int id)
        {
            return await _repository.GetByIdAsync(x=>x.AccountId == id); 
        }
        public async Task AddInforAccount(InforAccountUser inforAccount)
        {
            await _repository.AddAsync(inforAccount);
        }
        public async Task UpdateInforAccount(int id,InforAccountUser inforAccount)
        {
            var _inforAccount = await _repository.GetByIdAsync(x=> x.AccountId == id);
            _inforAccount.Id = inforAccount.Id;
            _inforAccount.AccountId = inforAccount.AccountId;
            _inforAccount.AccountMoney = inforAccount.AccountMoney;
            await _repository.UpdateAsync(_inforAccount);
        }
    }
}
