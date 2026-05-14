namespace Letstudy.Responses;

public record GetBoardResponse(string Title, List<GetBoardResponseModuleItem> Modules);

public record GetBoardResponseModuleItem(string Title);