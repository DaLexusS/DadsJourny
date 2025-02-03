using System.Collections.Generic;
using UnityEngine;

public class CloudsMover : MonoBehaviour
{
    [SerializeField] GameObject[] SpawnPoints;  // Random spawn locations
    [SerializeField] GameObject[] CloudPrefabs; // Different cloud types
    [SerializeField] List<GameObject> MyInstantiatedObjects = new List<GameObject>();
    [SerializeField] List<float> MyObjectsSpeed = new List<float>();
    [SerializeField] List<float> MyObjectsSize = new List<float>();

    [SerializeField] float SpeedMin = 5f;
    [SerializeField] float SpeedMax = 5f;
    [SerializeField] float SpawnTimeMin = 5f;
    [SerializeField] float SpawnTimeMax = 5f;
    [SerializeField] float SpawnSizeMin = 1f;
    [SerializeField] float SpawnSizeMax = 4f;



    [SerializeField] int screenbound = 10;

    private float timer = 0f;
    private bool SpawnFlag = false;
    private float NextSpawnTimer = 0f;
    void Start()
    {
        MyInstantiatedObjects.Clear();

        SpawnSomeStartingClouds();
    }

    private void SpawnSomeStartingClouds()
    {
        var rand = Random.Range(1, 5);
        for (int i = 0; i < rand; i++)
        {
            SpawnObject();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (!SpawnFlag) 
        {
             NextSpawnTimer = Random.Range(SpawnTimeMin, SpawnTimeMax);
            SpawnFlag = true;
        }
        if (timer > NextSpawnTimer)
        {
            if (MyInstantiatedObjects.Count < CloudPrefabs.Length)
            {
                SpawnObject(); // ✅ Spawn new clouds first
            }
            else
            {
                ResetRandomCloud(); // ✅ Reset a random cloud when all are spawned
            }
            timer = 0f;
            SpawnFlag = false;
        }

        MoveClouds();
    }

    private void MoveClouds()
    {
        for (int i = 0; i < MyInstantiatedObjects.Count; i++)
        {
            float MySpeed = MyObjectsSpeed[i];
            MyInstantiatedObjects[i].transform.Translate((Vector2.left * MySpeed * Time.deltaTime));
        }
    }

    private float RandomSpeed()
    {
        return Random.Range(SpeedMin, SpeedMax);
    }
    private float RandomSize()
    {
        return Random.Range(SpawnSizeMin, SpawnSizeMax);
    }

    private void ResetRandomCloud()
    {
        // ✅ Find a cloud that is off-screen and reset it
        int rnd = Random.Range(0, MyInstantiatedObjects.Count);
        GameObject cloudToReset = MyInstantiatedObjects[rnd];

        if (cloudToReset.transform.position.x < -screenbound) // Adjust your screen boundary
        {
            MyObjectsSpeed[rnd] = RandomSpeed();
            MyObjectsSize[rnd] = RandomSize();

            cloudToReset.transform.position = SpawnPoints[GetRandomSpawnPoint()].transform.position;

        }
        else
        {
            // If no clouds have left the screen yet, try again next frame
        }
    }

    private void SpawnObject()
    {
        // ✅ Spawn a random cloud at a random spawn point
        MyObjectsSpeed.Add(RandomSpeed());
        float size = RandomSize();
        MyObjectsSize.Add(size);



        GameObject newCloud = Instantiate(
            CloudPrefabs[Random.Range(0, CloudPrefabs.Length)],
            SpawnPoints[GetRandomSpawnPoint()].transform.position,
            Quaternion.identity
        );
        newCloud.transform.localScale *= size;
        MyInstantiatedObjects.Add(newCloud);
    }

    private int GetRandomSpawnPoint()
    {
        return Random.Range(0, SpawnPoints.Length);
    }
}
