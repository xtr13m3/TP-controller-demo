using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimManager : MonoBehaviour
{
    AimBaseState curState;
    public AimIdle aimIdle = new AimIdle();
    public AimingState aimState = new AimingState();

    [SerializeField] float mouseSensitivity = 1;
    float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineVirtualCamera vc;
    public float aimFov = 40;
    [HideInInspector] public float idleFov;
    [HideInInspector] public float curFov;
    public float fovSmoothing = 10;

    public Transform aimPos;
    [SerializeField] float aimSmoothing = 20;
    [SerializeField] LayerMask aimMask;

    float xFollowPos;
    float yFollowPos, ogYPos;
    [SerializeField] float crouchCamHeight = 0.6f;
    [SerializeField] float shoulderSwapSpeed = 10;
    MovementManager movement;

    void Start()
    {
        movement = GetComponent<MovementManager>();
        xFollowPos = camFollowPos.localPosition.x;
        ogYPos = camFollowPos.localPosition.y;
        yFollowPos = ogYPos;
        vc = GetComponentInChildren<CinemachineVirtualCamera>();
        idleFov = vc.m_Lens.FieldOfView;
        anim = GetComponentInChildren<Animator>();
        SwitchState(aimIdle);
    }

    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        yAxis += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        vc.m_Lens.FieldOfView = Mathf.Lerp(vc.m_Lens.FieldOfView, curFov, fovSmoothing * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask)) aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothing * Time.deltaTime);

        MoveCamera();

        curState.UpdateState(this);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        curState = state;
        curState.EnterState(this);
    }

    void MoveCamera()
    {
        if (Input.GetKeyDown(KeyCode.H)) xFollowPos = -xFollowPos;
        if (movement.currentState == movement.crouchState) yFollowPos = crouchCamHeight;
        else yFollowPos = ogYPos;

        Vector3 newFollowPos = new Vector3(xFollowPos, yFollowPos, camFollowPos.localPosition.z);
        camFollowPos.localPosition = Vector3.Lerp(camFollowPos.localPosition, newFollowPos, shoulderSwapSpeed * Time.deltaTime);
    }
}
