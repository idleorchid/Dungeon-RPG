using Godot;
using System;
using System.Linq;

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
        Node state = states.Where(x => x is T).FirstOrDefault();
        if (state == null) return;

        currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        currentState = state;
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
