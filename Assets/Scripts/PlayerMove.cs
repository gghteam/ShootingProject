using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float shootDelay = 1f;
    
    public Transform slashPosition { get; private set; }

    private SpriteRenderer spriteRenderer = null;
    private Animator playerAnimator = null;

    private Vector3 currentPosition = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;

    private float shootTimer = 0f;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerAnimator = gameObject.GetComponent<Animator>();
        slashPosition = transform.GetChild(0);
    }

    private void Update()
    {
        ChangeSortingOrder();
        if (Input.GetMouseButton(0) == true)
        {            
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.position = ApplyMoveLimit(currentPosition);

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootDelay)
            {
                shootTimer = 0f;
                Shoot();
            }
        }
        else
        {
            shootTimer = 0f;
            playerAnimator.SetTrigger("Attack");
        }
    }

    private void Shoot()
    {                
        SimplePoolHelper.Pop<Transform>("Slashes");
    }

    private Vector3 ApplyMoveLimit(Vector3 currentPosition)
    {
        Vector3 targetPosition = Vector3.zero;
        targetPosition.x = Mathf.Clamp(currentPosition.x, GameManager.Instance.moveLimitRect.xMin, GameManager.Instance.moveLimitRect.xMax);
        targetPosition.y = Mathf.Clamp(currentPosition.y, GameManager.Instance.moveLimitRect.yMin, GameManager.Instance.moveLimitRect.yMax);
        return targetPosition;
    }

    private void ChangeSortingOrder()
    {
        spriteRenderer.sortingOrder = (int)(GameManager.Instance.baseLayerOrder - transform.position.y * 10f);
    }
}
