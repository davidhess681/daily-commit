using SpotifyWeb;

namespace GraphQLServer.Types;

public class Query
{
    [GraphQLDescription("Playlists hand-picked to be featured to all users.")]
    public async Task<List<Playlist>> FeaturedPlaylistsAsync([Service] SpotifyService spotifyService)
    {
        var response = await spotifyService.GetFeaturedPlaylistsAsync();

        return response.Playlists.Items
            .Select(Map)
            .ToList();
    }

    [GraphQLDescription("Retrieves a specific playlist.")]
    public async Task<Playlist?> GetPlaylistAsync([ID] string id, [Service] SpotifyService spotifyService)
    {
        var response = await spotifyService.GetPlaylistAsync(id);
        return Map(response);
    }

    private static Playlist Map(PlaylistSimplified playlist)
    {
        return new Playlist(playlist.Id, playlist.Name)
        {
            Description = playlist.Description
        };
    }

    private static Playlist Map(SpotifyWeb.Playlist playlist)
    {
        var tracks = playlist.Tracks.Items
            .Select(x => new Track()
            {
                Id = x.Track.Id,
                Name = x.Track.Name,
                DurationMs = x.Track.Duration_ms,
                Explicit = x.Track.Explicit,
                Uri = x.Track.Uri
            }).ToList();

        return new Playlist(playlist.Id, playlist.Name)
        {
            Description = playlist.Description,
            Tracks = tracks
        };
    }
}
