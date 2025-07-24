//**********23.01.18 : 제너릭방식을 통해 MonsterBase클래스에서 파생되어 몬스터스킬DB(.csv)의 데이터를 읽고 함수를 통해 다른 클래스에서 정보를 불러올 수 있다. - 허인호
//**********MonsterSkillTablePaser : 몬스터 스킬 데이터 파싱 클래스 : 리소스 폴더의 csv에 저장된 파일 읽어오는 클래스
using UnityEngine;

public class MonsterSkillTablePaser : ParserBase<MonsterSkillTablePaser, MonsterSkillInfo>
{
    MonsterSkillInfo monsterSkillInfo;
    string monsterSkillPath = "/Resources/05DBData/MonsterSkillDB.csv";
    //List<MonsterSkillInfo> monsterSkillInfoList;
    public override void ReadData(string[] _datas) //쉼표로 구분된
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

        return list.Find(o => (o.monsterName.Equals(_monsterName) && o.skillName.Equals(_name))); //이름이 같은 스킬일수도 있기에 몬스터 이름으로도 판별

    }
}
