using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRoller : MonoBehaviour
{
    public float speed = 1.0f;
    float currentTime;
    public Material mat;
    // Start is called before the first frame update
    private void Start()
    {

    }

    private void Update()
    {
        currentTime += speed * Time.deltaTime;
        mat.mainTextureOffset = Vector3.up * currentTime;
    }
}
