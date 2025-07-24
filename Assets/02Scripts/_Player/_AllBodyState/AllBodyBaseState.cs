//**********22.12.24
//**********AllBodyBaseState : 전신움직임에 대한 틀(추상클래스)
public abstract class AllBodyBaseState 
{
    public abstract void EnterState(PlayerMovementManager movement);

    public abstract void UpdateState(PlayerMovementManager movement);
}
