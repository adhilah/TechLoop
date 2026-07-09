using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;
using TechLoop.Application.Interfaces.Infrastructure;

namespace TechLoop.Infrastructure.Data;

public class DapperContext : IDapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(
            _configuration.GetConnectionString("DefaultConnection"));
    }
}