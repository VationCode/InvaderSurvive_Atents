//**********23.01.04 : ApplyDamage함수로 Gun정보 전달 역할 - 허인호 
//**********IDamageabel : Gun정보 전달 인터페이스
public interface IDamageabel
{
    bool ApplyDamage(DamageMessage damageMessage);
}
