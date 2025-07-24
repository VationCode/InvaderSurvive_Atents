//**********22.12.10 : 다음 씬으로 넘어갈 시 보여주는 씬(일부러 시간을 주어 로딩화면처럼 구성함 원래는 바로 넘어갈수 있음(차후 서버 연결 시 조정 필요
//**********LoadingScene : 로딩 씬 클래스
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
    static string nextScene; //다른 클래스에서 불러오게

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
        StartCoroutine(LoadSceneProcess()); //로딩씬으로 넘어왔을때 자동으로 로딩동작
    }

    IEnumerator LoadSceneProcess()
    {
        //씬상황을 LoadSceneAsync함수가 AsyncOperation로 반환해줌
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); //일반 로드로 하게되면 씬을 불러올때까지 아무것도 못하지만 Async(비동기)로 불러오게되면 불러오는중에 다른작업이 가능
        
        //씬이 끝나면 자동으로 불러온 씬으로 이동할것인지 설정, false로 하게되면 90%에서 대기 이후 true로 변경하면 남은 부분 진행하고 씬넘어감
        //true로해도 상관없지만 너무 빠르게 진행되면 번들과 같은 데이터를 불러오기가 끝나기도전에 불러오면 데이터들이 깨져서 보이게되기에 제어함
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone) //씬로딩이 끝나지 않았을때
        {
            yield return null;
            if(op.progress < 0.9f) //90퍼전까지는 계속 채우고
            {
                progressBar.fillAmount = op.progress;
            }
            else //이후부터는 1초마다 나머지 채운후 씬 불러옴
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
