namespace SandboxClient;

public class LogFileWorker
{
    /// <summary>
    /// Append the log into a queue that will be retrieved by a worker to write to the log file
    /// </summary>
    /// <param name="log"></param>
    public void Append(string log) { }

    /// <summary>
    /// Write all the logs in the queue into the file
    /// </summary>
    /// <param name="log"></param>
    public void Write(string log) { }
}
