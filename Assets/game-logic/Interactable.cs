using UnityEngine;

public class Interactable : MonoBehaviour {


  public float radius = 3f;
  public Transform interactionTransform;

  private bool isFocus = false;
  private Transform playerTransform;
  private bool hasInteracted = false;

  void Start() {
    if (interactionTransform == null) {
      interactionTransform = transform;
    }
  }


  void Update() {
    if (isFocus) {
      float distance = Vector3.Distance(playerTransform.position, interactionTransform.position);

      if (!hasInteracted && distance <= radius) {
        hasInteracted = true;
        Interact();
      }
    }

  }

  // Called when the object starts being focused
  public void OnFocused(Transform playerTransform) {
    isFocus = true;
    hasInteracted = false;
    this.playerTransform = playerTransform;
  }

  // Called when the object is no longer focused
  public void OnDefocused() {
    isFocus = false;
    hasInteracted = false;
    playerTransform = null;
  }


  // This method is meant to be overriden
  public virtual void Interact() {
    Debug.Log("INTERACT " + transform.name);
  }

  // shows interaction radius in the Unity scene view
  void OnDrawGizmosSelected() {
    Transform drawFromPosition = interactionTransform != null ? interactionTransform : transform;
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(drawFromPosition.position, radius);
  }

}



