using Godot;
using System;

public partial class PlayerDashState : Node
{
    private Player character;
    [Export] private Timer dashTimer;

    public override void _Ready()
    {
        character = GetOwner<Player>();
        dashTimer.Timeout += HandleDashTimeout;
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == 5001)
        {
            character.animationPlayer.Play(GameConstants.ANIM_DASH);
            dashTimer.Start();
        }
    }

    private void HandleDashTimeout()
    {
        character.stateMachine.SwitchState<PlayerIdleState>();
    }
}
