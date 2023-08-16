using RamsoftServer.Domain.Entities;
using RamsoftServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamsoftServerTests.Mocks
{
    internal class ColumnRepositoryMock : IColumnRepository
    {

        public List<Column> Columns;

        public ColumnRepositoryMock(List<Column> columns)
        {
            Columns = columns;
        }

        public List<Column> GetColumns()
        {
            return Columns.ToList();
        }
    }
}
