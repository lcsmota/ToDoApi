using ToDoApi.Context;
using ToDoApi.Contracts;
using ToDoApi.Repository;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSingleton<DapperContext>();
    builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
    builder.Services.AddScoped<IPersonRepository, PersonRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}