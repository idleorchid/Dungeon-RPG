using Godot;
using System;

public partial class AttackHitbox : Area3D, IHitbox
{
    public bool CanStun()
    {
        return false;
    }

    public float GetDamage()
    {
        return GetOwner<Character>().GetStatResource(Stat.Strength).StatValue;
    }
}
