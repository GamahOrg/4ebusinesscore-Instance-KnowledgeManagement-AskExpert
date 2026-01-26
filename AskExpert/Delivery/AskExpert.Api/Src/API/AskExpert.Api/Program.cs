using _4EPlatform.PermissionsGroupingChecker;
using _4EPlatform.PremessionRegister;
using _4EPlatform_AskExpert.GrpClientsConfig;
using AskExpert.Application.Services;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Add services to the container.

    // Add unit of work & GRepo
    builder.Services.AddUnitOfWorkRepository();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowCors",
            builder =>
            {
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:3000")
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials();
            });
    });
    /*builder.Services.AddCorsConfig();*/
    builder.Services.AddDiServices();
    builder.Services.AddVersioning();
    builder.Services.AddDbConfig(builder.Configuration);
    builder.Services.AddUserManagementGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagementUrl"]);
    builder.Services.AddHostedService<AddPermissionsMiddleware>();
    builder.Services.AddScoped<PermissionsGroupingCheckerMiddleware, PermissionsGroupingCheckerMiddleware>();
    builder.Services.AddGrpc();
    builder.Services.AddGrpcReflection();
    builder.Services.AddRateLimitings(builder.Configuration);
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Listen(IPAddress.Any, 7180, o => o.Protocols = HttpProtocols.Http2);

        options.Listen(IPAddress.Any, 5333, o => o.Protocols = HttpProtocols.Http1);
    });
    builder.Host.UseSerilog((context, configuration) => configuration
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext().
            ReadFrom.Configuration(context.Configuration));

    builder.Services.AddAuthentication("Bearer")
          .AddJwtBearer("Bearer", options =>
          {
              // the name of your api resources   
              options.Audience = "UserManagementServer";
              /// identity server url                    
              options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
              options.RequireHttpsMetadata = false;
          });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ResourceOwnerPolicy", policy =>
        {
            policy.AddAuthenticationSchemes("Bearer");
            policy.RequireClaim("scope", "4EMicroServices"); // Require scope associated with Resource Owner Password token
        });

        options.AddPolicy("ClientCredentialsPolicy", policy =>
        {
            policy.AddAuthenticationSchemes("Bearer");
            policy.RequireClaim("scope", "CPAggregatorAPI"); // Require scope associated with Client Credentials token
        });
    });

    // Add grpc config to serviceCollection
    builder.Services.AddGrpc();
    //builder.Services.AddGrpcReflection();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    #region Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "AskExpert APIs Reference",
        });

        c.AddSecurityDefinition(
        "token",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization
        }
            );
        c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
        }
            );
    });
    #endregion



    builder.Services.AddControllers();
    builder.Services.AddFluentValidation();

    var app = builder.Build();
    app.MapGrpcService<QuestionService>();
    app.MapGrpcService<CommentService>();

    app.UseMiddleware<GlobalExceptionHandler>();
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
        RequestPath = "/wwwroot"
    });

    app.ApplyAutoMigration(builder.Configuration);

    // Add GrpcServices
    app.AddMapGrpcServices();
    app.UseStaticFiles();

    //app.UseHttpsRedirection();
    app.UseCors("AllowCors");
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    //Add support to logging request with SERILOG
    app.UseSerilogRequestLogging();
    app.UseMiddleware<PermissionsGroupingCheckerMiddleware>();

    app.Run();


}
catch (Exception)
{

    throw;
}