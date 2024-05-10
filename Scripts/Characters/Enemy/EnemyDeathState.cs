using Godot;
using System;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_DEATH);

        character.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        character.QueueFree();
    }
}
