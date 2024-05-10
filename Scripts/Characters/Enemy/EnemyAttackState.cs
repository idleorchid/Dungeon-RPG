using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    private Vector3 targetPosition;

    protected override void EnterState()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK);

        Node3D target = character.AttackAreaNode.GetOverlappingBodies().First();
        targetPosition = target.GlobalPosition;

        character.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        character.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        character.ToggleHitbox(true);

        Node3D target = character.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();

        if (target == null)
        {
            target = character.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();

            if (target == null)
            {
                character.StateMachineNode.SwitchState<EnemyReturnState>();
                return;
            }

            character.StateMachineNode.SwitchState<EnemyChaseState>();
            return;
        }

        character.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;

        Vector3 direction = character.GlobalPosition.DirectionTo(targetPosition);
        character.Sprite3DNode.FlipH = direction.X < 0;
    }

    private void PerformHit()
    {
        character.HitboxNode.GlobalPosition = targetPosition;
        character.ToggleHitbox(false);
    }
}
