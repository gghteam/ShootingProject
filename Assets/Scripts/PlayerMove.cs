using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        StartCoroutine(Fire());
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
            SpawnBullet();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void SpawnBullet()
    {
        GameObject newBullet = null;
        if(GameManager.Instance.poolManager.transform.childCount > 0)
        {
            newBullet = GameManager.Instance.poolManager.transform.GetChild(0).gameObject;
        }
        else
        {
            newBullet = Instantiate(bulletPrefab);
        }

        newBullet.transform.SetParent(null);
        newBullet.transform.position = bulletPosition.position;
        newBullet.SetActive(true);
    }

    private IEnumerator Revive()
    {
        col.enabled = false;

        int count = 0;
        while(count < 5)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.25f);
            count++;
        }

        col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.Instance.life > 0)
        {
            GameManager.Instance.life--;
            GameManager.Instance.UpdateLife();
            StartCoroutine(Revive());
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
