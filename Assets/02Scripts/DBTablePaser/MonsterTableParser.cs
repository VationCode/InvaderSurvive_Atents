//**********23.01.18 : 스킬정도 테이블 가져오기 - 허인호
//**********23.01.16 : 제너릭방식을 통해 MonsterBase클래스에서 파생되어 몬스터DB(.csv)의 데이터를 읽고 함수를 통해 다른 클래스에서 정보를 불러올 수 있다. - 허인호
//**********MonsterTableParser : 몬스터 데이터 파싱 클래스 : 리소스 폴더의 csv에 저장된 파일 읽어오는 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTableParser : ParserBase<MonsterTableParser, MonsterInfo>
{
    MonsterInfo monsterInfo;
    string monsterPath = "/Resources/05DBData/MonsterDB.csv";
    public override void ReadData(string[] _datas) //쉼표로 구분된
    {
        monsterInfo.id = int.Parse(_datas[0]);
        monsterInfo.name = _datas[1];
        monsterInfo.hp = int.Parse(_datas[2]);
        monsterInfo.moveSpeed = float.Parse(_datas[3]);
        monsterInfo.type = (MONSTERTYPE)System.Enum.Parse(typeof(MONSTERTYPE), _datas[4]);
        monsterInfo.attacktType = (ATTACKTYPE)System.Enum.Parse(typeof(ATTACKTYPE), _datas[5]);

        monsterInfo.monsterSkillList = new List<MonsterSkillInfo>();
        
        for (int i = 6; i < 10; i++)
        {
            if(_datas[i] != "null")
            {
                MonsterSkillInfo monsterSkillInfo = new MonsterSkillInfo();
                monsterSkillInfo = MonsterSkillTablePaser.Instatnce.GetMonsterSkillData(monsterInfo.name, _datas[i]);
                monsterInfo.monsterSkillList.Add(monsterSkillInfo); //스킬 데이터 정보 가져오기
                
            }
        }
        list.Add(monsterInfo);
    }

    public MonsterInfo GetMonsterData(string _name)
    {
        LoadData(Application.dataPath + monsterPath);

        return list.Find(o => (o.name.Equals(_name)));
    }
}
