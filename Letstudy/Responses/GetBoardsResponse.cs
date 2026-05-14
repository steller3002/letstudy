namespace Letstudy.Responses;

public record GetBoardsResponse(List<GetBoardsResponseItem> BoardItems);

public record GetBoardsResponseItem(string Title);