using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimer;
    [Export] private Timer cooldownTimer;
    [Export(PropertyHint.Range, "0,20,0.1")] private float speed = 10f;

    [Export] private PackedScene bombScene;

    public override void _Ready()
    {
        base._Ready();
        dashTimer.Timeout += HandleDashTimeout;
        CanTransition = () => cooldownTimer.IsStopped();
    }

    public override void _PhysicsProcess(double delta)
    {
        character.MoveAndSlide();
        character.Flip();
    }

    private void HandleDashTimeout()
    {
        cooldownTimer.Start();
        character.Velocity = Vector3.Zero;
        character.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    protected override void EnterState()
    {
        base.EnterState();

        character.AnimationPlayerNode.Play(GameConstants.ANIM_DASH);
        character.Velocity = new(character.direction.X, 0, character.direction.Y);

        if (character.Velocity == Vector3.Zero)
        {
            character.Velocity = character.Sprite3DNode.FlipH ? Vector3.Left : Vector3.Right;
        }

        character.Velocity *= speed;
        dashTimer.Start();

        Node3D bomb = bombScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(bomb);
        bomb.GlobalPosition = character.GlobalPosition;
    }
}
