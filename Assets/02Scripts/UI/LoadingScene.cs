//**********22.12.10 : ���� ������ �Ѿ �� �����ִ� ��(�Ϻη� �ð��� �־� �ε�ȭ��ó�� ������ ������ �ٷ� �Ѿ�� ����(���� ���� ���� �� ���� �ʿ�
//**********LoadingScene : �ε� �� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Boot,
    Lobby,
    InGame
}

public class LoadingScene : MonoBehaviour
{
    static string nextScene; //�ٸ� Ŭ�������� �ҷ�����

    [SerializeField]
    Image progressBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene(SceneType.Boot.ToString());
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess()); //�ε������� �Ѿ������ �ڵ����� �ε�����
    }

    IEnumerator LoadSceneProcess()
    {
        //����Ȳ�� LoadSceneAsync�Լ��� AsyncOperation�� ��ȯ����
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); //�Ϲ� �ε�� �ϰԵǸ� ���� �ҷ��ö����� �ƹ��͵� �������� Async(�񵿱�)�� �ҷ����ԵǸ� �ҷ������߿� �ٸ��۾��� ����
        
        //���� ������ �ڵ����� �ҷ��� ������ �̵��Ұ����� ����, false�� �ϰԵǸ� 90%���� ��� ���� true�� �����ϸ� ���� �κ� �����ϰ� ���Ѿ
        //true���ص� ��������� �ʹ� ������ ����Ǹ� ����� ���� �����͸� �ҷ����Ⱑ �����⵵���� �ҷ����� �����͵��� ������ ���̰ԵǱ⿡ ������
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone) //���ε��� ������ �ʾ�����
        {
            yield return null;
            if(op.progress < 0.9f) //90���������� ��� ä���
            {
                progressBar.fillAmount = op.progress;
            }
            else //���ĺ��ʹ� 1�ʸ��� ������ ä���� �� �ҷ���
            {
                timer += Time.unscaledTime/1000f;
                progressBar.fillAmount = Mathf.Lerp(0.9f,1f,timer);
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
