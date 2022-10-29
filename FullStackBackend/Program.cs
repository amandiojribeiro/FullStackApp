using AutoMapper;
using FullStackBackend.Data;
using FullStackBackend.Dtos;
using FullStackBackend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var sqlConBuilder = new SqlConnectionStringBuilder();
sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("SqlDbConnection");
sqlConBuilder.UserID = builder.Configuration["userId"];
sqlConBuilder.Password = builder.Configuration["password"];

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>
    (opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapGet("api/v1/employees", async(IEmployeeRepository repository, IMapper mapper) =>
{
    var employees= await repository.GetAllEmployeesAsync();
    return Results.Ok(mapper.Map<IEnumerable<EmployeeReadDto>>(employees));
});

app.MapGet("api/v1/employees/{id}", async(IEmployeeRepository repository, IMapper mapper, Guid id) =>
{
    var employee= await repository.GetEmployeeByIdAsync(id);
    if(employee != null)
        return Results.Ok(mapper.Map<EmployeeReadDto>(employee));
    
    return Results.NotFound();
});

app.MapPost("api/v1/employees", async(IEmployeeRepository repository, IMapper mapper, EmployeeCreateDto employeDto) =>
{
    var employee = mapper.Map<Employee>(employeDto);
    await repository.CreateEmployee(employee);
    await repository.SaveChangesAsync();
    var employeReadDto = mapper.Map<EmployeeReadDto>(employee);
    return Results.Created($"api/v1/commands/{employeReadDto.Id}", employeReadDto);
});

app.MapPut("api/v1/employees/{id}", async(IEmployeeRepository repository, IMapper mapper, Guid id, EmployeeUpdateDto employeDto) =>
{
    var employee = await repository.GetEmployeeByIdAsync(id);
    if(employee == null)
        return Results.NotFound();

    mapper.Map(employeDto, employee);
    await repository.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("api/v1/employees/{id}", async (IEmployeeRepository repository, IMapper mapper, Guid id) =>
{
    var employee = await repository.GetEmployeeByIdAsync(id);
    if (employee == null)
        return Results.NotFound();

    repository.Delete(employee);
    await repository.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
