using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 dir;
    public GameObject player;
    // 위로 날아가기
    public GameObject bulletExplosion;
    public PlayerMove playerMove;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        //transform.position = player.transform.position;
        dir = (player.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.back, dir);
        transform.rotation = targetRotation;
        Destroy(gameObject, 2f);
    }
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject bulletExplosion0 = Instantiate(bulletExplosion);
        bulletExplosion0.transform.position = transform.position;
        if (other.gameObject.CompareTag("Player"))
        {
            playerMove.hp--;
        }
        if (!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
