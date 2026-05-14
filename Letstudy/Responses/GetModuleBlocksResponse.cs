namespace Letstudy.Responses;

public record GetModuleBlocksResponse(List<GetModuleBlocksResponseItem> Blocks);

public record GetModuleBlocksResponseItem(Guid BlockId, int Order);