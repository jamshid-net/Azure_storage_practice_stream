namespace AngularApp2.Server;

public class CustomSettings
{
    public string Name { get; set; } 

    public InnerValue Value { get; set; }
}

public class InnerValue
{
    public string Val1 { get; set; }
    public string Val2 { get; set; }
    public string Val3 { get; set; }
}
