using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRTeleport : MonoBehaviour
{
    [SerializeField]
    private GameObject _teleportPointerPrefab;
    [SerializeField]
    private SteamVR_Action_Boolean _teleportAction;
    private SteamVR_Behaviour_Pose _pose = null;
    private bool _hasPostition = false;
    private GameObject _teleportPointer = null;
    private bool _isTeleporting = false;
    private float _fadeTime = 0.5f;
    // Start is called before the first frame update
    void Awake()
    {
        _pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void Start()
    {
        _teleportPointer = Instantiate<GameObject>(_teleportPointerPrefab);
        _teleportPointer.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        // Pointer
        _hasPostition = UpdatePointer();
        _teleportPointer.SetActive(_hasPostition);

        // Teleport
        if(_teleportAction.GetStateUp(_pose.inputSource))
        {
            TryTeleport();
        }
    }

    private void TryTeleport()
    {
        // Check for valid position, andif already teleporting
        if (!_hasPostition || _isTeleporting) return;

        Debug.Log("Teleport");
        // Get camera rig and head position
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        // Figure out transition
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translateVector = _teleportPointer.transform.position - groundPosition;

        // Move
        StartCoroutine(MoveRig(cameraRig, translateVector));
    }

    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        Debug.Log("Move");
        // Flag
        _isTeleporting = true;

        // Fade to black
        SteamVR_Fade.Start(Color.black, _fadeTime, true);
        yield return new WaitForSeconds(_fadeTime);

        // Apply translation
        cameraRig.position += translation;

        // Fade to clear
        SteamVR_Fade.Start(Color.clear, _fadeTime, true);

        // Deflag
        _isTeleporting = false;
    }

    private bool UpdatePointer()
    {
        // Ray from controller
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // If it's a hit
        if(Physics.Raycast(ray, out hit))
        {
            _teleportPointer.SetActive(true);
            _teleportPointer.transform.position = hit.point;
            return true;
        }
        _teleportPointer.SetActive(false);
        //If it's not a hit
        return false;
    }
}
