using Application.Common.Interfaces;
using Application.Common.Models;
using Mediator;

namespace Application.TodoItems.Queries.GetTodoItemsWithPagination;

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemBriefDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<PaginatedList<TodoItemBriefDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TodoItems
            .Where(x => x.ListId == request.ListId)
            .Select(x => new TodoItemBriefDto
            {
                Id = x.Id,
                ListId = x.ListId,
                Title = x.Title,
                Done = x.Done
            })
            .OrderBy(x => x.Title);

        return await PaginatedList<TodoItemBriefDto>.CreateAsync(query, request.PageNumber, request.PageSize);
    }
}
