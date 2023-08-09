using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject turretBullet;
    public GameObject player;

    public float currentTime;
    public float createTime;
    public Vector3 dir;
    public GameObject gunPos;

    public float gunDir = 30f;

    public bool isShootable = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(Camera.main.transform.position, transform.position);

        if (dist > Camera.main.orthographicSize * 1.3f)
        {
            isShootable = false;
            if (dist > Camera.main.orthographicSize * 2.5f)
            {
                Destroy(gameObject);
            }
        }
        currentTime += Time.deltaTime;
        if (currentTime > createTime && isShootable)
        {

            dir = (player.transform.position - transform.position).normalized;


            GameObject bulletGO = Instantiate(turretBullet);
            bulletGO.transform.position = gunPos.transform.position;
            if(transform.position.x > 0)
            {
                gunDir = gunDir * -1;
            }
            bulletGO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -gunDir *3));
            GameObject bulletGameObject0 = Instantiate(turretBullet);
            GameObject bulletGameObject1 = Instantiate(turretBullet);

            bulletGameObject0.transform.position = gunPos.transform.position;
            bulletGameObject0.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -gunDir * 4));
            bulletGameObject1.transform.position = gunPos.transform.position ;
            bulletGameObject1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -gunDir * 2));


            currentTime = 0;
        }
    }
}
