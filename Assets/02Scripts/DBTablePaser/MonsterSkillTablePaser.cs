//**********23.01.18 : ���ʸ������ ���� MonsterBaseŬ�������� �Ļ��Ǿ� ���ͽ�ųDB(.csv)�� �����͸� �а� �Լ��� ���� �ٸ� Ŭ�������� ������ �ҷ��� �� �ִ�. - ����ȣ
//**********MonsterSkillTablePaser : ���� ��ų ������ �Ľ� Ŭ���� : ���ҽ� ������ csv�� ����� ���� �о���� Ŭ����
using UnityEngine;

public class MonsterSkillTablePaser : ParserBase<MonsterSkillTablePaser, MonsterSkillInfo>
{
    MonsterSkillInfo monsterSkillInfo;
    string monsterSkillPath = "/Resources/05DBData/MonsterSkillDB.csv";
    //List<MonsterSkillInfo> monsterSkillInfoList;
    public override void ReadData(string[] _datas) //��ǥ�� ���е�
    {
        monsterSkillInfo.monsterName = _datas[0];
        monsterSkillInfo.skillName = _datas[1];
        monsterSkillInfo.attackStartDis = int.Parse(_datas[2]);
        monsterSkillInfo.damage = int.Parse(_datas[3]);
        if (_datas[4] == "Base") monsterSkillInfo.eAttackType = EAttackTypes.Base;
        else if (_datas[4] == "Strong") monsterSkillInfo.eAttackType = EAttackTypes.Strong;
        else if (_datas[4] == "Jump") monsterSkillInfo.eAttackType = EAttackTypes.Jump;
        else if (_datas[4] == "Rush") monsterSkillInfo.eAttackType = EAttackTypes.Rush;
        else if (_datas[4] == "AOE") monsterSkillInfo.eAttackType = EAttackTypes.AOE;
        //else if(_datas[4] == "Laser") monsterSkillInfo.eAttackType = EAttackTypes.Laser;
        monsterSkillInfo.angle = int.Parse(_datas[5]);
        monsterSkillInfo.range = int.Parse(_datas[6]);
        monsterSkillInfo.rotateCount = int.Parse(_datas[7]);
        list.Add(monsterSkillInfo);
    }

    public MonsterSkillInfo GetMonsterSkillData(string _monsterName, string _name)
    {
        LoadData(Application.dataPath + monsterSkillPath);

        return list.Find(o => (o.monsterName.Equals(_monsterName) && o.skillName.Equals(_name))); //�̸��� ���� ��ų�ϼ��� �ֱ⿡ ���� �̸����ε� �Ǻ�

    }
}
