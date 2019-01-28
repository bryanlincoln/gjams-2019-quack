using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] nest = null;
    private int nestIndex = 0;

    [SerializeField]
    private PlayerController playerController = null;

    [SerializeField]
    private GameObject nestIndicator = null;
    [SerializeField]
    private AudioController audioController;

    void Start () {
        for (int i = 0; i < nest.Length; i++) {
            nest[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Pickable")) {
            CreateNest();
            Destroy(other.gameObject);
        }
    }

    private void CreateNest () {
        nestIndicator.SetActive(false);
        nest[nestIndex].SetActive(true);
        nestIndex++;
        ScoreManager.score++;
        audioController.PlayPoint();
        if (nestIndex == 5) {
            playerController.GameWin();
        }
    }
}
