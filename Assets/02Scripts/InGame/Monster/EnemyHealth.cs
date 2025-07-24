using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField]
    [Range(10, 50)]
    int hitHealth;
    private SkinnedMeshRenderer meshRenderer;
    private Color originColor;
    private Coroutine hitCoroutine;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originColor = meshRenderer.material.color;
    }

    public void TakeDameage(float damage)
    {
        if (health > 0)
        {
            if(health % hitHealth == 0) hitCoroutine = StartCoroutine("OnHitColor");
            health -= damage;
            Debug.Log("Hit");
            if (health <= 0) EnemyDeath();
        }
    }
    void EnemyDeath()
    {
        animator.SetTrigger("Death");

    }

    private IEnumerator OnHitColor()
    {
        animator.SetTrigger("Hit");
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.color = originColor;
    }
}
