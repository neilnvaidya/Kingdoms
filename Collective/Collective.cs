using Godot;
using System;
using System.Collections.Generic;
public class Collective
{
    Ruler ruler;
    public Collective()
    {
    }
}

public class Nation : Collective
{
    int population;
    ResourcesStruct resources;
    Province[] Provinces;
    public Nation()
    {
    }
}


public class Province
{
    string name;
    ResourcesStruct resources;
    List<Tile> tiles;
}
