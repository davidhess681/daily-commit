using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.ApolloFederation.Types;
using SpotifyWeb;

namespace GraphQLServer.Types;

public class Recipe
{
    public Recipe(string id, string? name)
    {
        Id = id;
        Name = name;
    }

    [Key]
    [ID]
    public string Id { get; }

    [External]
    public string? Name { get; }

    [ReferenceResolver]
    public static Recipe GetRecipeById(string id, string name)
    {
        return new Recipe(id, name);
    }

    [Requires("name")]
    [GraphQLDescription("A list of recommended playlists for this particular recipe. Returns 1 to 3 playlists.")]
    public async Task<List<Playlist>> RecommendedPlaylists([Service] SpotifyService spotifyService)
    {
        var response = await spotifyService.SearchAsync(
            Name,
            [SearchType.Playlist],
            3,
            0,
            null);

        return response.Playlists.Items
            .Select(item => new Playlist(item))
            .ToList();
    }
}
