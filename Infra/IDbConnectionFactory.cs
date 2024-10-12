using System.Data;

namespace RunningTracker.Infra
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
