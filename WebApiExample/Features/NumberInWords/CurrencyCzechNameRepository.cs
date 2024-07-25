using Microsoft.EntityFrameworkCore;

namespace WebApiExample.Features.NumberInWords
{
    /// <inheritdoc cref="ICurrencyCzechNameRepository"/>
    public class CurrencyCzechNameRepository(ApplicationDbContext dbContext) : ICurrencyCzechNameRepository
    {
        private readonly ApplicationDbContext _context = dbContext;

        /// <inheritdoc />
        public async Task<CurrencyCzechName?> GetCurrencyCzechNameByCodeAsync(string currencyCode)
        {
            return await _context.CurrencyCzechNames.FirstOrDefaultAsync(c => c.Code.Equals(currencyCode.ToLower()));
        }
    }
}
