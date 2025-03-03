using SpotifyWeb;

namespace GraphQLServer.Types;

public class Mutation
{
    [GraphQLDescription("Add one or more items to a user's playlist.")]
    public async Task<AddItemsToPlaylistPayload> AddItemsToPlaylist(
        AddItemsToPlaylistInput input,
        [Service] SpotifyService spotifyService,
        [Service] ILogger<Mutation> logger)
    {
        try
        {
            string tracksCsv = string.Join(',', input.Uris);
            
            await spotifyService.AddTracksToPlaylistAsync(input.PlaylistId, null, tracksCsv);

            var response = await spotifyService.GetPlaylistAsync(input.PlaylistId);
            var playlist = new Playlist(response);

            logger.LogInformation("Playlist {id} updated", playlist.Id);

            return new AddItemsToPlaylistPayload(
                200,
                true,
                "Successfully added items to playlist.",
                playlist);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new AddItemsToPlaylistPayload(500, false, ex.Message, null);
        }
    }
}
