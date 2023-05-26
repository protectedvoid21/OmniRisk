namespace OmniRiskAPI.Models; 

public class Alert {
    public int Id { get; set; }
    public string? Comment { get; set; }
    
    public int AlertTypeId { get; set; }
    public AlertType AlertType { get; set; }
    
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}