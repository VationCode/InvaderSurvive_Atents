using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossAttack : MonoBehaviour
{
    Monster monster;
    GameObject player;

    public Collider jumpAttackArea;
    NavMeshAgent nav;

    ParticleSystem laserParticle;

    bool isLook;
    bool isWalk;
    bool isWakeUp;

    int ranAction;
    float t;

    Vector3 lookVec;
    Vector3 lookPos;
   
    void Start()
    {
        monster = GetComponent<Monster>();
        player = PlayerMovementManager.Instance.playerObj;
        nav = GetComponent<NavMeshAgent>();
        ResourceManager.Instance.LoadrcTexture();
        ResourceManager.Instance.LoadrcParticle();
        nav.isStopped = true;
        nav.enabled = false;

        jumpAttackArea.enabled = false;
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
                if(t > 6)
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

        if (Vector3.Distance(transform.position, player.transform.position) < 6) //공격 범위에
        {
            isWalk = false;
            nav.isStopped = true;
            nav.enabled = false;
            ranAction = Random.Range(0, 5);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > 17) //점프로 접근 혹은 레이저
        {
            isWalk = false;
            ranAction = Random.Range(5, 10);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) >= 6 && Vector3.Distance(transform.position, player.transform.position) <= 17) //걸어서 접근
        {
            isWalk = true;
            ranAction = 10;
        }

        switch (ranAction)
        {
            case 0:
            case 1:
                StartCoroutine(BaseAttack()); //0~2까지일때는 일반 공격 (이렇게 확률 배분)
                break;
            case 2:
            case 3:
            case 4:
                StartCoroutine(StrongAttack());
                break;
            case 5:
            case 6:
            case 7:
                StartCoroutine(JumpAttack());
                break;
            case 8:
            case 9:
                StartCoroutine(LaserAttack());
                break;
            case 10:
                StartCoroutine(Walking());
                break;
        }





    }

    IEnumerator Walking()
    {
        //transform.LookAt(player.transform);

            nav.enabled = true;
            nav.isStopped = false;
            nav.speed = 10f;
            nav.stoppingDistance = 6;
            monster.animator.SetBool("IsWalk", isWalk);
            nav.SetDestination(player.transform.position);
        yield return null;
        StartCoroutine(State());
    }
    IEnumerator BaseAttack()
    {

        isLook = false;
        monster.animator.SetBool("IsWalk", isWalk);
        monster.animator.SetTrigger("BaseAttack");
        yield return new WaitForSeconds(4f);
        isLook = true;
        StartCoroutine(State());
    }
    IEnumerator StrongAttack()
    {
        isLook = false;
        monster.animator.SetBool("IsWalk", isWalk);
        monster.animator.SetTrigger("StrongAttack");
        yield return new WaitForSeconds(4f);
        isLook = true;
        StartCoroutine(State());
    }
    IEnumerator JumpAttack()
    {
        //yield return new WaitForSeconds(6f);
        //tauntVec = player.transform.position + lookVec;
        isLook = false;
        nav.enabled = true;
        nav.isStopped = false;
        nav.stoppingDistance = 1;
        monster.animator.SetBool("IsWalk", isWalk);
        monster.animator.SetTrigger("JumpAttack");
        nav.speed = 20;
        yield return new WaitForSeconds(0.5f);
        nav.SetDestination(lookPos);
        yield return new WaitForSeconds(0.5f);
        jumpAttackArea.enabled = true;
        yield return new WaitForSeconds(0.5f);
        jumpAttackArea.enabled = false;
        yield return new WaitForSeconds(2f);
        isLook = true;

        StartCoroutine(State());
    }
    IEnumerator LaserAttack()
    {
        isLook = false;
        nav.enabled = false;
        CreateDangerLine();
        monster.animator.SetBool("IsWalk", isWalk);
        monster.animator.SetTrigger("LaserAttack");
        yield return new WaitForSeconds(4f);
        isLook = true;
        StartCoroutine(State());
    }

    void CreateDangerLine()
    {
        GameObject[] dangerLineObj = new GameObject[4];
        float rotationY = 0;
        GameObject obj = ResourceManager.Instance.GetrcTexture("DangerLine");
        for (int i = 0; i < dangerLineObj.Length; i++)
        {
            dangerLineObj[i] = Instantiate(obj, transform.position, transform.rotation, transform);
            dangerLineObj[i].transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + rotationY, transform.rotation.eulerAngles.z);
            rotationY += 45;
        }
    }

    public void CreateLaser()
    {
        GameObject[] laserObj = new GameObject[8];
        float rotationY = 0;
        GameObject obj = ResourceManager.Instance.GetrcParticle("LigthningArrow");
        for (int i = 0; i < 8; i++)
        {
            laserObj[i] = Instantiate(obj,transform);
            laserObj[i].transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            laserObj[i].transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + rotationY, transform.rotation.eulerAngles.z);
            rotationY += 45;
        }
    }
}
