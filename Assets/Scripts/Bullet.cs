using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ǥ : ����(�Ѿ�) ���� ���ư���.
// ������ �ʿ��ϴ�.
// �ӵ��� �ʿ��ϴ�.

public class Bullet : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 dir = Vector3.up;

    public GameObject bulletExplosion;
    public GameObject player;
    public Enemy enemy;

    public AudioClip clip;
    // ���� ���ư���

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //transform.position = player.transform.position;

        Destroy(gameObject, 2f);
    }
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime, Space.Self);
       //transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy")) 
        {
            GameObject soundManager = GameObject.Find("SoundManager");
            AudioSource audioSource = soundManager.GetComponent<SoundManager>().effAudioSource;
            audioSource.clip = clip;
            audioSource.Play();

            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.hp--;
            GameObject bulletExplosion0 = Instantiate(bulletExplosion);
            bulletExplosion0.transform.position = transform.position;

            Destroy(gameObject);
        }


    }

}
