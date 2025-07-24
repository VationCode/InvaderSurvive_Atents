//**********UpperBaseState : 상반신 상태 틀(추상 클래스)
public abstract class UpperBaseState
{
    public abstract void EnterState(UpperStateManager aim);

    public abstract void UpdateState(UpperStateManager aim);
}
