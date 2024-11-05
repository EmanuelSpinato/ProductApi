public class Asset
{
    public int Id { get; set; }
    public string Type { get; set; } // "image" ou "video"
    public string Url { get; set; }
}

public class Attribute
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; } 
    public string Value { get; set; }
}