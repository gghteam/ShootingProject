using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private float limitTime = 1f;
    private void OnEnable()
    {
        StartCoroutine(DespawnByTime(limitTime));
    }

    private IEnumerator DespawnByTime(float time)
    {
        yield return new WaitForSeconds(time);
        Despawn();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        CheckLimit();
    }
    
    private void CheckLimit()
    {
        if(transform.position.y > GameManager.Instance.spawnLimitRect.yMax || transform.position.y < GameManager.Instance.spawnLimitRect.yMin)
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        transform.Pool("Slashes");
    }
}
