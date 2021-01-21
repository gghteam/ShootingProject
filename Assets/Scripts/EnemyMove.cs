using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    protected float speed = 0.5f;
    [SerializeField]
    protected int rewardScore = 100;
    [SerializeField]
    protected int hp = 2;

    protected bool isDead = false;

    private int currentHp = 2;
    private SpriteRenderer spriteRenderer = null;
    private Animator animator = null;
    
    protected virtual void Start()
    {
        currentHp = hp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }

    protected virtual void Update()
    {
        if (isDead) { return; }
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if(transform.localPosition.y < GameManager.Instance.minimumPosition.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) { return; }
        if(collision.tag == "Bullet")
        {
            currentHp--;
            BulletMove bullet = collision.GetComponent<BulletMove>();
            if(bullet != null)
            {
                bullet.Despawn();
            }

            if (currentHp <= 0)
            {
                isDead = true;
                GameManager.Instance.score += rewardScore;
                GameManager.Instance.UpdateScore();
                animator.Play("Explosion");
                Destroy(gameObject, 0.5f);
            }
            else
            {
                StartCoroutine(DamageEffect());
            }
        }
    }

    protected IEnumerator DamageEffect()
    {
        spriteRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0f));
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
    }
}
