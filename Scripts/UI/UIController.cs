using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private bool canPause = false;
    private Dictionary<ContainerType, UIContainer> containers;

    public override void _Ready()
    {
        containers = GetChildren().Where(x => x is UIContainer)
            .Cast<UIContainer>()
            .ToDictionary(x => x.Container);

        containers[ContainerType.Start].Visible = true;
        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;

        containers[ContainerType.Pause].ButtonNode.Pressed += HandleContinuePressed;
    }

    public override void _Input(InputEvent @event)
    {
        if (canPause && Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            containers[ContainerType.Stats].Visible = GetTree().Paused;
            GetTree().Paused = !GetTree().Paused;
            containers[ContainerType.Pause].Visible = GetTree().Paused;
        }
    }

    private void HandleStartPressed()
    {
        canPause = true;
        GetTree().Paused = false;

        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;

        GameEvents.RaiseStartGame();
    }

    private void HandleContinuePressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Pause].Visible = false;
        containers[ContainerType.Stats].Visible = true;
    }

    private void HandleEndGame()
    {
        canPause = false;
        containers[ContainerType.Defeat].Visible = true;
        containers[ContainerType.Stats].Visible = false;
    }

    private void HandleVictory()
    {
        canPause = false;
        containers[ContainerType.Victory].Visible = true;
        containers[ContainerType.Stats].Visible = false;
        GetTree().Paused = true;
    }
}
