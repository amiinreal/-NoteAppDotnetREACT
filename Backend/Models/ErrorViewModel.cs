namespace NoteApp.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string Message { get; set; } = "An error occurred.";
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
