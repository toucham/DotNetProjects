using System.Collections.Concurrent;
using DICSharpDev.Lib;
using SandboxServer.Models;
using SandboxServer.Services.Interfaces;

namespace SandboxServer.Services;

[DILifetime(ServiceLifetime.Singleton)]
public class MessageService : IMessageService
{
    private ConcurrentQueue<Message> _messages;
    public MessageService()
    {
        _messages = new();
        _messages.Append(new Message { Body = "hi" });
        _messages.Append(new Message { Body = "how are you doing" });
        _messages.Append(new Message { Body = "i am doing fine" });
        _messages.Append(new Message { Body = "bye" });
    }
    int IMessageService.Count => _messages.Count;

    public void Add(string body)
    {
        _messages.Enqueue(new Message { Body = body });
    }
    public void Add(Message msg)
    {
        _messages.Enqueue(msg);
    }

    public Message Get(int index) => _messages.ElementAt(index);


    public IList<Message> GetAll() => _messages.ToList();

    public Message? Remove(int index)
    {
        _messages.TryDequeue(out var msg);
        return msg;
    }
}
