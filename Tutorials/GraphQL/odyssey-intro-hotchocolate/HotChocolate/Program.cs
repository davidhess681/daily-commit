using GraphQLServer.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<Playlist>();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(builder => builder
    .WithOrigins("https://studio.apollographql.com")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.MapGraphQL();

app.Run();
