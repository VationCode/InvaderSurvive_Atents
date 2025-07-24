//**********23.01.16 : 제너릭방식을 통해 상속된 클래스에게 로드할 데이터들을 가져오며 상속받은 클래스는 ReadData함수 재정의와 불러올 데이함수만 정의 해주면된다. - 허인호
//**********ParserBase : 파싱 클래스 : 리소스 폴더의 csv에 저장된 파일 읽어오는 클래스의 베이스가된다.
using System.Collections.Generic;
using System.IO;

public class ParserBase<T, Q> where T : class, new() where Q : struct //T 클래스에서 매개변수없는 생성자로 구조체(제약조건)인 Q를 받음
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

    public List<Q> list; //Q는 사용할 때 선언 어떠한 데이터를 받아올지 모르기에 제너릭으로 사용

    public void LoadData(string _fileName)
    {
        list = new List<Q>();
        using (StreamReader sr = new StreamReader(_fileName))
        {
            string line = string.Empty;
            line = sr.ReadLine(); //컬럼 명들
            while((line = sr.ReadLine()) != null) //실제 데이터는 그다음 컬럼부터기에
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
