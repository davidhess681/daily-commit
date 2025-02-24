namespace GraphQLServer.Types
{
    public class Artist
    {
        [ID]
        public string Id { get; }

        public string Name { get; set; }

        public int? Followers { get; set; }

        public double? Popularity { get; set; }
    }
}
