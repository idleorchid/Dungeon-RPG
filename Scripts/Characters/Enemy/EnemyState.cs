using Godot;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 destination;

    protected Vector3 GetPointGlobalPosition(int index)
    {
        Vector3 localPosition = character.Path3DNode.Curve.GetPointPosition(index);
        Vector3 globalPosition = character.Path3DNode.GlobalPosition;
        return localPosition + globalPosition;
    }

    protected void Move()
    {
        character.AgentNode.GetNextPathPosition();
        character.Velocity = character.GlobalPosition.DirectionTo(destination);
        character.MoveAndSlide();
    }
}