using Godot;

public abstract partial class PlayerState : CharacterState
{
    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            character.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
}