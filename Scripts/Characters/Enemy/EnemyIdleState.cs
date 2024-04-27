using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
    protected override void EnterState()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);
    }

    public override void _PhysicsProcess(double delta)
    {
        character.StateMachineNode.SwitchState<EnemyReturnState>();
    }
}
