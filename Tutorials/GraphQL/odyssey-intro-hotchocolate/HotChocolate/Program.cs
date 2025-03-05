using GraphQLServer.Types;
using SpotifyWeb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddApolloFederation()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();
builder.Services.AddCors();
builder.Services.AddLogging();
builder.Services.AddHttpLogging(_ => { });

builder.Services.AddHttpClient<SpotifyService>();

var app = builder.Build();

app.UseCors(builder => builder
    .WithOrigins("https://studio.apollographql.com")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
