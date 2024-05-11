using System;
using Godot;

public abstract partial class PlayerState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();

        character.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }


    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            character.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }

    private void HandleZeroHealth()
    {
        character.StateMachineNode.SwitchState<PlayerDeathState>();
        GameEvents.RaiseEndGame();
    }
}