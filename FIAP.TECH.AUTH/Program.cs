using FIAP.TECH.CORE.APPLICATION.Authentication;
using FIAP.TECH.CORE.APPLICATION.Configurations;
using FIAP.TECH.CORE.APPLICATION.Services.Doctors;
using FIAP.TECH.CORE.APPLICATION.Services.Patients;
using FIAP.TECH.CORE.APPLICATION.Settings.JwtExtensions;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

// Add methods extensions
builder.Services.AddInjectionApplication(builder.Configuration);

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddSecurity();

builder.Services.AddMassTransitExtensionWeb(builder.Configuration);
// Add DbContext
builder.Services.AddDbContextConfiguration(builder.Configuration);


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
.WithName("/doctor/login")
.WithOpenApi();

app.MapPost("/patient/login", [AllowAnonymous] async ([FromBody] AuthenticateRequestPatient request, IPatientService userService) =>
{
    var response = await userService.AuthenticatePatient(request);

    if (response is null)
        return Results.BadRequest(new { message = "CPF e/ou senha inválido(s)" });

    return Results.Ok(response);
})
.WithName("/patient/login")
.WithOpenApi();

app.Run();

public partial class ProgramAuth { }