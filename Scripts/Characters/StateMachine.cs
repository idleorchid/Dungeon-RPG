using Godot;
using System;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready()
    {
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>()
    {
        foreach (Node state in states)
        {
            if (state is T)
            {
                currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
                currentState = state;
                currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
                return;
            }
        }
    }
}
