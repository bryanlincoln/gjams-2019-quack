using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSpawner : MonoBehaviour
{
    [SerializeField]
    private int numberOfSticks = 5;

    [SerializeField]
    private GameObject prefabStick;

    private List<GameObject> spawnPoints;
    

    private void Awake() {
        spawnPoints = new List<GameObject>();

        foreach(Transform child in transform) {
            spawnPoints.Add(child.gameObject);
            child.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void Start() {
        for(int i = 0; i < numberOfSticks; i++) {
            int random = Random.Range(0, spawnPoints.Count);
            GameObject go = GameObject.Instantiate(prefabStick);
            go.transform.position = spawnPoints[random].transform.position;
            spawnPoints.RemoveAt(random);
        }
    }
}
