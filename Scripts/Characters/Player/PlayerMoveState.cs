using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (character.direction == Vector2.Zero)
        {
            character.stateMachine.SwitchState<PlayerIdleState>();
            return;
        }

        character.Velocity = new(character.direction.X, 0, character.direction.Y);
        character.Velocity *= 5;

        character.MoveAndSlide();
        character.Flip();
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
        character.animationPlayer.Play(GameConstants.ANIM_MOVE);
    }
}
