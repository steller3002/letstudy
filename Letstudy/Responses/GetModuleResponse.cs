namespace Letstudy.Responses;

public record GetModuleResponse(string Title, List<GetModuleResponseBlockItem> BlockItems);

public record GetModuleResponseBlockItem();