using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public List<GameObject> firstWaveMobs = new List<GameObject>();

    public Camera mainCamera; // ī�޶�
    private Vector3 targetPosition; // �̵���ų Ÿ�� ��ġ
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
                float xOffset = (i / 2) % 2 == 0 ? -cameraWidth + enemyWidth : cameraWidth - enemyWidth; // �� ��ü�� ��� x��ǥ ����
                float yOffset = (i % 2 == 0) ? -cameraHeight + enemyHeight : cameraHeight - enemyHeight; // Ȧ�� ��° ��ü�� y��ǥ ����

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
