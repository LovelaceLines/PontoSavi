namespace PontoSavi.API.ViewModels;

public class UserSettingsVM
{
    public string UserPublicId { get; set; } = null!;

    public TimeOnly CheckIn { get; set; }
    public int CheckInToleranceMinutes { get; set; }
    public TimeOnly CheckOut { get; set; }
    public int CheckOutToleranceMinutes { get; set; }
}
