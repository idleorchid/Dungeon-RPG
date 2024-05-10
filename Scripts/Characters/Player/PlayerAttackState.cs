using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboTimer;
    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();
        comboTimer.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK + comboCounter, customSpeed: 1.5f);

        character.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        character.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
        comboTimer.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter = Mathf.Wrap(comboCounter + 1, 1, maxComboCount + 1);

        character.StateMachineNode.SwitchState<PlayerIdleState>();
        character.ToggleHitbox(true);
    }

    private void PerformHit()
    {
        float distanceMultiplier = .75f;
        Vector3 newPosition = character.Sprite3DNode.FlipH ? Vector3.Left : Vector3.Right;

        character.HitboxNode.Position = newPosition * distanceMultiplier;
        character.ToggleHitbox(false);
    }
}
