using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어가 움직이고 싶다.
public class PlayerMove : MonoBehaviour
{
    public float speed = 5;


    public int hp = 10;
    void Start()
    {
    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.up * v;
        transform.position += dir * speed * Time.deltaTime;

        if(hp < 0)
        {
            GameManager.Instance.GameEnd();
            Destroy(gameObject);
        }
    }


}
