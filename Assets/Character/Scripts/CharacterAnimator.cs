using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

  const float ANIM_SMOOTH_TIME = .1f;
  private NavMeshAgent agent;
  private Animator animator;


  void Start() {
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponentInChildren<Animator>();
  }

  void Update() {

    float speedPercent = agent.velocity.magnitude / agent.speed;
    animator.SetFloat("speedPercent", speedPercent, ANIM_SMOOTH_TIME, Time.deltaTime);


  }




}

