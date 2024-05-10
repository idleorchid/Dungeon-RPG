using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    private CharacterBody3D target;
    [Export] private Timer chaseTimer;

    protected override void EnterState()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);
        target = character.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;

        chaseTimer.Timeout += HandleChaseTimerTimeout;
        character.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
        character.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        chaseTimer.Timeout -= HandleChaseTimerTimeout;
        character.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
        character.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    private void HandleChaseTimerTimeout()
    {
        destination = target.GlobalPosition;
        character.AgentNode.TargetPosition = destination;
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        character.StateMachineNode.SwitchState<EnemyAttackState>();
    }

    private void HandleChaseAreaBodyExited(Node3D body)
    {
        character.StateMachineNode.SwitchState<EnemyReturnState>();
    }
}
