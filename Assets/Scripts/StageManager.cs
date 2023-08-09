using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public List<GameObject> firstWaveMobs = new List<GameObject>();

    public Camera mainCamera; // 카메라
    private Vector3 targetPosition; // 이동시킬 타겟 위치
    public float firstStageMoveDuration = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Stage1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Stage1()
    {
        StartCoroutine("FirstWave");
        yield return null;
    }

    IEnumerator FirstWave()
    {
        float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float cameraHeight = (mainCamera.orthographicSize) / 2;

        float targetX = Mathf.Clamp(mainCamera.transform.position.x, -cameraWidth, cameraWidth);
        float targetY = Mathf.Clamp(mainCamera.transform.position.y, -cameraWidth, cameraHeight);


        float startTime = Time.time;

        while (Time.time - startTime < firstStageMoveDuration)
        {
            float t = (Time.time - startTime) / firstStageMoveDuration;

            for (int i = 0; i < firstWaveMobs.Count; i++)
            {
                if (firstWaveMobs[i] == null)
                {
                    continue;
                }
                float enemyWidth = firstWaveMobs[i].GetComponent<MeshRenderer>().bounds.extents.x;
                float enemyHeight = firstWaveMobs[i].GetComponent<MeshRenderer>().bounds.extents.y;
                float xOffset = (i / 2) % 2 == 0 ? -cameraWidth + enemyWidth : cameraWidth - enemyWidth; // 두 개체씩 묶어서 x좌표 조정
                float yOffset = (i % 2 == 0) ? -cameraHeight + enemyHeight : cameraHeight - enemyHeight; // 홀수 번째 개체의 y좌표 조정

                targetPosition = new Vector3(targetX + xOffset, targetY + yOffset, 0f);

                firstWaveMobs[i].transform.position = Vector3.Lerp(firstWaveMobs[i].transform.position, targetPosition, t / 2);
                Turret turret = firstWaveMobs[i].GetComponent<Turret>();
                if (turret != null)
                {
                    turret.isShootable = true;
                }
            }
            yield return null;

        }



        yield return new WaitForSeconds(15f);
        SecondWave();

    }

    IEnumerator SecondWave()
    {
        print("ddd");
        yield return null;
    }
}
