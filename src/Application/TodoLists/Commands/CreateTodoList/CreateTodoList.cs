namespace Application.TodoLists.Commands.CreateTodoList;

public record CreateTodoListCommmand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateTodoListCommmandHandler : IRequestHandler<CreateTodoListCommmand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommmandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<int> Handle(CreateTodoListCommmand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList();

        entity.Title = request.Title!;

        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}