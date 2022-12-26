using System;

[AttributeUsage(AttributeTargets.Field)]
public class CityNameAttribute : Attribute
{
    public string Name { get; set; }

    public CityNameAttribute(string name)
    {
        Name = name;
    }
}

public enum Cities
{
    [CityName("Chicago")]
    Chicago,

    [CityName("Houston")]
    Houston,

    [CityName("Los Angeles")]
    LosAngeles,

    [CityName("New York")]
    NewYork,

    [CityName("Philadelphia")]
    Philadelphia,

    [CityName("Phoenix")]
    Phoenix,

    [CityName("San Antonio")]
    SanAntonio,

    [CityName("San Diego")]
    SanDiego,


}
