public struct ResourcesStruct
{
    Resource food;
    Resource gold;
    Resource wood;
    Resource stone;
    Resource iron;

    public Resource Food { get => food; set => food = value; }
    public Resource Gold { get => gold; set => gold = value; }
    public Resource Wood { get => wood; set => wood = value; }
    public Resource Stone { get => stone; set => stone = value; }
    public Resource Iron { get => iron; set => iron = value; }

    public ResourcesStruct(float _f, float _w, float _g, float _s, float _i)
    {
        food = new Resource(_f);
        gold = new Resource(_g);
        wood = new Resource(_w);
        stone = new Resource(_s);
        iron = new Resource(_i);
    }

    void add(ResourcesStruct rs)
    {
        Food.add(rs.Food);
        Gold.add(rs.Gold);
        Wood.add(rs.Wood);
        Stone.add(rs.Stone);
        Iron.add(rs.Iron);
    }
}

public class Resource
{
    float income;
    float spending;
    float stock;

    public float Income { get => income; set => income = value; }
    public float Spending { get => spending; set => spending = value; }
    public float Stock { get => stock; set => stock = value; }


    public Resource(float _s)
    {
        income = 0f;
        spending = 0f;
        stock = _s;
    }
    public Resource( float _i, float _sp)
    {
        income = _i;
        spending = _sp;
        stock = 0f;
    }
    public Resource(float _i, float _sp, float _st)
    {
        income = _i;
        spending = _sp;
        stock = _st;
    }

    public void add(Resource r){
        Income += r.Income;
        Spending += r.Spending;
        Stock += r.Stock;
    }
    
}

public enum ResourceType
{
    Food, Gold, Wood, Stone, Iron
}