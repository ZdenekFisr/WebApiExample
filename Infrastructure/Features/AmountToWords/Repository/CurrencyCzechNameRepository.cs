using Application.Features.AmountToWords;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.AmountToWords.Repository
{
    /// <inheritdoc cref="ICurrencyCzechNameRepository"/>
    public class CurrencyCzechNameRepository(
        ApplicationDbContext dbContext)
        : ICurrencyCzechNameRepository
    {
        private readonly ApplicationDbContext _context = dbContext;

        /// <inheritdoc />
        public async Task<CurrencyCzechNameModel?> GetCurrencyCzechNameByCodeAsync(string currencyCode)
        {
            CurrencyCzechName? entity = await _context.CurrencyCzechNames.FirstOrDefaultAsync(c => c.Code.Equals(currencyCode.ToLower()));
            if (entity is null)
                return null;

            return CurrencyCzechNameModel.FromEntity(entity);
        }
    }
}
