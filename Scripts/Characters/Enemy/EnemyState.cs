using System;
using Godot;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 destination;

    public override void _Ready()
    {
        base._Ready();

        character.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    protected Vector3 GetPointGlobalPosition(int index)
    {
        Vector3 localPosition = character.Path3DNode.Curve.GetPointPosition(index);
        Vector3 globalPosition = character.Path3DNode.GlobalPosition;
        return localPosition + globalPosition;
    }

    protected void Move()
    {
        character.AgentNode.GetNextPathPosition();
        character.Velocity = character.GlobalPosition.DirectionTo(destination);
        character.MoveAndSlide();

        character.Flip();
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        character.StateMachineNode.SwitchState<EnemyChaseState>();
    }

    private void HandleZeroHealth()
    {
        character.StateMachineNode.SwitchState<EnemyDeathState>();
    }
}