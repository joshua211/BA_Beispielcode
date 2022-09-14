namespace BeispielProjekt;

public class Preis
{
    public decimal Value { get; set; }
    public Preis(decimal value)
    {
        Value = value;
    }

    public static Preis operator +(Preis a, Preis b) => new Preis(a.Value + b.Value);
    public static Preis operator -(Preis a, Preis b) => new Preis(a.Value - b.Value);
    public static bool operator ==(Preis a, Preis b) => a.Value == b.Value;
    public static bool operator !=(Preis a, Preis b) => a.Value != b.Value;
    public static bool operator >=(Preis a, Preis b) => a.Value >= b.Value;
    public static bool operator <=(Preis a, Preis b) => a.Value <= b.Value;
}
