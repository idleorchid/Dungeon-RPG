using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();
        destination = GetPointGlobalPosition(0);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (character.AgentNode.IsNavigationFinished())
        {
            character.StateMachineNode.SwitchState<EnemyPatrolState>();
        }

        Move();
    }

    protected override void EnterState()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);
        character.AgentNode.TargetPosition = destination;
        character.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        character.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }
}