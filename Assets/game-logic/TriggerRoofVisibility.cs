using UnityEngine;

public class TriggerRoofVisibility : MonoBehaviour {

  public GameObject houseRoof;

  void OnTriggerEnter(Collider obj) {
    if(obj.tag == "Player") {
      houseRoof.SetActive(false);
    }
  }

  void OnTriggerExit(Collider obj) {
    if (obj.tag == "Player") {
      houseRoof.SetActive(true);
    }
  }


}






