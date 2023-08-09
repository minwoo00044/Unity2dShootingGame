using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//단계1 시간의 흐름
//단계2 현재 시간이 특정 시간(최소, 최대)을 넘으면
//단계3. 스킬 아이템을 생성한다.
public class SkillManager : MonoBehaviour
{
    public GameObject skillItem;
    private float createTime;

    public float minTime = 3;
    public float maxTime = 10;

    public float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        createTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        //단계1 시간의 흐름
        currentTime += Time.deltaTime;
        //시간 됐따.
        if (currentTime > createTime) 
        {
            //스킬 아이템 생성
            GameObject skillItmeGO = Instantiate(skillItem);
            //스킬 매니저의 위치로 설정한다.
            skillItmeGO.transform.position = transform.position;

            currentTime = 0;

            createTime = Random.Range(minTime, maxTime);
        }
    }
}
