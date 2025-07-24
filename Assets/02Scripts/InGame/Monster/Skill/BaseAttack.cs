using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    Monster monster;
    GameObject target;
    public GameObject firePos;
    float attackT;
    [SerializeField] float meleeT;
    [SerializeField] float farT;
    public float attackDis;
    float meleeAttackDis;
    float farAttackDis;
    private float T;
    private void Start()
    {
        monster = GetComponent<Monster>();
        BaseAttackInit(monster);
        
    }
    private void Update()
    {
        if (monster.isDead)
        {
            return;
        }
        Attack(monster);

    }

    public void BaseAttackInit(Monster _monster)
    {
        target = _monster.player;
        meleeT = 2;
        farT = 2;
        meleeAttackDis = 4;
        farAttackDis = 15;
       /* if (firePos != null)
        {
            firePos = transform.Find("firePos").gameObject;
        }*/
        ResourceManager.Instance.LoadrcMonsterBullet();
        if (_monster.monsterInfo.attacktType == ATTACKTYPE.Melee)
        {
            T = meleeT;
            attackDis = meleeAttackDis;
        }
        else if (_monster.monsterInfo.attacktType == ATTACKTYPE.Far)
        {
            T = farT;
            attackDis = farAttackDis;
        }
    }

    public void Attack(Monster _monster)
    {
        if (PlayerMovementManager.Instance.isDead) return;
   
        if (_monster.Discover())
        {

                _monster.gameObject.transform.LookAt(_monster.player.transform.position);
            if(Vector3.Distance(_monster.gameObject.transform.position, _monster.player.transform.position) <= attackDis) //발견상태에서 공격거리진입
            {
                attackT += Time.deltaTime;_monster.animator.SetBool("IsWalk", false);
                if (attackT >= meleeT)
                {
                    _monster.animator.SetBool("IsAttack", true);
                    _monster.animator.SetTrigger("BaseAttack");
                    attackT = 0;
                }
            }
            else if(Vector3.Distance(_monster.gameObject.transform.position, _monster.player.transform.position) > attackDis)
            {
                //Debug.Log(Vector3.Distance(_monster.gameObject.transform.position, _monster.player.transform.position) + "aaa" + attackDis);
                _monster.animator.SetBool("IsAttack", false);
                _monster.animator.SetBool("IsWalk", true);
                _monster.gameObject.transform.position = Vector3.MoveTowards(_monster.gameObject.transform.position, _monster.player.transform.position, Time.deltaTime * _monster.monsterInfo.moveSpeed);
            }
        }

        else if(!_monster.Discover())
        {
            _monster.isBaseAttack = false;
            _monster.animator.SetBool("IsAttack", false);
            _monster.SwitchState(_monster.patrol) ;
            attackT = 0;
        }
    }

    public void CreateBullet()
    {
        GameObject bullet = ResourceManager.Instance.GetrcMonsterBullet("Bullet");
        Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);
    }
}
