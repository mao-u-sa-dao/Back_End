using WebApplication1.Models;
using WebApplication1.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication1.Services
{
    public class AccountService
    {
        private readonly IAccountRepository<AccountUser> _accountRepository;
        private readonly IInforAccountRepository<InforAccountUser> _inforAccountRepository;
        private readonly ICommentRepository<Comment> _commentRepository;
        private readonly IMoviesUserOwnedRepository<MoviesUserOwned> _moviesUserOwnedRepository;
        private readonly IMovieBillRepository<MoviesBill> _moviesBillRepository;
        public AccountService(IAccountRepository<AccountUser> accountRepository, IInforAccountRepository<InforAccountUser> inforAccountRepository, ICommentRepository<Comment> commentRepository, IMoviesUserOwnedRepository<MoviesUserOwned> moviesUserOwnedRepository, IMovieBillRepository<MoviesBill> moviesBillRepository)
        {
            _accountRepository = accountRepository;
            _inforAccountRepository = inforAccountRepository;
            _commentRepository = commentRepository;
            _moviesUserOwnedRepository = moviesUserOwnedRepository;
            _moviesBillRepository = moviesBillRepository;
        }
        public async Task<PagedResult<AccountUser>> GetAllUser(int pageNumber)
        {
            int pageSize = 10;
            var account =  await _accountRepository.GetAllAsync();
            var totalItems = account.Count();
            var items = account.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var totalPage = (int)Math.Ceiling((double)totalItems / pageSize);
            return new PagedResult<AccountUser>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPage,
                Items = items
            };
        }
        public async Task<AccountUser> GetUser(string userName, string passWord)
        {
            return await _accountRepository.GetUserAsync(x => x.AccountName == userName && x.AccountPassword == passWord);
        }
        public async Task<AccountUser> GetUserById(int id)
        {
            return await _accountRepository.GetByIdAsync(x=>x.AccountId == id);
        }
        public async Task<AccountUser> GetUserByName(string name)
        {
            return await _accountRepository.GetByIdAsync(x => x.AccountName == name);
        }
        public async Task<AccountUser> GetUserByEmail(string email)
        {
            return await _accountRepository.GetByIdAsync(x => x.AccountGmail == email);
        }
        public async Task AddUser(AccountUser user)
        {
            await _accountRepository.AddAsync(user);
        }
        public async Task UpdateUser(AccountUser user)
        {
            var _user = await _accountRepository.GetByIdAsync(x => x.AccountId == user.AccountId);
            _user.AccountId = user.AccountId;
            _user.AccountName = user.AccountName;
            _user.AccountPassword = user.AccountPassword;
            _user.AccountRole = user.AccountRole;
            _user.AccountGmail = user.AccountGmail;
            _user.AccountCreateTime = _user.AccountCreateTime;
            await _accountRepository.UpdateAsync(_user);
            
        }
        public async Task DeleteUser(int id)
        {
           
            var listCommentByUser = await _commentRepository.GetListByIdAsync(x=>x.AccountId==id);
            var listMovieOwnedByUser = await _moviesUserOwnedRepository.GetListByIdAsync1(x=>x.AccountId==id);
            var inforUser = await _inforAccountRepository.GetByIdAsync(x=>x.AccountId==id);
            var billByUser = await _moviesBillRepository.GetListByIdAsync(x=>x.AccountId == id);
            var user = await _accountRepository.GetByIdAsync(x=>x.AccountId == id);
            if(listCommentByUser != null )
            {
                foreach( var comment in listCommentByUser )
                {
                    await _commentRepository.DeleteAsync(comment);
                }
            }
            if(listMovieOwnedByUser != null )
            {
                foreach( var movie in listMovieOwnedByUser)
                {
                    await _moviesUserOwnedRepository.DeleteAsync(movie);
                }
            }
            if (inforUser != null)
            {
                await _inforAccountRepository.DeleteAsync(inforUser);
            }
            if(billByUser != null)
            {
                foreach ( var bill in billByUser)
                {
                    await _moviesBillRepository.DeleteAsync(bill);
                }
            }
            await _accountRepository.DeleteAsync(user);
        }
    }
}
