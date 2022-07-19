using Godot;
using System;


// USING DOD.

public class Map : Node2D
{
    //Private
    Tile[] tiles;
    string[] tilesDisplay; // Display for tiles for debug purposes
    TileMap affiliationMap;
    TileMap featureMap;
    Vector2 endPointPosition;
    Tile hoveredTile;
    Vector2 mouseLocalPos;
    Vector2 mouseDeltaPos;


    //Public
    public Tile[] Tiles { get => tiles; set => tiles = value; }
    [Export] public string[] TilesDisplay { get => tilesDisplay; set => tilesDisplay = value; }
    [Export] public Vector2 EndPointPosition { get => endPointPosition; set => endPointPosition = value; }


    //HoveredTileChanged
    public delegate void HoveredTileChangedHandler(Tile t);
    public event HoveredTileChangedHandler HoverTileChangedEvent;

    public override void _EnterTree()
    {
        base._EnterTree();
    }

    public override void _Ready()
    {
        EndPointPosition = GetNode<Node2D>("EndPointMarker").Position;

        //Get References
        affiliationMap = GetNode<TileMap>("AffiliationMap");
        featureMap = GetNode<TileMap>("FeatureMap");

        //Make the tile array 
        MakeTiles();


    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }

    //Make tiles -> Assumes a affinity and feature tilemap is populated.
    void MakeTiles()
    {
        tiles = new Tile[Constants.TileCount];
        TilesDisplay = new String[Constants.TileCount];

        for (int y = 0, i = 0; y < Constants.MapSize; y++)
        {
            for (int x = 0; x < Constants.MapSize; x++, i++)
            {
                //NOTE : When changing this see about using other constructor for tile.
                Feature f = new Feature((FeatureType)featureMap.GetCell(x, y));
                Affiliation a = (Affiliation)affiliationMap.GetCell(x, y);
                tiles[i] = new Tile(x, y, f, a);
                tilesDisplay[i] = tiles[i].AsString();
            }
        }
    }

    //Get index in tile array from x,y 
    int getTileIndex(int _x, int _y)
    {
        int i = _x + Constants.MapSize * _y;

        bool isValid = i >= 0 && i < Constants.TileCount;
        return isValid ? i : 0;
    }

    //Get tile from globalPosition
    Tile getTile(Vector2 globalPos)
    {
        int _x = Mathf.FloorToInt(globalPos.x / Constants.TileSize);
        int _y = Mathf.FloorToInt(globalPos.y / Constants.TileSize);
        return tiles[getTileIndex(_x, _y)];
    }

    // Subscribed method for GlobalMousePositionChangedEvent from Map
    public void OnGlobalMousePositionChanged(Vector2 pos)
    {
        mouseLocalPos = pos;
        Tile t = getTile(pos);
        if (t != hoveredTile) OnRaiseHoveredTileChangedEvent(t);
    }

    // Delegate Method for HoveredTileChangedEvent
    void OnRaiseHoveredTileChangedEvent(Tile t)
    {
        hoveredTile = t;
        HoverTileChangedEvent?.Invoke(t);
    }

    void HandleSettlmentEstablishedEvent(Tile sender, Settlement s)
    {

    }
}

public class Tile
{
    //Private
    Vector2 position; //location of tile in worldspace
    Coord coords;
    Feature feature; // feature that appears on the tile, can only have one
    int movementCost; //total movementcost to move ONTO this tile
    Affiliation affiliation; // which kingdom owns this tile.
    float development;
    int stability;
    float taxes;
    Settlement settlement;


    //Public
    public Coord Coords { get => coords; set => coords = value; }
    public Feature Feature { get => feature; set => feature = value; }
    public int MovementCost { get => movementCost; set => movementCost = value; }
    public Affiliation Affiliation { get => affiliation; set => affiliation = value; }
    public Vector2 Position { get => position; set => position = value; }
    public float Development { get => development; set => SetDevelopment(value); }
    public int Stability { get => stability; set => stability = value; }
    public float Taxes { get => taxes; set => taxes = value; }
    public Settlement Settlement { get => settlement; set => settlement = value; }

    public delegate void SettlementEstablishedHandler(object sender, Settlement s);
    public event SettlementEstablishedHandler SettlementEstablishedEvent;

    public Tile(int _x, int _y, Feature _feature, Affiliation _affliation)
    {
        coords = new Coord(_x, _y);
        feature = _feature;
        movementCost = _feature.movementCost + 1;
        affiliation = _affliation;
        position = new Vector2(_x, _y) * Constants.TileSize;
        development = 0;
        stability = 0;
        taxes = 0f;
        settlement = null;
    }

    public Tile(int _x, int _y)
    {
        coords = new Coord(_x, _y);
        feature = new Feature(FeatureType.None);
        movementCost = feature.movementCost + 1;
        affiliation = Affiliation.None;
        position = new Vector2(_x, _y) * Constants.TileSize;
        development = 0;
        stability = 0;
        taxes = 0f;
        settlement = null;
    }

    void SetDevelopment(float value)
    {
        bool shouldEstablishSettlment = (settlement == null && development <1  && value > 1);

        if (shouldEstablishSettlment) EstablishSettlement();

        development = value;


    }

    void EstablishSettlement()
    {
        settlement = new Settlement(1);
    }

    void OnRaiseEstablishSettlementEvent(Settlement s)
    {
        SettlementEstablishedEvent?.Invoke(this, s);
    }
    //Returns combined string of properties.
    public string AsString()
    {
        return coords.AsString() + ", " + affiliation.ToString() + ", " + feature.type.ToString();
    }

    //Returns name of tile
    public string GetName()
    {
        return "Tile " + coords.AsString();
    }

    //Comparison Overrides
    public static bool operator ==(Tile t1, Tile t2)
    {
        return (t1.Coords.X == t2.Coords.X && t1.Coords.Y == t2.Coords.Y);
    }
    public static bool operator !=(Tile t1, Tile t2)
    {
        return !(t1 == t2);
    }

    public override bool Equals(object o)
    {
        return this.GetHashCode() == o.GetHashCode();
    }

    public override int GetHashCode()
    {
        return GetHashCode();
    }
}

public class Settlement
{
    string name;
    int size;
    int population;

    public Settlement(int _size)
    {
        name = NameGenerator.GetRandomName();
        size = _size;
    }
    public string Name { get => name; set => name = value; }
    public int Size { get => size; set => SetSize(value); }

    public string AsString()
    {
        return name + " (" + size.ToString() + ")";
    }

    void SetSize(int value)
    {
        size = value;
    }
}


public static class NameGenerator
{
    public static string[] SettlementNames = new string[]{
        "Aerilon", "Aquarin", "Aramoor", "Azmar",
        "Begger’s Hole","Black Hollow", "Blue Field", "Briar Glen",
                "Brickelwhyte","Broken Shield", "Boatwright", "Bullmar",
        "Carran", "City of Fire","Coalfell", "Cullfield",
        "Darkwell", "Deathfall", "Doonatel","Dry Gulch",
        "Easthaven", "Ecrin", "Erast",
        "Far Water","Firebend", "Fool’s March", "Frostford",
        "Goldcrest","Goldenleaf", "Greenflower", "Garen’s Well",
        "Haran","Hillfar", "Hogsfeet", "Hollyhead", "Hull", "Hwen",
        "Icemeet", "Ironforge", "Irragin",
        "Jarren’s Outpost","Jongvale",
        "Kara’s Vale", "Knife’s Edge",
        "Lakeshore","Leeside", "Lullin",
        "Marren’s Eve", "Millstone", "Moonbright","Mountmend",
        "Nearon", "New Cresthill", "Northpass","Nuxvar",
        "Oakheart", "Oar’s Rest", "Old Ashton","Orrinshire", "Ozryn",
        "Pavv", "Pella’s Wish", "Pinnella Pass","Pran",
        "Quan Ma", "Queenstown",
        "Ramshorn", "Red Hawk","Rivermouth",
        "Saker Keep", "Seameet", "Ship’s Haven","Silverkeep",
                "South Warren", "Snake’s Canyon", "Snowmelt",
                "Squall’s End", "Swordbreak",
        "Tarrin", "Three Streams","Trudid",
        "Ubbin Falls", "Ula’ree",
        "Veritas", "Violl’s Garden",
        "Wavemeet", "Whiteridge", "Willowdale", "Windrip","Wintervale",
                "Wellspring", "Westwend", "Wolfden",
        "Xan’s Bequest", "Xynnar",
        "Yarrin", "Yellowseed",
        "Zao Ying", "Zeffari", "Zumka"};

    public static string GetRandomName()
    {
        Random rng = new Random();
        int index = rng.Next(SettlementNames.Length);
        return SettlementNames[index];
    }


}
public static class Constants
{
    public static int MapSize = 20;
    public static int TileCount = MapSize * MapSize;
    public static float TileSize = 64f;

    public static int WoodsTileID = 0;
    public static int MountainsTileID = 1;

    public static int AllyTileID = 0;
    public static int EnemyTileID = 1;
    public static int WaterTileID = 2;

    public static Texture[] SettlementTextures = new Texture[]{
        null,
        GD.Load<Texture>("res://Map/Tile/Settlement/Settlement_1_texture.png"),
        GD.Load<Texture>("res://Map/Tile/Settlement/Settlement_2_texture.png"),
        GD.Load<Texture>("res://Map/Tile/Settlement/Settlement_3_texture.png"),
        GD.Load<Texture>("res://Map/Tile/Settlement/Settlement_4_texture.png"),
        GD.Load<Texture>("res://Map/Tile/Settlement/Settlement_5_texture.png")
    };
}


public struct Feature
{
    public int movementCost;
    public FeatureType type;

    public Feature(FeatureType t)
    {
        if (t == FeatureType.Woods)
        {
            type = FeatureType.Woods;
            movementCost = 1;
        }
        else if (t == FeatureType.Mountain)
        {
            type = FeatureType.Mountain;
            movementCost = 2;
        }
        else { type = FeatureType.None; movementCost = 0; }
    }
}

public enum FeatureType
{
    None = -1,
    Woods = 0,
    Mountain = 1,
}
public enum Affiliation
{
    None = -1,
    Ally = 0,
    Enemy = 1,
    Water = 2

}