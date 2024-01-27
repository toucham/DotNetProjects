using SandboxServer.Models;

namespace SandboxServer.Services.Interfaces;

public interface IMessageService
{
    public void Add(string body);
    public void Add(Message msg);
    public Message Get(int index);
    public IList<Message> GetAll();
    public Message? Remove(int index);

    int Count { get; }
}