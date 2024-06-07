namespace PontoSavi.Domain.Entities;

public class UserSettings
{
    public int UserId { get; set; }
    public User? User { get; set; }

    public TimeOnly CheckIn { get; set; }
    public int CheckInToleranceMinutes { get; set; }
    public TimeOnly CheckOut { get; set; }
    public int CheckOutToleranceMinutes { get; set; }
}
