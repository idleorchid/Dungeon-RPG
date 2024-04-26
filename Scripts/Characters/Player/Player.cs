using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer animationPlayer;
    [Export] public Sprite3D sprite3D;
    [Export] public StateMachine stateMachine;

    public Vector2 direction = new();

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD,
            GameConstants.INPUT_MOVE_BACKWARD
        );
    }

    public void Flip()
    {
        bool isMovingHorizontally = Velocity.X != 0;
        if (isMovingHorizontally)
        {
            bool isMovingLeft = Velocity.X < 0;
            sprite3D.FlipH = isMovingLeft;
        }
    }
}
