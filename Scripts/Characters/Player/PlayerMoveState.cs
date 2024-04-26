using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range, "0,20,0.1")] private float speed = 5f;

    public override void _PhysicsProcess(double delta)
    {
        if (character.direction == Vector2.Zero)
        {
            character.StateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }

        character.Velocity = new(character.direction.X, 0, character.direction.Y);
        character.Velocity *= speed;

        character.MoveAndSlide();
        character.Flip();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            character.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);
    }
}
