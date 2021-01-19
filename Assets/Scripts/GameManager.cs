using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int baseLayerOrder = 100;

    [SerializeField]
    private Transform slashPrefab = null;
    [SerializeField]
    private Transform enemyPrefab = null;
    public Rect moveLimitRect = new Rect();
    public Rect spawnLimitRect = new Rect();    

    private PlayerMove player = null;
    private float randomX = 0f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMove>();

        InitSlashPool();
        InitEnemyPool();

        StartCoroutine(SpawnSkeleton());
    }

    private IEnumerator SpawnSkeleton()
    {        
        while (true)
        {
            randomX = Random.Range(spawnLimitRect.xMin + 0.5f, spawnLimitRect.xMax - 0.5f);
            yield return new WaitForSeconds(1f);
            Enemy tempEnemy = SimplePoolHelper.Pop<Enemy>("Skeletons");
            tempEnemy.transform.position = new Vector2(randomX, spawnLimitRect.yMax);
        }
    }

    private void InitSlashPool()
    {
        SimplePool<Transform> slashPool = SimplePoolHelper.GetPool<Transform>("Slashes");

        slashPool.OnPush = (item) =>
        {
            item.gameObject.SetActive(false);
        };

        slashPool.OnPop = (item) => {
            item.gameObject.SetActive(true);
            item.position = player.slashPosition.position;
            item.rotation = Quaternion.identity;
        };

        slashPool.CreateFunction = (template) =>
        {
            Transform newSlash = Instantiate(slashPrefab);
            return newSlash;
        };

        slashPool.Populate(10);
    }

    private void InitEnemyPool()
    {
        SimplePool<Enemy> enemyPool = SimplePoolHelper.GetPool<Enemy>("Skeletons");

        enemyPool.OnPush = (item) =>
        {
            item.gameObject.SetActive(false);
        };

        enemyPool.OnPop = (item) =>
        {
            item.gameObject.SetActive(true);
            item.ResetHP();            
        };

        enemyPool.CreateFunction = (template) =>
        {
            Transform newEnemy = Instantiate(enemyPrefab) as Transform;
            return newEnemy.GetComponent<Enemy>();
        };

        enemyPool.Populate(10);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        DrawRect(spawnLimitRect);
        Gizmos.color = Color.blue;
        DrawRect(moveLimitRect);
    }

    private void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }
}
