using FIAP.TECH.CORE.APPLICATION.Authentication;
using FIAP.TECH.CORE.APPLICATION.Configurations;
using FIAP.TECH.CORE.APPLICATION.Services.Doctors;
using FIAP.TECH.CORE.APPLICATION.Services.Patients;
using FIAP.TECH.CORE.APPLICATION.Settings.JwtExtensions;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using FIAP.TECH.INFRASTRUCTURE.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add methods extensions
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPatientRepository, UserRepository>();

// Add DbContext
builder.Services.AddDbContextConfiguration(builder.Configuration);

builder.Services.AddSecurity();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/doctor/login", [AllowAnonymous] async ([FromBody] AuthenticateRequestDoctor request, IDoctorService userService) =>
{
    var response = await userService.AuthenticateDoctor(request);

    if (response is null)
        return Results.BadRequest(new { message = "CRM e/ou senha inválido(s)" });

    return Results.Ok(response);
})
.WithName("login")
.WithOpenApi();

app.MapPost("/patient/login", [AllowAnonymous] async ([FromBody] AuthenticateRequestPatient request, IPatientService userService) =>
{
    var response = await userService.AuthenticatePatient(request);

    if (response is null)
        return Results.BadRequest(new { message = "CPF e/ou senha inválido(s)" });

    return Results.Ok(response);
})
.WithName("login")
.WithOpenApi();

app.Run();

public partial class ProgramAuth { }