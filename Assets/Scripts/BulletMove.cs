using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if(transform.localPosition.y > GameManager.Instance.maximumPosition.y)
        {
            Despawn();
        }
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
        transform.SetParent(GameManager.Instance.poolManager.transform, false);
    }
}
