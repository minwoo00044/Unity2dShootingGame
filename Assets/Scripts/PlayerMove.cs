using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어가 움직이고 싶다.
public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float playerWidth;
    private float playerHeight;

    public int hp = 10;
    void Start()
    {
        mainCamera = Camera.main;

        playerWidth = 0.5f;
        playerHeight = 0.5f;

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.up * v;
        transform.position += dir * speed * Time.deltaTime;

        if(hp < 0)
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + playerWidth, screenBounds.x - playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + playerHeight, screenBounds.y - playerHeight);
        transform.position = viewPos;
    }
}
