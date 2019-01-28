using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target = null;
    [SerializeField]
    private Camera myCamera;
    [SerializeField]
    private float smoothSpeed = 0.05f;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, 17);

    void FixedUpdate() {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public IEnumerator Zoom() {
        float targetFov = myCamera.fieldOfView - 10;
        while(myCamera.fieldOfView > targetFov) {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 0.01f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            offset.y -= 0.03f;
            myCamera.fieldOfView -= 0.2f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
