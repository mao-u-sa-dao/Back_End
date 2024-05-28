using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class CommentService
    {
        private readonly ICommentRepository<Comment> _commentRepository;
        public CommentService(ICommentRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<List<Comment>> GetAllCommentAsync()
        {
            return await _commentRepository.GetAllAsync();
        }
        public async Task<PagedResult<Comment>> GetCommentByAsync(int id, int pageNumber, int pageSize)
        {
            var query = await _commentRepository.GetByIncludeAsync(
                predicate: x => x.MovieId == id,
                include: x => x.Account,
                select: comment => new Comment // Chọn kiểu Comment cho phương thức select
                {
                    CommentId = comment.CommentId,
                    CommentContent = comment.CommentContent,
                    Account = new AccountUser { 
                        AccountName = comment.Account.AccountName,
                        AccountId = comment.Account.AccountId} // Nếu cần, bạn có thể chọn ra các thuộc tính khác của Account ở đây
                });
            var totalItems =  query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var totalPage = (int)Math.Ceiling((double)totalItems / pageSize);
            return new PagedResult<Comment>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPage,
                Items = items
            };
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _commentRepository.AddAsync(comment);
        }
    }
}
