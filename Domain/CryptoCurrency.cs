namespace Domain;

public class CryptoCurrency
{
    public int Id { get; set; }
    public string? Symbol { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}