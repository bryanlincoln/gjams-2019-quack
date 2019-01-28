using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private enum LookDirection {
        Front,
        Left,
        Right
    }

    [SerializeField]
    private AudioController audioController;

    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private float velocity = 10;
    [SerializeField]
    private float defaultDirection = 0;
    [SerializeField]
    private float maxHeight = 45;
    [SerializeField]
    private float rotationSpeed = 2;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Rigidbody rig = null;
    [SerializeField]
    private GameObject myStick = null;
    [SerializeField]
    private GameObject stickPrefab = null;
    [SerializeField]
    private GameObject eggPrefab = null;
    [SerializeField]
    private Transform assPosition = null;
    [SerializeField]
    private CanvasController canvasController = null;

    private bool carrying = false;
    private GameObject nextToPickable = null;

    public bool canMove = true;

    void Update() {
        rig.velocity = Vector3.zero;


        // Controla o deslocamento
        if (canMove) {
            LookDirection direction = LookDirection.Front;
            if (Input.GetAxis("Vertical") > 0.1) {
                Move(new Vector3(0, velocity * Time.deltaTime, 0));
            }
            else if (Input.GetAxis("Vertical") < -0.1) {
                Move(new Vector3(0, -1 * velocity * Time.deltaTime, 0));
            }
            if (Input.GetAxis("Horizontal") > 0.1) {
                direction = LookDirection.Right;
                Move(new Vector3(-1 * velocity * Time.deltaTime, 0, 0));
            }
            else if (Input.GetAxis("Horizontal") < -0.1) {
                direction = LookDirection.Left;
                Move(new Vector3(velocity * Time.deltaTime, 0, 0));
            }

            Look(direction);

            if (Input.GetButtonDown("Interact")) {
                if (carrying) {
                    Drop();
                }
                else if (nextToPickable) {
                    Pick();
                }
            }
        }

        if(Input.GetButtonDown("Cancel")) {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Pickable")) {
            nextToPickable = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Pickable")) {
            nextToPickable = null;
        }
    }

    private void Move(Vector3 to) {
        Vector3 target = transform.position;
        target += to;
        transform.position = target;
        audioController.PlayWing();
        animator.SetTrigger("Fly");
        if (transform.position.y > maxHeight) {
            StartCoroutine(GetDown());
        }
    }

    private IEnumerator GetDown() {
        canMove = false;
        while (transform.position.y > maxHeight * 0.9) {
            Move(new Vector3(0, -velocity * Time.deltaTime, 0));
            yield return new WaitForSeconds(0.001f);
        }
        canMove = true;
    }

    private void Look(LookDirection to) {
        float targetRotation;

        switch (to) {
            case LookDirection.Left:
                targetRotation = defaultDirection + 90;
                break;
            case LookDirection.Right:
                targetRotation = defaultDirection - 90;
                break;
            default:
                targetRotation = defaultDirection;
                break;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, targetRotation, transform.eulerAngles.z), Time.deltaTime * rotationSpeed);
    }

    private void Pick() {
        carrying = true;
        canMove = false;
        animator.SetTrigger("Pick");
        StartCoroutine(GetStick());
    }

    private void Drop() {
        carrying = false;
        canMove = false;
        animator.SetTrigger("Drop");
        StartCoroutine(LoseStick());
    }

    private IEnumerator LoseStick() {
        yield return new WaitForSeconds(1);
        if (!carrying) {
            canMove = true;
            myStick.SetActive(false);
            GameObject go = GameObject.Instantiate(stickPrefab);
            go.transform.position = myStick.transform.position;
            go.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private IEnumerator GetStick() {
        yield return new WaitForSeconds(0.9f);
        if (carrying) {
            canMove = true;
            audioController.PlayStickPick();
            myStick.SetActive(true);
            Destroy(nextToPickable);
        }
    }

    private IEnumerator LayEgg() {
        animator.SetTrigger("LayEgg");

        StartCoroutine(cameraFollow.Zoom());
        while (transform.rotation.eulerAngles.y != 180 || transform.position.y != 38) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 38, transform.position.z), rotationSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, 180, transform.eulerAngles.z), 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        
        GameObject go = GameObject.Instantiate(eggPrefab);
        go.transform.position = assPosition.position;
        go.transform.rotation = Quaternion.Euler(0, 0, 0);
        cameraFollow.target = go.transform;
        StartCoroutine(cameraFollow.Zoom());
        yield return new WaitForSeconds(1);
        go.GetComponent<Animator>().SetTrigger("Eclode");

        canvasController.EndGame();
    }

    public void GameWin() {
        audioController.PlayWin();
        canMove = false;
        StartCoroutine(LayEgg());
    }
}