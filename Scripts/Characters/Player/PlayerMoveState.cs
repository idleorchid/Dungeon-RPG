using Godot;
using System;

public partial class PlayerMoveState : Node
{
    private Player character;

    public override void _Ready()
    {
        character = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == 5001)
        {
            character.animationPlayer.Play(GameConstants.ANIM_MOVE);
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == 5002)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }

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
}
