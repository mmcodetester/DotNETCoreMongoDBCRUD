using DotNETCoreMongoDBCRUD.Context;
using DotNETCoreMongoDBCRUD.Repository;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register MongoDB context and repository
builder.Services.Configure<DatabaseSetting>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<DatabaseSetting>(serviceProvider =>
        serviceProvider.GetRequiredService<IOptions<DatabaseSetting>>().Value);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
    }
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
