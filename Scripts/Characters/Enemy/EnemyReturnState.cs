using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{
    private Vector3 destination;

    public override void _Ready()
    {
        base._Ready();
        Vector3 localPosition = character.Path3DNode.Curve.GetPointPosition(0);
        Vector3 globalPosition = character.Path3DNode.GlobalPosition;
        destination = localPosition + globalPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (character.GlobalPosition != destination)
        {
            character.Velocity = character.GlobalPosition.DirectionTo(destination);
            character.MoveAndSlide();
        }
    }

    protected override void EnterState()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);
    }
}