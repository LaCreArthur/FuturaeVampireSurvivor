public interface ICollidable
{
    public CollisionType CollisionType { get; }
}

public enum CollisionType
{
    Enemy,
    Ally,
}
