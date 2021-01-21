using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCroissant : EnemyMove
{
    protected override void Update()
    {
        base.Update();
        if (!isDead)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }            
    }
}
