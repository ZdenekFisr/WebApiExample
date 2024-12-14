using Application.Features.AmountToWords;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.AmountToWords.Repository
{
    /// <inheritdoc cref="ICurrencyCzechNameRepository"/>
    public class CurrencyCzechNameRepository(
        ApplicationDbContext dbContext,
        IMapper mapper) : ICurrencyCzechNameRepository
    {
        private readonly ApplicationDbContext _context = dbContext;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc />
        public async Task<CurrencyCzechNameModel?> GetCurrencyCzechNameByCodeAsync(string currencyCode)
        {
            CurrencyCzechName? currencyCzechName = await _context.CurrencyCzechNames.FirstOrDefaultAsync(c => c.Code.Equals(currencyCode.ToLower()));
            if (currencyCzechName is null)
                return null;

            return _mapper.Map<CurrencyCzechNameModel>(currencyCzechName);
        }
    }
}
