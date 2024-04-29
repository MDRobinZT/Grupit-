namespace teamtrack_api.Model;

public record Person {
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
}