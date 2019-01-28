using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulse : MonoBehaviour
{
    [SerializeField]
    private Light pointLight = null;
    [SerializeField]
    private float velocity = 1;
    [SerializeField]
    private float rangeMin = 1;
    [SerializeField]
    private float rangeMax = 3;

    private bool up = false;

    void Update()
    {
        if (up) {
            if (pointLight.range < rangeMax) {
                pointLight.range += velocity * Time.deltaTime;
            }
            else {
                up = false;
            }
        }
        else if (pointLight.range > rangeMin) {
            pointLight.range -= velocity * Time.deltaTime;
        }
        else {
            up = true;
    }
        }
}
