using System.Collections.Generic;
using UnityEngine;

public class ObjectCullingManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;

    // 카메라 범위를 벗어난 물체를 유지하는 시간 (초)
    public float cullDelay = 2.0f;
    public float frustumExpansion = 1.1f;

    // 뷰 밖으로 벗어난 물체들의 리스트
    private List<OutOfViewObject> outOfViewObjects = new List<OutOfViewObject>();

    private void Update()
    {
        // 카메라 뷰 밖으로 벗어난 물체들을 확인하고 삭제
        CullObjectsOutsideCameraView();
    }

    private void CullObjectsOutsideCameraView()
    {
        if (mainCamera == null || player == null)
        {
            Debug.LogWarning("Camera or player reference not set in ObjectCullingManager.");
            return;
        }

        // 카메라 뷰의 영역을 계산
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        // 평면 확장
        for (int i = 0; i < frustumPlanes.Length; i++)
        {
            frustumPlanes[i].normal *= frustumExpansion;
        }

        foreach (OutOfViewObject obj in outOfViewObjects.ToArray())
        {
            // 이미 삭제된 경우 리스트에서 제거
            if (obj.gameObject == null)
            {
                outOfViewObjects.Remove(obj);
                continue;
            }

            // 뷰 안으로 다시 들어왔을 경우 리스트에서 제거
            if (GeometryUtility.TestPlanesAABB(frustumPlanes, obj.bounds))
            {
                outOfViewObjects.Remove(obj);
            }
            else if (Time.time - obj.timeStamp >= cullDelay)
            {
                // 일정 시간 동안 뷰 밖에 있었다면 삭제
                Destroy(obj.gameObject);
                outOfViewObjects.Remove(obj);
            }
        }

        // 모든 물체들을 확인
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objectsInScene)
        {
            // 플레이어는 제외
            if (obj == player)
                continue;

            // Collider 컴포넌트가 있는지 확인
            Collider collider = obj.GetComponent<Collider>();
            if (collider == null)
                continue;

            // 뷰 밖에 있는 물체인지 확인
            if (!GeometryUtility.TestPlanesAABB(frustumPlanes, collider.bounds))
            {
                if (!ContainsObject(outOfViewObjects, obj))
                {
                    outOfViewObjects.Add(new OutOfViewObject(obj, Time.time));
                }
            }
        }
    }

    private bool ContainsObject(List<OutOfViewObject> list, GameObject obj)
    {
        foreach (OutOfViewObject ovo in list)
        {
            if (ovo.gameObject == obj)
                return true;
        }
        return false;
    }
}

public class OutOfViewObject
{
    public GameObject gameObject;
    public float timeStamp;
    public Bounds bounds;

    public OutOfViewObject(GameObject obj, float time)
    {
        gameObject = obj;
        timeStamp = time;
        bounds = obj.GetComponent<Collider>().bounds;
    }
}
