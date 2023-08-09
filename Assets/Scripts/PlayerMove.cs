using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾ �����̰� �ʹ�.
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
