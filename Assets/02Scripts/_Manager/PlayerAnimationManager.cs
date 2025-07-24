using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    #region SingleTon
    public static PlayerAnimationManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    #endregion


    public Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        
    }

    public void Move(float x, float y)
    {
        animator.SetFloat("HorizontalMove", x, 0.05f, Time.deltaTime);
        animator.SetFloat("VerticalMove", y, 0.05f, Time.deltaTime);
    }

    public void GunAttack()
    {

    }

    public void Aim()
    {

    }

    public void Jump()
    {

    }


    public void GunSkill()
    {
        if (PlayerInput.Instance.isDodge)
        {

        }
        else if (PlayerInput.Instance.isSkillQ)
        {

        }
        else if (PlayerInput.Instance.isSkillE)
        {

        }
    }

    public void SwordAttack()
    {


    }

    public void SwordSkill()
    {
        if (PlayerInput.Instance.isDodge)
        {

        }
        else if (PlayerInput.Instance.isSkillQ)
        {

        }
        else if (PlayerInput.Instance.isSkillE)
        {

        }
    }

}
