using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] 
    public MapType mapType;

    public enum MapType { Spawn, Tunnel, Station, Subway };
    public GameObject[] spawnPrefabs;

    Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 worldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnPlatform()
    {
        switch (mapType)
        {
            case MapType.Spawn:
                spawnPos = new Vector3(0f, 0f, transform.position.z + 960f);
                break;

            case MapType.Tunnel:
                spawnPos = new Vector3(0f, 0f, transform.position.z + 720f);
                break;

            case MapType.Station:
                spawnPos = new Vector3(0f, 0f, transform.position.z + 900f);
                break;

            case MapType.Subway:
                spawnPos = new Vector3(0f, 0f, transform.position.z + 1080f);
                break;
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject prefabToSpawn = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];

            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

            Debug.Log("Player Collision");
        }
    }
}
