using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class MovieCategoryService
    {
        private readonly IMovieCategoryRepository<MovieCategory> _repository;
        public MovieCategoryService(IMovieCategoryRepository<MovieCategory> repository)
        {
            _repository = repository;
        }
        public async Task<List<MovieCategory>> GetAllCategory()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<PagedResult<MovieCategory>> GetAllCategoryAdmin(int pageNumber)
        {
            if (pageNumber > 0)
            {
                int pageSize = 10;
                var category = await _repository.GetAllAsync();
                var totalItems = category.Count();
                var items = category.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var totalPage = (int)Math.Ceiling((double)totalItems / pageSize);
                return new PagedResult<MovieCategory>
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalItems = totalItems,
                    TotalPages = totalPage,
                    Items = items
                };
            }
            var categoryy = await _repository.GetAllAsync();
            return new PagedResult<MovieCategory>
            {
                Items = categoryy,
            };

        }
        public async Task AddCategory(MovieCategory movieCategory)
        {
            await _repository.AddAsync(movieCategory);
        }
        public async Task UpdateCategory(int id, MovieCategory movieCategory)
        {
            var _movieCategory = await _repository.GetByIdAsync(x=>x.CategoryId == id);
            _movieCategory.CategoryId = movieCategory.CategoryId;
            _movieCategory.CategoryName = movieCategory.CategoryName;
            await _repository.UpdateAsync(_movieCategory);

        }
        public async Task DeleteCategory(int id)
        {
            var movieCategory = await _repository.GetByIdAsync(x=>x.CategoryId == id);
            await _repository.DeleteAsync(movieCategory);
        }
    }
}
