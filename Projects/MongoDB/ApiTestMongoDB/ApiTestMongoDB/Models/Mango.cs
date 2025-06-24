namespace ApiTestMongoDB.Models;

public class Mango
{
    public string Id { get; set; }

    public int WeightGrams { get; set; }

    public string FarmName { get; set; }

    public DateTime SellBy { get; set; }

    public bool Organic { get; set; }
}
