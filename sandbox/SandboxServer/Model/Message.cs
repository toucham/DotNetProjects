namespace SandboxServer.Models
{
    public class Message
    {
        public string? Body { get; set; }

        // must have getter method for the field to be serialized
        public DateTime CreatedTime { get; } = DateTime.Now;

    }
}