using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//목표 : 적 생성
//필요 속성 : 특정시간
public class EnemyManager : MonoBehaviour
{
    //적
    public GameObject enemy;
    //현재시간
    [SerializeField]
    private float currentTime;
    //목표 시간
    public float createTime;

    public float minTime = 3;
    public float maxTime = 5;

    //카메라
    private Camera mainCamera;
    private Vector3 screenBounds;

    private void Start()
    {
        createTime = Random.Range(minTime, maxTime);

        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //시간 흐름
        currentTime += Time.deltaTime;
        //특정 시간이 지나면
        if(currentTime >= createTime)
        {
            //생성
            GameObject CVenemy = Instantiate(enemy, screenBounds, Quaternion.Euler(0,0,0));
            print(CVenemy.transform.position);
            //시간 초기화
            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }
    }
}
