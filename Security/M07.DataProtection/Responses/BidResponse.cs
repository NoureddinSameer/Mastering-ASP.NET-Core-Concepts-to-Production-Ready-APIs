using M07.DataProtection.Entities;
using Microsoft.AspNetCore.DataProtection;

namespace M07.DataProtection.Responses;

public class BidResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime BidDate { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Telephone { get; set; }
    public string? Address { get; set; }

    public static BidResponse FromModel(Bid bid)
    {
        ArgumentNullException.ThrowIfNull(bid);

        return new BidResponse
        {
            Id = bid.Id,
            Amount = bid.Amount,
            BidDate = bid.BidDate,
            FirstName = bid.FirstName,
            LastName = bid.LastName,
            Email = bid.Email,
            Telephone = bid.Telephone,
            Address = bid.Address
        };
    }
}