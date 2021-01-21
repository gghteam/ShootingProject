using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHotdog : EnemyMove
{
    [SerializeField]
    private GameObject bulletPrefab = null;
    private Vector3 direction = Vector3.zero;
    private float shootAngle = 0f;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Fire());
    }
    protected override void Update()
    {
        base.Update();
        if (!isDead)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }            
    }

    private IEnumerator Fire()
    {
        while (true) 
        {
            yield return new WaitForSeconds(1f);
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        GameObject newBullet = null;
        if (GameManager.Instance.poolManager.transform.childCount > 0)
        {
            newBullet = GameManager.Instance.poolManager.transform.GetChild(0).gameObject;
        }
        else
        {
            newBullet = Instantiate(bulletPrefab);
        }

        newBullet.GetComponent<BulletMove>().SetBulletMode(BulletMove.BULLET_MODE.ENEMY);
        newBullet.transform.SetParent(null);
        newBullet.transform.position = transform.position;
        direction = transform.InverseTransformPoint(GameManager.Instance.player.transform.position);
        shootAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        newBullet.transform.Rotate(0f, 0f, -shootAngle, Space.Self);
        newBullet.SetActive(true);
    }
}
