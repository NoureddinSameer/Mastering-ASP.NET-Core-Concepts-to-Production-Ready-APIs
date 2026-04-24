using M07.DataProtection.Requests;
using M07.DataProtection.Responses;

namespace M07.DataProtection.Services;

public interface IBiddingService
{
    Task<BidResponse> CreateBidAsync(CreateBidRequest request);
    Task<BidResponse?> GetBidAsync(Guid id);
    Task<List<BidResponse>> GetAllBidsAsync();
}
