using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    private MeshRenderer meshRenderer;
    private Vector2 offset = Vector2.zero;
    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        offset.y += speed * Time.deltaTime; 
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}
