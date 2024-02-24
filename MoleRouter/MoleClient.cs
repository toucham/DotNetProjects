namespace MoleRouter;

public interface IMoleClient
{

}

public class MoleClient : IMoleClient
{
    readonly string Id;

    public MoleClient(string Id)
    {
        this.Id = Id;
    }

}