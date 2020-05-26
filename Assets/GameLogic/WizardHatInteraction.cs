using UnityEngine;


/*
 *
 * WizardHatInteraction script is assigned to the hat-stand game object in the cottage
 *
 * The hat-stand and wizard-hat game objects in the cottage is assigned Layer: Interactable
 *
 *
 *
 *
 *
 *
 */
public class WizardHatInteraction : Interactable {

  public GameObject hat;
  public SkinnedMeshRenderer playerMesh;
  public SkinnedMeshRenderer playerHat;

  private bool hatEquipped = false;
  private SkinnedMeshRenderer playerHatInstantiated = null;



  private void AttachHat(SkinnedMeshRenderer hatMesh) {
    playerHatInstantiated = Instantiate(hatMesh) as SkinnedMeshRenderer;
    playerHatInstantiated.bones = playerMesh.bones;
    playerHatInstantiated.rootBone = playerMesh.rootBone;
  }



  public override void Interact() {

    if (!hatEquipped) {
      AttachHat(playerHat);
      hat.SetActive(false);
      hatEquipped = true;
    } else {
      Destroy(playerHatInstantiated.gameObject);
      hat.SetActive(true);
      hatEquipped = false;
    }

  }


}










