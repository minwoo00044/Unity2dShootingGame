using System.Collections.Generic;
using UnityEngine;

public class ObjectCullingManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;

    // ī�޶� ������ ��� ��ü�� �����ϴ� �ð� (��)
    public float cullDelay = 2.0f;
    public float frustumExpansion = 1.1f;

    // �� ������ ��� ��ü���� ����Ʈ
    private List<OutOfViewObject> outOfViewObjects = new List<OutOfViewObject>();

    private void Update()
    {
        // ī�޶� �� ������ ��� ��ü���� Ȯ���ϰ� ����
        CullObjectsOutsideCameraView();
    }

    private void CullObjectsOutsideCameraView()
    {
        if (mainCamera == null || player == null)
        {
            Debug.LogWarning("Camera or player reference not set in ObjectCullingManager.");
            return;
        }

        // ī�޶� ���� ������ ���
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        // ��� Ȯ��
        for (int i = 0; i < frustumPlanes.Length; i++)
        {
            frustumPlanes[i].normal *= frustumExpansion;
        }

        foreach (OutOfViewObject obj in outOfViewObjects.ToArray())
        {
            // �̹� ������ ��� ����Ʈ���� ����
            if (obj.gameObject == null)
            {
                outOfViewObjects.Remove(obj);
                continue;
            }

            // �� ������ �ٽ� ������ ��� ����Ʈ���� ����
            if (GeometryUtility.TestPlanesAABB(frustumPlanes, obj.bounds))
            {
                outOfViewObjects.Remove(obj);
            }
            else if (Time.time - obj.timeStamp >= cullDelay)
            {
                // ���� �ð� ���� �� �ۿ� �־��ٸ� ����
                Destroy(obj.gameObject);
                outOfViewObjects.Remove(obj);
            }
        }

        // ��� ��ü���� Ȯ��
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objectsInScene)
        {
            // �÷��̾�� ����
            if (obj == player)
                continue;

            // Collider ������Ʈ�� �ִ��� Ȯ��
            Collider collider = obj.GetComponent<Collider>();
            if (collider == null)
                continue;

            // �� �ۿ� �ִ� ��ü���� Ȯ��
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
