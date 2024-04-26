using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (character.direction != Vector2.Zero)
        {
            character.stateMachine.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            character.stateMachine.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        character.animationPlayer.Play(GameConstants.ANIM_IDLE);
    }
}
