using GraphQLServer.Types;
using SpotifyWeb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddApolloFederation()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<Recipe>();

builder.Services.AddCors();
builder.Services.AddLogging();
builder.Services.AddHttpLogging(_ => { });

builder.Services.AddDistributedMemoryCache();

builder.Services.AddClientCredentialsTokenManagement()
    .AddClient("spotify.token", client =>
    {
        client.TokenEndpoint = "https://accounts.spotify.com/api/token";
        client.ClientId = builder.Configuration["Spotify:ClientId"];
        client.ClientSecret = builder.Configuration["Spotify:ClientSecret"];
        client.Scope = "app-remote-control playlist-read-private playlist-read-collaborative playlist-modify-public playlist-modify-private user-library-read user-library-modify user-read-private user-read-email user-follow-read user-follow-modify user-top-read user-read-playback-position user-read-playback-state user-read-recently-played user-read-currently-playing user-modify-playback-state ugc-image-upload streaming";
    });

builder.Services.AddHttpClient<SpotifyService>()
    .AddClientCredentialsTokenHandler("spotify.token");

var app = builder.Build();

app.UseCors(builder => builder
    .WithOrigins("https://studio.apollographql.com")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
