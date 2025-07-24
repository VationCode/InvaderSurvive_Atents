//**********23.01.18 : ��ų���� ���̺� �������� - ����ȣ
//**********23.01.16 : ���ʸ������ ���� MonsterBaseŬ�������� �Ļ��Ǿ� ����DB(.csv)�� �����͸� �а� �Լ��� ���� �ٸ� Ŭ�������� ������ �ҷ��� �� �ִ�. - ����ȣ
//**********MonsterTableParser : ���� ������ �Ľ� Ŭ���� : ���ҽ� ������ csv�� ����� ���� �о���� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTableParser : ParserBase<MonsterTableParser, MonsterInfo>
{
    MonsterInfo monsterInfo;
    string monsterPath = "/Resources/05DBData/MonsterDB.csv";
    public override void ReadData(string[] _datas) //��ǥ�� ���е�
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
                monsterInfo.monsterSkillList.Add(monsterSkillInfo); //��ų ������ ���� ��������
                
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
