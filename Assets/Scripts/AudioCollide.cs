using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCollide : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip collisionSound;

    private void OnCollisionEnter(Collision collision) {
        source.PlayOneShot(collisionSound);
    }
}
