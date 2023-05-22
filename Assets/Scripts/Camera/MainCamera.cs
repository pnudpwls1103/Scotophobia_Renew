using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    Transform playerTrans;

    [SerializeField]
    Vector3 cameraPosition;

    public Vector2[] centers = new Vector2[6];

    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;
    int stageNum = 1;
    string[] stageWalls = new string[2] { "Stage1_Wall", "Stage2_Wall" };

    Vector2 mapSize;
    Vector2 center;
    float minX;
    float maxX;
    float minY;
    float maxY;

    void Start()
    {
        CalculateMapSize();
        print(minX);
        print(maxX);
        height = Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    void FixedUpdate()
    {
        LimitCameraArea();
        if (stageNum != PlayerController.instance.currentStage)
        {
            stageNum = PlayerController.instance.currentStage;
            print(stageNum);
            CalculateMapSize();
        }
    }

    void CalculateMapSize()
    {
        minX = float.MaxValue;
        maxX = float.MinValue;
        minY = float.MaxValue;
        maxY = float.MinValue;

        foreach (Collider2D wallCollider in FindObjectsOfType<Collider2D>())
        {
            if (wallCollider.CompareTag(stageWalls[stageNum]))
            {
                Vector2 colliderMin = wallCollider.bounds.max;
                Vector2 colliderMax = wallCollider.bounds.min;
                minX = Mathf.Min(minX, colliderMin.x);
                maxX = Mathf.Max(maxX, colliderMax.x);
                minY = Mathf.Min(minY, colliderMin.y);
                maxY = Mathf.Max(maxY, colliderMax.y);
            }
        }

        mapSize = new Vector2(maxX - minX, maxY - minY);
        center = new Vector2((minX + maxX) / 2, (minY + maxY) / 2);
    }

    void LimitCameraArea()
    {
        transform.position = Vector3.Lerp(transform.position, playerTrans.position + cameraPosition, Time.deltaTime * cameraMoveSpeed);

        float clampX = Mathf.Clamp(transform.position.x, minX + width, maxX - width);

        transform.position = new Vector3(clampX, center.y + cameraPosition.y, -10f);
    }
}
