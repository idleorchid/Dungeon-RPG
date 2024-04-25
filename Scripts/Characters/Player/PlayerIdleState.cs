using Godot;
using System;

public partial class PlayerIdleState : Node
{
    private Player character;

    public override void _Ready()
    {
        character = GetOwner<Player>();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == 5001)
        {
            character.animationPlayer.Play(GameConstants.ANIM_IDLE);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (character.direction != Vector2.Zero)
        {
            character.stateMachine.SwitchState<PlayerMoveState>();
        }
    }
}
