using Godot;
using System;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready()
    {
        currentState.Notification(5001);
    }

    public void SwitchState<T>()
    {
        foreach (Node state in states)
        {
            if (state is T)
            {
                currentState.Notification(5002);
                currentState = state;
                currentState.Notification(5001);
                return;
            }
        }
    }
}
