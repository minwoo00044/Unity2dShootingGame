using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//��ǥ : �� ����
//�ʿ� �Ӽ� : Ư���ð�
public class EnemyManager : MonoBehaviour
{
    //��
    public GameObject enemy;
    //����ð�
    [SerializeField]
    private float currentTime;
    //��ǥ �ð�
    public float createTime;

    public float minTime = 3;
    public float maxTime = 5;

    //ī�޶�
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
        //�ð� �帧
        currentTime += Time.deltaTime;
        //Ư�� �ð��� ������
        if(currentTime >= createTime)
        {
            //����
            GameObject CVenemy = Instantiate(enemy, screenBounds, Quaternion.Euler(0,0,0));
            print(CVenemy.transform.position);
            //�ð� �ʱ�ȭ
            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }
    }
}
