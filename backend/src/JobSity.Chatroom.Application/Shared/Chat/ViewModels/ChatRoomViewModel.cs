namespace JobSityNETChallenge.Application.ViewModels
{
    public class ChatRoomViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
