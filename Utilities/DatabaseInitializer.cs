using System.IO;
using Microsoft.EntityFrameworkCore;
using TreasuryManagementAPI.Data;

namespace TreasuryManagementAPI.Utilities
{
    public class DatabaseInitializer
    {
        private readonly TreasuryDbContext _context;

        public DatabaseInitializer(TreasuryDbContext context)
        {
            _context = context;
        }

        public void ApplySqlScript(string scriptPath)
        {
            var sql = File.ReadAllText(scriptPath);
            _context.Database.ExecuteSqlRaw(sql);
        }
    }
}
