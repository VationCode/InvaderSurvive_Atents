using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;


[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    #region SingleTon
    public static SoundManager Instance;
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

    [SerializeField]
    private int audioSourceCount;
    [Header("중복 효과음을 위한 오디오소스배열")]
    public AudioSource[] audioSourceEffects; //효과음 하나당 오디오소스 하나씩 필요하기에 중복 효과음들 , 발걸음, 총소리, 이펙트 등등 같이 나오기위해
    
    [Header("BGM 오디오소스")]
    public AudioSource audioSourceBGM; //브금한 하나로만 틀어주면 되기에

    [Tooltip("AudioEffects와 갯수 동일하게")]
    public string[] playSoundName; //특정 사운드만 끄고 싶을때

    [Header("효과음")]
    public Sound[] effectSounds;
    [Header("브금")]
    public Sound[] bgmSounds;

    [SerializeField]
    [Range(0f, 1f)]
    private float BGMVolme; 

    /*[SerializeField]
    [Range(-100,100)]
    private float[] EffectVolmes;
    private AudioMixer[] EffectVolume;*/

    private void Start()
    {
        audioSourceBGM = gameObject.AddComponent<AudioSource>();
        audioSourceBGM.loop = true;
        for (int i = 0; i < audioSourceCount; i++)
        {
            this.gameObject.AddComponent<AudioSource>();
        }
        BGMVolme = 0.1f;
    }
    private void Update()
    {
        audioSourceBGM.volume = BGMVolme;
    }
    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if(_name == effectSounds[i].name) //사운드 이름 검색 있다면
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)//곡을 재생시켜야하는데 일단 사용할 오디오소스를 찾기위해 사용하지 않고 잇는 오디오소스를 사용
                {
                    if(!audioSourceEffects[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name; //현재 재생중인 사운드들(사용중인 오디오소스) 정보 가지고 있고 차후 특정 사운드만 끄고 싶을때 사용
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다."); //부족하면 늘려줄것
                return;
            }
        }

        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if(_name == bgmSounds[i].name)
            {
                audioSourceBGM.clip = bgmSounds[i].clip;
                audioSourceBGM.Play();
                return;
            }
        }
        Debug.Log("등록된 AudioClip이 없습니다.");
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if(playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                break;
            }
        }
        Debug.Log("중지 시킬" + _name + "사운드가 없습니다.");
    }
}
