using System;
using System.Linq;
using Godot;

public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] stats;
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimationPlayerNode { get; private set; }
    [Export] public Sprite3D Sprite3DNode { get; private set; }
    [Export] public StateMachine StateMachineNode { get; private set; }
    [Export] public Area3D HurtboxNode { get; private set; }
    [Export] public Area3D HitboxNode { get; private set; }
    [Export] public CollisionShape3D HitboxShapeNode { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Path3D Path3DNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }

    public Vector2 direction = new();

    public override void _Ready()
    {
        HurtboxNode.AreaEntered += HandleHurtboxEntered;
    }

    public void Flip()
    {
        bool isMovingHorizontally = Velocity.X != 0;
        if (isMovingHorizontally)
        {
            bool isMovingLeft = Velocity.X < 0;
            Sprite3DNode.FlipH = isMovingLeft;
        }
    }

    private void HandleHurtboxEntered(Area3D area)
    {
        StatResource health = GetStatResource(Stat.Health);

        Character player = area.GetOwner<Character>();
        health.StatValue -= player.GetStatResource(Stat.Strength).StatValue;

        GD.Print(health.StatValue);
    }

    public StatResource GetStatResource(Stat stat)
    {
        return stats.Where(x => x.StatType == stat).FirstOrDefault();
    }

    public void ToggleHitbox(bool flag)
    {
        HitboxShapeNode.Disabled = flag;
    }
}