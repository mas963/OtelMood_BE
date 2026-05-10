using Application.Common.Models;
using Application.TodoItems.Commands.CreateTodoItem;
using Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace API.Endpoints;

public class TodoItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTodoItemsWithPagination)
            .MapPost(CreateTodoItem);
    }

    public ValueTask<PaginatedList<TodoItemBriefDto>> GetTodoItemsWithPagination(IMediator mediator, [AsParameters] GetTodoItemsWithPaginationQuery query)
    {
        return mediator.Send(query);
    }

    public ValueTask<int> CreateTodoItem(IMediator mediator, CreateTodoItemCommand command)
    {
        return mediator.Send(command);
    }
}
