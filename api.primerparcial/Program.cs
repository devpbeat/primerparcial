using api.primerparcial.Data;
using api.primerparcial.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");

builder.Services.AddDbContext<PrimerParcialApp>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/cliente/", async (Cliente cliente, PrimerParcialApp db) =>
{
    db.Clientes.Add(cliente);
    await db.SaveChangesAsync();

    return Results.Created($"/cliente/{cliente.id}", cliente);
});

app.MapGet("/cliente/{id:int}", async (int id, PrimerParcialApp db) =>
{
    return await db.Clientes.FindAsync(id) is Cliente cliente ? Results.Ok(cliente) : Results.NotFound();
});

app.MapGet("/cliente/", async (PrimerParcialApp db) =>
{
    var cliente = await db.Clientes.ToListAsync();
    return  cliente is not null? Results.Ok(cliente) : Results.NotFound();
});

app.MapPut("/cliente/{id}", async (int id, Cliente cliente, PrimerParcialApp db) =>
{
    var cli = await db.Clientes.FindAsync(id);

    if (cli is null) return Results.NotFound();

    cli.id = cliente.id;
    cli.idCiudad = cliente.idCiudad;
    cli.Nombres = cliente.Nombres;
    cli.Apellidos = cliente.Apellidos;
    cli.Documento = cliente.Documento;
    cli.Telefono = cliente.Telefono;
    cli.Email = cliente.Email;
    cli.FechaNacimiento = cliente.FechaNacimiento;
    cli.Ciudad = cliente.Ciudad;
    cli.Nacionalidad = cliente.Nacionalidad;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/cliente/{id}", async (int id, PrimerParcialApp db) =>
{
    if (await db.Clientes.FindAsync(id) is Cliente cli)
    {
        db.Clientes.Remove(cli);
        await db.SaveChangesAsync();
        return Results.Ok(cli);
    }

    return Results.NotFound();
});




app.MapPost("/ciudad/", async (Ciudad ciudad, PrimerParcialApp db) =>
{
    db.Ciudades.Add(ciudad);
    await db.SaveChangesAsync();

    return Results.Created($"/ciudad/{ciudad.Id}", ciudad);
});

app.MapGet("/ciudad/", async (PrimerParcialApp db) =>
{
    var ciudad = await db.Ciudades.ToListAsync();
    return ciudad is not null ? Results.Ok(ciudad) : Results.NotFound();
});

app.MapGet("/ciudad/{Id:int}", async (int Id, PrimerParcialApp db) =>
{
    return await db.Ciudades.FindAsync(Id) is Ciudad ciudad ? Results.Ok(ciudad) : Results.NotFound();
});

app.MapPut("/ciudad/{Id}", async (int Id, Ciudad ciudad, PrimerParcialApp db) =>
{
    var ciu = await db.Ciudades.FindAsync(Id);
    Console.WriteLine(ciudad);
    if (ciu is null) return Results.NotFound();

    ciu.Id = ciudad.Id;
    ciu.ciudad = ciudad.ciudad;
    ciu.Estado = ciudad.Estado;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/ciudad/{Id}", async (int Id, PrimerParcialApp db) =>
{
    if (await db.Ciudades.FindAsync(Id) is Ciudad ciu)
    {
        db.Ciudades.Remove(ciu);
        await db.SaveChangesAsync();
        return Results.Ok(ciu);
    }

    return Results.NotFound();
});


app.Run();