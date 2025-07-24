//**********23.01.07
//**********BossBaseState : 전신움직임에 대한 틀(추상클래스)

public abstract class Boss_BaseState
{
    public abstract void EnterState(BossManager boss);

    public abstract void UpdateState(BossManager boss);
}
