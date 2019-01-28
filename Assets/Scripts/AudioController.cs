using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField]
    private AudioSource source = null;

    [SerializeField]
    private AudioClip wings = null;
    [SerializeField]
    private AudioClip stickPick = null;
    [SerializeField]
    private AudioClip stickFall = null;
    [SerializeField]
    private AudioClip point = null;
    [SerializeField]
    private AudioClip win = null;
    [SerializeField]
    private AudioClip lose = null;

    [SerializeField]
    private float wingDelay = 0.5f;
    private float wingTimer = 0;

    private void Update() {
        if(wingTimer > 0) {
            wingTimer -= Time.deltaTime;
        }
    }

    public void PlayWing() {
        if (wingTimer > 0) return;
        source.PlayOneShot(wings);
        wingTimer = wingDelay;
    }

    public void PlayStickFall() {
        source.PlayOneShot(stickFall);
    }

    public void PlayPoint() {
        source.PlayOneShot(point, 0.4f);
    }

    public void PlayWin() {
        source.PlayOneShot(win);
    }

    public void PlayLose() {
        source.PlayOneShot(lose);
    }

    public void PlayStickPick() {
        source.PlayOneShot(stickPick);
    }
}
