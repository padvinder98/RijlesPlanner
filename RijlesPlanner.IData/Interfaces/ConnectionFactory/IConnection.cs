using System.Data;

namespace RijlesPlanner.IData.Interfaces.ConnectionFactory
{
    public interface IConnection
    {
        public IDbConnection GetConnection { get; }
    }
}
