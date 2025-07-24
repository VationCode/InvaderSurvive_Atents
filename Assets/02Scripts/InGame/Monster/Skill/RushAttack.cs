using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RushAttack : MonoBehaviour
{

    Monster monster;
    GameObject player;
    NavMeshAgent nav;

    bool isLook;
    bool isWalk;
    bool isRush;
    bool isWakeUp;

    int Action;
    float t;
    
    Vector3 lookVec;
    Vector3 lookPos;

    void Start()
    {
        monster = GetComponent<Monster>();
        player = PlayerMovementManager.Instance.playerObj;
        nav = GetComponent<NavMeshAgent>();
        nav.isStopped = true;
        nav.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (monster.isDead || PlayerMovementManager.Instance.isDead)
        {
            StopAllCoroutines();
            return;
        }

        if (monster.Discover())
        {
            monster.animator.SetBool("IsWakeUp", true);
            if (!isWakeUp) //한번만 부르기
            {
                t += Time.deltaTime;
                if (t > 2)
                {
                    StartCoroutine(State());
                    isWakeUp = true;
                    t = 0;
                }
                isLook = true;
            }
        }
        if (isLook)
        {
            float h = PlayerMovementManager.Instance.moveDirection.x;
            float v = PlayerMovementManager.Instance.moveDirection.y;
            lookVec = new Vector3(h, 0, v);
            lookPos = player.transform.position;
            transform.LookAt(player.transform.position + lookVec);
        }
    }

    IEnumerator State()
    {
        yield return new WaitForSeconds(0.1f);

        if (Vector3.Distance(transform.position, player.transform.position) < 4) //공격 범위에
        {
            isWalk = false;
            isRush = false;
            nav.isStopped = true;
            nav.enabled = false;
            Action = 0;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > 10) //러쉬
        {
            isRush = true;
            isWalk = false;
            Action = 1;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) >= 4 && Vector3.Distance(transform.position, player.transform.position) <= 10) //걸어서 접근
        {
            isWalk = true;
            isRush = false;
            Action = 2;
        }

        switch (Action)
        {
            case 0:
                StartCoroutine(BaseAttack());
                break;
            case 1:
                StartCoroutine(MobRushAttack());
                break;
            case 2:
                StartCoroutine(Tracking());
                break;
        }
    }

    IEnumerator MobRushAttack()
    {
        isLook = false;
        nav.enabled = true;
        nav.isStopped = false;
        nav.stoppingDistance = 1;
        monster.animator.SetBool("IsWalk", isWalk);
        monster.animator.SetBool("IsRush", true);
        nav.speed = 20;
        yield return new WaitForSeconds(0.5f);
        nav.SetDestination(lookPos);
        yield return new WaitForSeconds(2f);
        monster.animator.SetBool("IsRush", false);
        isLook = true;

        StartCoroutine(State());

    }
    IEnumerator Tracking()
    {
        if(!PlayerMovementManager.Instance.isDead)
        {
            nav.enabled = true;
            nav.isStopped = false;
            nav.speed = 2f;
            nav.stoppingDistance = 3;
            monster.animator.SetBool("IsWalk", isWalk);
            monster.animator.SetBool("IsRush", isRush);
            nav.SetDestination(player.transform.position);
            yield return null;
            StartCoroutine(State());
        }
    }
    IEnumerator BaseAttack()
    {
        isLook = false;
        monster.animator.SetBool("IsWalk", isWalk);
        monster.animator.SetBool("IsRush", isRush);
        monster.animator.SetTrigger("BaseAttack");
        yield return new WaitForSeconds(2f);
        isLook = true;
        StartCoroutine(State());
    }

}
