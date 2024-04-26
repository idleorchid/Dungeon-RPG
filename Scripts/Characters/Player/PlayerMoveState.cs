using Godot;
using System;

public partial class PlayerMoveState : Node
{

    private Player character;

    public override void _Ready()
    {
        character = GetOwner<Player>();
        SetPhysicsProcess(false);
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == 5001)
        {
            character.animationPlayer.Play(GameConstants.ANIM_MOVE);
            SetPhysicsProcess(true);
        }
        else if (what == 5002)
        {
            SetPhysicsProcess(false);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (character.direction == Vector2.Zero)
        {
            character.stateMachine.SwitchState<PlayerIdleState>();
        }
    }
}
