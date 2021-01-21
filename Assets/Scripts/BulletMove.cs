using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public enum BULLET_MODE
    {
        PLAYER = 0,
        ENEMY = 1
    }

    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private Sprite playerBulletSprite = null;
    [SerializeField]
    private Sprite enemyBulletSprite = null;

    private SpriteRenderer spriteRenderer = null;

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        CheckLimit();
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
        transform.SetParent(GameManager.Instance.poolManager.transform, false);
    }

    public void SetBulletMode(BULLET_MODE bulletMode = BULLET_MODE.PLAYER) 
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if(bulletMode == BULLET_MODE.PLAYER)
        {
            spriteRenderer.sprite = playerBulletSprite;
            gameObject.layer = GameManager.Instance.PLAYER_LAYER;
        }
        else
        {
            spriteRenderer.sprite = enemyBulletSprite;
            gameObject.layer = GameManager.Instance.ENEMY_LAYER;
        }
    }

    private void CheckLimit()
    {
        if (transform.localPosition.x < GameManager.Instance.minimumPosition.x - 2f || transform.localPosition.x > GameManager.Instance.maximumPosition.x + 2f)
        {
            Despawn();
        }
        if (transform.localPosition.y < GameManager.Instance.minimumPosition.y - 5f || transform.localPosition.y > GameManager.Instance.maximumPosition.y + 5f)
        {
            Despawn();
        }
    }
}
