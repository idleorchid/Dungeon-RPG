using Godot;
using System;

public partial class BombHitbox : Area3D, IHitbox
{
    public float GetDamage() => GetOwner<Bomb>().Damage;
}

