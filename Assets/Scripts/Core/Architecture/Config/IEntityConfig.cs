public interface IEntityConfig
{
    float WalkSpeed { get; }
    float SprintMultiplier { get; }
    float JumpForce { get; }
    float GravityMultiplier { get; }
    float MouseSensitivity { get; }
    float UpDownLimit { get; }
    float MoveTurnTreshold { get; }
    int Health { get; }
}