using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHP = 1;
    [SerializeField]
    private float speed = 5f;

    private int currentHP = 0;
    private SpriteRenderer spriteRenderer = null;
    private Rigidbody2D enemyRigidbody = null;

    private readonly int PLAYER_ATTACK_LAYER = 9;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        enemyRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void ResetHP()
    {        
        currentHP = maxHP;
        spriteRenderer.material.SetColor("_Color", new Color(0, 0, 0, 0));
    }
    private void Update()
    {
        ChangeSortingOrder();
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        CheckLimit();
    }

    private void CheckLimit()
    {
        if (transform.position.y > GameManager.Instance.spawnLimitRect.yMax || transform.position.y < GameManager.Instance.spawnLimitRect.yMin)
        {
            Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == PLAYER_ATTACK_LAYER)
        {
            collision.transform.Pool("Slashes");
            currentHP--;
            StartCoroutine(DamageEffect());

            if(currentHP <= 0)
            {
                Despawn();
            }
        }
    }

    private IEnumerator DamageEffect()
    {        
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetColor("_Color", new Color(1, 0.5f, 0.5f, 0));
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetColor("_Color", new Color(0, 0, 0, 0));
    }

    private void Despawn()
    {
        this.Pool("Skeletons");
    }
    private void ChangeSortingOrder()
    {
        spriteRenderer.sortingOrder = (int)(GameManager.Instance.baseLayerOrder - transform.position.y * 10f);
    }
}
