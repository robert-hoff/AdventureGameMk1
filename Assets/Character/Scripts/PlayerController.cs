 using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

  public delegate void OnFocusChanged(Interactable newFocus);
  public OnFocusChanged onFocusChangedCallback;


  public Interactable interactableBeingFocussed;


  public LayerMask movementMask;
  public LayerMask interactionMask;


  private PlayerMotor motor;
  private Camera cam;

  // Get references
  void Start() {
    motor = GetComponent<PlayerMotor>();
    cam = Camera.main;
  }

  void Update() {


    if (Input.GetMouseButtonDown(0)) {
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, 1000f, movementMask)) {
        motor.MoveToPoint(hit.point);
        SetFocus(null);
      }
    }

    if (Input.GetMouseButtonDown(1)) {
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, 100f, interactionMask)) {
        SetFocus(hit.collider.GetComponent<Interactable>());
      }
    }

  }

  void SetFocus(Interactable newFocus) {
    if (onFocusChangedCallback != null) {
      onFocusChangedCallback.Invoke(newFocus);
    }

    if (interactableBeingFocussed != newFocus && interactableBeingFocussed != null) {
      interactableBeingFocussed.OnDefocused();
    }

    interactableBeingFocussed = newFocus;
    if (interactableBeingFocussed != null) {
      interactableBeingFocussed.OnFocused(transform);
    }

  }

}



