using Godot;
using System;

public partial class EnemyAttackState : EnemyState
{
    protected override void EnterState()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK);
    }
}
