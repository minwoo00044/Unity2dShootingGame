using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bullet;
    float currentTime;
    public float createTime = 1;
    GameObject player;
    public Vector3 dir;
    Vector3 playerDir;

    public GameObject gunPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime) 
        {
            
            dir = (player.transform.position - transform.position).normalized;


            GameObject bulletGO = Instantiate(bullet);
            bulletGO.transform.position = gunPos.transform.position;

            currentTime = 0;
        }
    }
}
