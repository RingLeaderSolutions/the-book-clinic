using System.Collections.Generic;
using System.Threading.Tasks;
using TheBookClinic.Persistence.Model;

namespace TheBookClinic.Persistence.DbContext
{
    public interface IPersistedTradeDbContext
    {
        Task<List<PersistedTrade>> GetOutstandingTrades();
    }
}