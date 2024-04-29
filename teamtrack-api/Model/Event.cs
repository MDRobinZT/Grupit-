namespace teamtrack_api.Model;

public record Event {
    public int Id {get; set;}
    public string? Type {get; set;}
    public DateTime Date {get; set;}
    public string? Description {get; set;}
    public string? Location {get; set;}
    public bool Attendance {get; set;}
}
