using UnityEngine;

/*
 * Camera following the player
 * Left click on ground to move player
 * Hold right-click to pan camera
 * Scrollwheel to zoom
 *
 * 
 */
public class CameraController : MonoBehaviour {

  // The target is set as the 'Camera target' component of the Player object
  public Transform target;
  // The camera's initial offset from the target
  public Vector3 offset;

  public float currentZoom;
  public float maxZoom;
  public float minZoom;
  public float zoomSensitivity;
  float dst;
  public float pitch;

  public float yawSpeed;

  // If a variable is private it doesn't appear in the inspector
  private float horizontalRotate;
  private float verticalRotate = 0;

  private float zoomSmoothV;
  private float targetZoom;


  void Start() {
    dst = offset.magnitude;
    transform.LookAt(target);
    targetZoom = currentZoom;
  }


  private float mouseSensitivity = 0.2f;
  private Vector3 lastPosition;
  private Vector3 delta;

  void Update() {

    float scroll = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSensitivity;

    if (scroll != 0f) {
      targetZoom = Mathf.Clamp(targetZoom - scroll, minZoom, maxZoom);
    }
    currentZoom = Mathf.SmoothDamp(currentZoom, targetZoom, ref zoomSmoothV, 0.15f);


    if (Input.GetMouseButtonDown(1)) {
      lastPosition = Input.mousePosition;
    }

    if (Input.GetMouseButton(1)) {
      delta = Input.mousePosition - lastPosition;
      lastPosition = Input.mousePosition;
    }

    if (Input.GetMouseButtonUp(1)) {
      delta.x = 0;
      delta.y = 0;
      delta.z = 0;
    }
  }


  // LateUpdate is similar to Update but called right after
  void LateUpdate() {
    transform.position = target.position - offset * currentZoom;
    transform.LookAt(target.position);

    horizontalRotate += delta.x * mouseSensitivity;
    transform.RotateAround(target.position, Vector3.up, horizontalRotate);

    verticalRotate -= delta.y * mouseSensitivity;
    if (verticalRotate < -25) verticalRotate = -25;
    if (verticalRotate > 40) verticalRotate = 40;
    transform.RotateAround(target.position, transform.right, verticalRotate);
  }



}



