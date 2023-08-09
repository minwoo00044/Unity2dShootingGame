using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ܰ�1 �ð��� �帧
//�ܰ�2 ���� �ð��� Ư�� �ð�(�ּ�, �ִ�)�� ������
//�ܰ�3. ��ų �������� �����Ѵ�.
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
        //�ܰ�1 �ð��� �帧
        currentTime += Time.deltaTime;
        //�ð� �Ƶ�.
        if (currentTime > createTime) 
        {
            //��ų ������ ����
            GameObject skillItmeGO = Instantiate(skillItem);
            //��ų �Ŵ����� ��ġ�� �����Ѵ�.
            skillItmeGO.transform.position = transform.position;

            currentTime = 0;

            createTime = Random.Range(minTime, maxTime);
        }
    }
}
