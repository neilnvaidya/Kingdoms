public struct Coord
{
    int x;
    int y;
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }

    public Coord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    
    public string AsString()
    {
        return "(" + x.ToString() + ", " + y.ToString() + ")";
    }
}
