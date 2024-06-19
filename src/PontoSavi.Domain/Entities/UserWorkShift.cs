using System.Text.Json.Serialization;

namespace PontoSavi.Domain.Entities;

public class UserWorkShift
{
    public int UserId { get; set; }
    public User? User { get; set; }
    public int WorkShiftId { get; set; }
    public WorkShift? WorkShift { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [JsonIgnore]
    public int CompanyId { get; set; }
    [JsonIgnore]
    public Company? Company { get; set; }
}
