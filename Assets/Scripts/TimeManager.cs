using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour {
    [SerializeField]
    public float time;   

    Text text;                      

    void Awake() {
        text = GetComponent<Text>();
        time = 0;
    }

    void Update() {
        time += Time.deltaTime;
        text.text = FormatTime();
    }
    
    public string FormatTime () {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;

        string timeText = string.Format ("{0:00}:{1:00}", minutes, seconds);
        return timeText;
    }
    
}