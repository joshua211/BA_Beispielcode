namespace Beispielprojekt;

public class BestellungsId
{
    public string Value { get; private set; }

    public BestellungsId(string value)
    {
        Value = value;
    }
    public static BestellungsId Neu()
    {
        return new BestellungsId(Guid.NewGuid().ToString());
    }
}