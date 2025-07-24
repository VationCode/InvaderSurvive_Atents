using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    #region SingleTon
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    #endregion

    public Image FieldTitleImage;
    private  TextMeshProUGUI FieldName;

    [SerializeField]
    [Range(0.1f, 2f)]
    private float fadeTime;
    private Coroutine FadeCoroutine;

    [SerializeField]
    private TextMeshProUGUI ammoText;

    void Start()
    {
        FieldName = FieldTitleImage.GetComponentInChildren<TextMeshProUGUI>();
        Color color = FieldTitleImage.color;
        Color color2 = FieldName.color;
        color.a = 0f;
        color2.a = 0f;
        FieldTitleImage.color = color;
        FieldName.color = color2;
    }

   public void FadeInOut(string _fieldName)
    {
        FadeCoroutine = StartCoroutine(FadeInOut(1,0, _fieldName));
    }

    private IEnumerator FadeInOut(float _startT, float _endT, string _name)
    {
        float currentT = 0.0f;
        float percent = 0.0f;
        Color color;
        Color color2;
        FieldName.text = _name;
        while (percent <= 1.0f) //알파 최대값 1.0까지 반복 //페이드 아웃(점점 어둡게)
        {
            currentT += Time.deltaTime;
            percent = currentT / fadeTime;
            yield return null;
            color = FieldTitleImage.color;
            color2 = FieldName.color;
            color.a = percent;
            color2.a = percent;
            FieldTitleImage.color = color;
            FieldName.color = color2;
        }

        yield return new WaitForSeconds(2.0f); ////페이드 인(점점 사라지는)
        while (percent >= 0.0f)
        {
            currentT -= Time.deltaTime / fadeTime;
            percent = currentT / fadeTime;
            yield return null;
            color = FieldTitleImage.color;
            color2 = FieldName.color;
            color.a = percent;
            color2.a = percent;
            FieldTitleImage.color = color;
            FieldName.color = color2;
        }
        yield return null;
    }

    public void GunAmmo(int _ammoRemain, int _magAmmo)
    {
        ammoText.text = _ammoRemain.ToString() + " / " + _magAmmo.ToString();
    }


}
