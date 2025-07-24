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
    [Header("�ߺ� ȿ������ ���� ������ҽ��迭")]
    public AudioSource[] audioSourceEffects; //ȿ���� �ϳ��� ������ҽ� �ϳ��� �ʿ��ϱ⿡ �ߺ� ȿ������ , �߰���, �ѼҸ�, ����Ʈ ��� ���� ����������
    
    [Header("BGM ������ҽ�")]
    public AudioSource audioSourceBGM; //����� �ϳ��θ� Ʋ���ָ� �Ǳ⿡

    [Tooltip("AudioEffects�� ���� �����ϰ�")]
    public string[] playSoundName; //Ư�� ���常 ���� ������

    [Header("ȿ����")]
    public Sound[] effectSounds;
    [Header("���")]
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
            if(_name == effectSounds[i].name) //���� �̸� �˻� �ִٸ�
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)//���� ������Ѿ��ϴµ� �ϴ� ����� ������ҽ��� ã������ ������� �ʰ� �մ� ������ҽ��� ���
                {
                    if(!audioSourceEffects[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name; //���� ������� �����(������� ������ҽ�) ���� ������ �ְ� ���� Ư�� ���常 ���� ������ ���
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("��� ���� AudioSource�� ������Դϴ�."); //�����ϸ� �÷��ٰ�
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
        Debug.Log("��ϵ� AudioClip�� �����ϴ�.");
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
        Debug.Log("���� ��ų" + _name + "���尡 �����ϴ�.");
    }
}
