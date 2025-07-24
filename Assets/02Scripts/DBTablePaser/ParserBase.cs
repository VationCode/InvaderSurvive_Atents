//**********23.01.16 : ���ʸ������ ���� ��ӵ� Ŭ�������� �ε��� �����͵��� �������� ��ӹ��� Ŭ������ ReadData�Լ� �����ǿ� �ҷ��� �����Լ��� ���� ���ָ�ȴ�. - ����ȣ
//**********ParserBase : �Ľ� Ŭ���� : ���ҽ� ������ csv�� ����� ���� �о���� Ŭ������ ���̽����ȴ�.
using System.Collections.Generic;
using System.IO;

public class ParserBase<T, Q> where T : class, new() where Q : struct //T Ŭ�������� �Ű��������� �����ڷ� ����ü(��������)�� Q�� ����
{
    #region SingleTon
    private static T instance;
    public static T Instatnce
    {
        get
        {
            if (instance == null) instance = new T();
            return instance;
        }
    }
    #endregion

    public List<Q> list; //Q�� ����� �� ���� ��� �����͸� �޾ƿ��� �𸣱⿡ ���ʸ����� ���

    public void LoadData(string _fileName)
    {
        list = new List<Q>();
        using (StreamReader sr = new StreamReader(_fileName))
        {
            string line = string.Empty;
            line = sr.ReadLine(); //�÷� ���
            while((line = sr.ReadLine()) != null) //���� �����ʹ� �״��� �÷����ͱ⿡
            {
                string[] datas = line.Split(',');
                ReadData(datas);
            }
            sr.Close();
        }
    }

    public virtual void ReadData(string[] _datas)
    {

    }
}
