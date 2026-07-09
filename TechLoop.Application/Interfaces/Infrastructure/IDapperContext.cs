using System.Data;

namespace  TechLoop.Application.Interfaces.Infrastructure;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}