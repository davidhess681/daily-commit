namespace GraphQLServer.Types;

public class AddItemsToPlaylistInput
{
    public AddItemsToPlaylistInput(string playlistId, List<string> uris)
    {
        PlaylistId = playlistId;
        Uris = uris;
    }

    [ID]
    [GraphQLDescription("The ID of the playlist.")]
    public string PlaylistId { get; set; }

    [GraphQLDescription("A comma-separated list of Spotify URIs to add.")]
    public List<string> Uris { get; set; }
}
