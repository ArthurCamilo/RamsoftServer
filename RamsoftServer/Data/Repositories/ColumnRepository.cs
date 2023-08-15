using RamsoftServer.Domain.Entities;
using RamsoftServer.Interfaces;

namespace RamsoftServer.Infrastructure.Repositories
{
    public class ColumnRepository : IColumnRepository
    {

        private readonly DatabaseContext _db;

        public ColumnRepository(DatabaseContext context)
        {
            _db = context;
        }

        public List<Column> GetColumns()
        {
            return _db.Columns.ToList();
        }
    }
}
