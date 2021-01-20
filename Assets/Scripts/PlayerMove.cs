using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private Transform bulletPosition = null;
    [SerializeField]
    private GameObject bulletPrefab = null;

    private Vector2 mousePosition = Vector2.zero;
    private Vector3 targetPosition = Vector3.zero;

    private Collider2D col = null;
    private SpriteRenderer spriteRenderer = null;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col.enabled = true;        
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) == true)
        {
            mousePosition = Input.mousePosition;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            targetPosition.x = Mathf.Clamp(targetPosition.x, GameManager.Instance.minimumPosition.x, GameManager.Instance.maximumPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, GameManager.Instance.minimumPosition.y, GameManager.Instance.maximumPosition.y);
            targetPosition.z = 0f;

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
        }
    }

    private IEnumerator Fire()
    {
        while (true)
        {

        }
    }
}
