using UnityEngine;

public class AttachEquipment : MonoBehaviour {


  public SkinnedMeshRenderer playerMesh;
  public SkinnedMeshRenderer playerHair;
  public SkinnedMeshRenderer playerShirt;
  public SkinnedMeshRenderer playerTrousers;
  public SkinnedMeshRenderer playerShoes;

  public SkinnedMeshRenderer playerSword;
  private SkinnedMeshRenderer playerSwordInstantiated = null;


  private Animator animator;


  void Start() {
    animator = GetComponentInChildren<Animator>();
    AttachMesh(playerHair);
    AttachMesh(playerShirt);
    AttachMesh(playerTrousers);
    AttachMesh(playerShoes);
  }

  private void AttachMesh(SkinnedMeshRenderer playerEquipmentMesh) {
    if (playerEquipmentMesh == null) return;
    SkinnedMeshRenderer newMesh = Instantiate(playerEquipmentMesh) as SkinnedMeshRenderer;
    newMesh.bones = playerMesh.bones;
    newMesh.rootBone = playerMesh.rootBone;
  }

  private void AttachSword(SkinnedMeshRenderer swordMesh) {
    playerSwordInstantiated = Instantiate(swordMesh) as SkinnedMeshRenderer;
    playerSwordInstantiated.bones = playerMesh.bones;
    playerSwordInstantiated.rootBone = playerMesh.rootBone;
  }





  private bool swordEquipped = false;


  void Update() {
    if (Input.GetKeyDown(KeyCode.S)) {
      if (!swordEquipped) {
        animator.SetLayerWeight(1, 1); // right hand grip
        AttachSword(playerSword);
        swordEquipped = true;
      } else {
        animator.SetLayerWeight(1, 0); // release grip
        Destroy(playerSwordInstantiated.gameObject);
        swordEquipped = false;
      }
    }
  }


}





