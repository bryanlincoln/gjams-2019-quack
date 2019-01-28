using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{

    private bool started = false;
    private bool ended = false;

    [SerializeField]
    private Animator animMenu = null;
    [SerializeField]
    private GameObject goFade = null;
    [SerializeField]
    private GameObject endGame = null;
    [SerializeField]
    private TimeManager timeManager = null;
    [SerializeField]
    private GameObject Time = null;
    [SerializeField]
    private GameObject inGame = null;


    private void Awake() {
        goFade.SetActive(true);
    }

    void Update() {
        if(!started) {
            if(Input.anyKey) {
                InGame();
            }
        } else if(ended) {
            if (Input.anyKey) {
                ended = false;
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }

    void InGame () {
        started = true;
        animMenu.SetTrigger("FadeOut");
        inGame.SetActive(true);

    }

    public void EndGame() {
        ended = true;
        inGame.SetActive(false);
        endGame.SetActive(true);
        Text text = Time.GetComponent<Text>();
        text.text = timeManager.FormatTime();
        timeManager.enabled = false;
    }
}
