using Godot;
using System;

public partial class PlayerMoveState : Node
{
    public override void _Ready()
    {
        Player character = GetOwner<Player>();
        character.animationPlayer.Play(GameConstants.ANIM_MOVE);
    }
}
