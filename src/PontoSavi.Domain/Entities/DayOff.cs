using System.Text.Json.Serialization;

namespace PontoSavi.Domain.Entities;

public class DayOff
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [JsonIgnore]
    public int CompanyId { get; set; }
    [JsonIgnore]
    public Company? Company { get; set; } = null;
}
