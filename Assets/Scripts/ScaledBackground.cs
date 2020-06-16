using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;
        float height = camera.orthographicSize * 2f;
        float width = camera.aspect * height;
        float scale = width / height;
        transform.localScale = new Vector3(scale, scale, 0);
    }
}
