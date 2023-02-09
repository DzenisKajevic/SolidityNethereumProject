using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float spawnNewAfter = 2f;
    private float timeSincePrevious = 1.5f;
    public GameObject obstacle;

    public float height = 16.5f;

    // Update is called once per frame
    void Update()
    {
        if (timeSincePrevious > spawnNewAfter)
        {
            GameObject gameObject = Instantiate(obstacle);
            gameObject.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);

            timeSincePrevious = 0;

            Destroy(gameObject, 10);
        }

        timeSincePrevious += Time.deltaTime;
    }
}