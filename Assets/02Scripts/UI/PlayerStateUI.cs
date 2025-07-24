using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    public int character_Lv;
    public int[] needExp;
    public int currentEXP;

    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;

    public int recover_hp;
    public int recover_mp;

    public Slider hpSlider;
    public Slider mpSlider;
    void Start()
    {
        currentHP = PlayerMovementManager.Instance.playerInfo.hp;
        currentMP = PlayerMovementManager.Instance.playerInfo.mana;

    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.maxValue = maxHP;
        mpSlider.maxValue = maxMP;

        hpSlider.value = PlayerMovementManager.Instance.playerInfo.hp;
        mpSlider.value = PlayerMovementManager.Instance.playerInfo.mana;
    }
}
