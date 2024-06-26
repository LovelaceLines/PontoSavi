namespace PontoSavi.Domain.Entities;

public class DayOff : Base
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
}
