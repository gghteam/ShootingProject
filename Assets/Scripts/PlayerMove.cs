using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Animator playerAnimator = null;

    private Vector3 currentPosition = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;

    private void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) == true)
        {
            playerAnimator.SetBool("Attack", false);
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.position = currentPosition;
        }
        else
        {
            playerAnimator.SetBool("Attack", true);
        }
    }
}
