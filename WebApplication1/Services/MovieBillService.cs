using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class MovieBillService
    {
        private readonly IMovieBillRepository<MoviesBill> _repository;
        public MovieBillService(IMovieBillRepository<MoviesBill> repository)
        {
            _repository = repository;
        }

        public async Task<List<MoviesBill>> GetListBillByIdAccount(int id)
        {
            return await _repository.GetListByIdAsync(x=>x.AccountId == id);
        }
        public async Task<PagedResult<MoviesBill>> GetAllBIllAsync(int pageNumber)
        {
            int pageSize = 10;
            var query = await _repository.GetAllAsync(
                include: x => x.MovieList,
                include1: x => x.MovieList,
                select: movieBill => new MoviesBill
                {
                    BillId = movieBill.BillId,
                    BillCreateTime = movieBill.BillCreateTime,
                    MovieList = new MoviesList
                    {
                        MovieListId = movieBill.MovieList.MovieListId,
                        MovieListName = movieBill.MovieList.MovieListName,
                        Price = movieBill.MovieList.Price,
                        AvatarMovie = movieBill.MovieList.AvatarMovie,
                    },
                    Account = new AccountUser
                    {
                        AccountName = movieBill.Account.AccountName,
                        AccountId = movieBill.Account.AccountId,
                    }
                    
                }
                );
            var totalItems = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var totalPage = (int)Math.Ceiling((double)totalItems / pageSize);
            return new PagedResult<MoviesBill>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPage,
                Items = items
            };
        }
        public async Task AddMovieBill(MoviesBill movieBill)
        {
            await _repository.AddAsync(movieBill);
        }
        public async Task UpdateMovieBill(MoviesBill moviesBill)
        {
            var _movieBill = await _repository.GetByIdAsync(x=> x.BillId == moviesBill.BillId);
            _movieBill.BillId = moviesBill.BillId;
            _movieBill.MovieListId = moviesBill.MovieListId;
            _movieBill.AccountId = moviesBill.AccountId;
            _movieBill.BillCreateTime = moviesBill.BillCreateTime;
            await _repository.UpdateAsync(_movieBill);
        }
    }
}
