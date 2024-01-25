using GrpcServer.API.Service;
using GrpcServer.API.Service.gRPC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ProdutoService>();
builder.Services.AddGrpc();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Total",
        builder =>
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("Total");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<ProdutoGrpcService>().RequireCors("Total");
});

app.Run();
