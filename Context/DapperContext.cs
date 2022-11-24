using System.Data;
using Microsoft.Data.SqlClient;

namespace ToDoApi.Context;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectString;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectString = _configuration.GetConnectionString("Default");
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectString);
}
