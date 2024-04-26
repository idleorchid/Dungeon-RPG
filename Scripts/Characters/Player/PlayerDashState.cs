using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimer;
    [Export] private float speed = 10f;

    public override void _Ready()
    {
        base._Ready();
        dashTimer.Timeout += HandleDashTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        character.MoveAndSlide();
        character.Flip();
    }

    private void HandleDashTimeout()
    {
        character.Velocity = Vector3.Zero;
        character.stateMachine.SwitchState<PlayerIdleState>();
    }

    protected override void EnterState()
    {
        base.EnterState();

        character.animationPlayer.Play(GameConstants.ANIM_DASH);
        character.Velocity = new(character.direction.X, 0, character.direction.Y);

        if (character.Velocity == Vector3.Zero)
        {
            character.Velocity = character.sprite3D.FlipH ? Vector3.Left : Vector3.Right;
        }

        character.Velocity *= speed;
        dashTimer.Start();
    }
}
