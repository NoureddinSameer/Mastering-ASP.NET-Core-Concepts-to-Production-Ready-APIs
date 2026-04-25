using M07.DataProtection.Data;
using M07.DataProtection.Entities;
using M07.DataProtection.Requests;
using M07.DataProtection.Responses;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace M07.DataProtection.Services;

public class BiddingService(AppDbContext context, IDataProtectionProvider protectionProvider) : IBiddingService
{
    private readonly IDataProtector _protector = protectionProvider.CreateProtector("Bidding.Protection");

    public async Task<BidResponse> CreateBidAsync(CreateBidRequest request)
    {
        var bid = new Bid
        {
            Id = Guid.NewGuid(),
            Amount = request.Amount,
            BidDate = DateTime.UtcNow,
            FirstName = _protector.Protect(request.FirstName!),
            LastName = _protector.Protect(request.LastName!),
            Email = _protector.Protect(request.Email!),
            Telephone = _protector.Protect(request.Telephone!),
            Address = _protector.Protect(request.Address!)
        };

        context.Bids.Add(bid);

        await context.SaveChangesAsync();

        return BidResponse.FromModel(bid, _protector);
    }


    public async Task<List<BidResponse>> GetAllBidsAsync()
    {
        var bids = await context.Bids
                 .OrderByDescending(b => b.BidDate)
                 .ToListAsync();

        return bids.Select(bid => BidResponse.FromModel(bid, _protector)).ToList();
    }

    public async Task<BidResponse?> GetBidAsync(Guid id)
    {
        var bid = await context.Bids.FirstOrDefaultAsync(b => b.Id == id);

        if (bid == null)
            return null;

        return BidResponse.FromModel(bid, _protector);
    }
}