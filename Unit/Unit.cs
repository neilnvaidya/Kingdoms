public class Unit
{
    int health;
    int damage;
    int movementSpeed;
    public Unit(int _health, int _damage, int _movementSpeed)
    {
        health = _health;
        damage = _damage;
        movementSpeed = _movementSpeed;
    }
}

public class Warrior : Unit
{
    public Warrior(int _health, int _damage, int _movementSpeed) : base(_health, _damage, _movementSpeed) { }
}

public class Archer : Unit
{
    public Archer(int _health, int _damage, int _movementSpeed) : base(_health, _damage, _movementSpeed) { }
}
public class Champion : Unit
{
    public Champion(int _health, int _damage, int _movementSpeed) : base(_health, _damage, _movementSpeed) { }
}

public static class UnitConstants
{

}