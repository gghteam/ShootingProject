using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    private MeshRenderer backgroundRenderer = null;

    private Vector2 offset = Vector2.zero;
    private void Start()
    {
        if(backgroundRenderer == null)
        {
            backgroundRenderer = gameObject.GetComponent<MeshRenderer>();
        }
    }

    private void Update()
    {
        offset += new Vector2(0f, speed * Time.deltaTime);
        backgroundRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}