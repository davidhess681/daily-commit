namespace GraphQLServer.Types;

[GraphQLDescription("A single audio file, usually a song.")]
public class Track
{
    [ID]
    [GraphQLDescription("The ID for the track.")]
    public required string Id { get; init; }

    [GraphQLDescription("The name of the track.")]
    public required string Name { get; set; }

    [GraphQLDescription("The track length in milliseconds.")]
    public double DurationMs { get; set; }

    [GraphQLDescription("Whether or not the track has explicit lyrics (true = yes it does; false = no it does not OR unknown)")]
    public bool Explicit { get; set; }

    [GraphQLDescription("The URI for the track, usually a Spotify link.")]
    public required string Uri { get; set; }
}
