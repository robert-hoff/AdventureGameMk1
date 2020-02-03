using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerController))]
public class PlayerMotor : MonoBehaviour {

  private Transform target;
  private NavMeshAgent agent;

  void Start() {
    agent = GetComponent<NavMeshAgent>();
    GetComponent<PlayerController>().onFocusChangedCallback += OnFocusChanged;
  }

  void Update() {
    if (target != null) {
      MoveToPoint(target.position);
      FaceTarget();
    }
  }

  public void MoveToPoint(Vector3 point) {
    agent.SetDestination(point);
  }

  private void OnFocusChanged(Interactable newFocus) {
    if (newFocus != null) {
      agent.stoppingDistance = newFocus.radius * .9f;
      agent.updateRotation = false;

      target = newFocus.interactionTransform;
    } else {
      agent.stoppingDistance = 0f;
      agent.updateRotation = true;
      target = null;
    }
  }


  /*
   * FaceTarget()
   * The method is used by Update() in cases there exists a moving object that is under focus.
   * As the object is moving we want the player rotation to update.
   *
   */
  private void FaceTarget() {
    Vector3 direction = (target.position - transform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
  }


}
