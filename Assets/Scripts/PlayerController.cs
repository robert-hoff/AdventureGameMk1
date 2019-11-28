using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour {

  // The movement mask is applied to the objects that make up the ground
  // (in the Layer drop-down these objects are assigned 'Ground')
  public LayerMask movementMask;
  NavMeshAgent agent;
  Camera cam;

  // Get references
  void Start() {
    agent = GetComponent<NavMeshAgent>();
    cam = Camera.main;
  }

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, movementMask)) {
        agent.SetDestination(hit.point);
      }
    }


  }

}


