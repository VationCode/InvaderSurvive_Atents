using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_optionMenu;

    // StartBtn Event
    public void GoInGame()
    {
        LoadingScene.LoadScene(SceneType.InGame.ToString());
    }

    public void ActivateOptionMenu()
    {
        m_optionMenu.SetActive(!m_optionMenu.activeSelf);
    }

#if UNITY_EDITOR
    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
#endif
}
