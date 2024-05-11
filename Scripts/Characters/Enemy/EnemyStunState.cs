using Godot;
using System;

public partial class EnemyStunState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();
        character.AnimationPlayerNode.Play(GameConstants.ANIM_STUN);
        character.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        character.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        if (character.AttackAreaNode.HasOverlappingBodies())
        {
            character.StateMachineNode.SwitchState<EnemyAttackState>();
        }
        else if (character.ChaseAreaNode.HasOverlappingBodies())
        {
            character.StateMachineNode.SwitchState<EnemyChaseState>();
        }
        else
        {
            character.StateMachineNode.SwitchState<EnemyIdleState>();
        }
    }
}
