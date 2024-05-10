using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{

    [Export] private Timer idleTimerNode;
    [Export(PropertyHint.Range, "0,20,0.1")]
    private float maxIdleTime = 4;
    private int pointIndex = 0;


    protected override void EnterState()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);

        pointIndex = 1;

        destination = GetPointGlobalPosition(pointIndex);
        character.AgentNode.TargetPosition = destination;

        character.AgentNode.NavigationFinished += HandleNavigationFinished;
        idleTimerNode.Timeout += HandleTimeout;
    }

    protected override void ExitState()
    {
        character.AgentNode.NavigationFinished -= HandleNavigationFinished;
        idleTimerNode.Timeout -= HandleTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (idleTimerNode.IsStopped())
        {
            Move();
        }

    }

    private void HandleNavigationFinished()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new();
        idleTimerNode.WaitTime = rng.RandfRange(0, maxIdleTime);
        idleTimerNode.Start();
    }

    private void HandleTimeout()
    {
        character.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);
        pointIndex = Mathf.Wrap(pointIndex + 1, 0, character.Path3DNode.Curve.PointCount);

        destination = GetPointGlobalPosition(pointIndex);
        character.AgentNode.TargetPosition = destination;
    }
}
