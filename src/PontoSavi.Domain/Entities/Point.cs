using System.Text.Json.Serialization;

namespace PontoSavi.Domain.Entities;

public class Point
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int? ManagerId { get; set; }
    public User? Manager { get; set; }

    public DateTime CheckIn { get; set; }
    public PointStatus CheckInStatus { get; set; }
    public string? CheckInDescription { get; set; }
    public DateTime? CheckOut { get; set; }
    public DateTime? CheckOutAt { get; set; }
    public PointStatus? CheckOutStatus { get; set; }
    public string? CheckOutDescription { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [JsonIgnore]
    public int CompanyId { get; set; }
    [JsonIgnore]
    public Company? Company { get; set; }
}

public enum PointStatus
{
    AutoCheck = 0,
    ManualCheck = 1,
    Pending = 2,
    Approved = 3,
    Rejected = 4
}
