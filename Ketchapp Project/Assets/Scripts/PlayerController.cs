using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public enum PlayerState
{
    Default,
    DirtyArea,
    Stopped
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController pc;

    [Header("References")]
    public Transform body;
    public Transform modelTransform;
    public Animator myAnim;
    public CinemachineVirtualCamera dirtyAreaVC;

    [Space]
    [Header("Variables to tweak")]
    public float positionLerpSpeed;
    public float verticalSpeed;
    public float movementMultiplierInToxicArea;
    public float maxZRotationWhenMoving;

    public float reviveOffsetZ;
    public float timeToRevive;
    public AnimationCurve reviveAnimCurve;

    public float timeToNextDirtyArea;
    public AnimationCurve dirtyAreaAnimCurve;


    //HIDDEN PUBLIC VARIABLES
    [HideInInspector] public DirtyArea currentDirtyArea;

    //PRIVATE VARIABLES
    bool verticalMovementLocked = false;
    bool horizontalMovementLocked = false;
    float horizontalMovementMultiplier = 1;
    float verticalMovementMultiplier = 1;
    PlayerState myState = PlayerState.Default;

    void Awake()
    {
        pc = this;
    }
    void Update()
    {
        UpdateHorizontally();
        UpdateVertically();
        UpdateState();
        CleanDirtyArea();
    }

    void UpdateHorizontally()
    {
        if (!horizontalMovementLocked && Input.GetMouseButton(0))
        {
            //POSITION
            Vector3 i_wantedPosition = body.position;
            i_wantedPosition.x = GameManager.gm.RemapValues(Input.mousePosition.x, 0, Screen.width, GameManager.gm.minX.position.x, GameManager.gm.maxX.position.x);
            body.position = Vector3.Lerp(body.position, i_wantedPosition, positionLerpSpeed * horizontalMovementMultiplier);

            //ROTATION
            float maxDistance = GameManager.gm.maxX.position.x - GameManager.gm.minX.position.x;
            float distance = i_wantedPosition.x - body.position.x;
            float rotationPercentage = distance / maxDistance;
            modelTransform.rotation = Quaternion.Euler(0, 0, maxZRotationWhenMoving * rotationPercentage);
        }
    }

    void UpdateVertically()
    {
        if (!verticalMovementLocked)
        {
            body.position += Vector3.forward * verticalSpeed * Time.deltaTime * verticalMovementMultiplier;
        }
    }

    public void ChangeState(PlayerState _newState)
    {
        ExitState();
        myState = _newState;
        EnterState();
    }

    void UpdateState()
    {
        switch (myState)
        {
            case PlayerState.Default:
                if (currentDirtyArea != null)
                {
                    ChangeState(PlayerState.DirtyArea);
                }
                break;
            case PlayerState.DirtyArea:
                if (currentDirtyArea == null)
                {
                    ChangeState(PlayerState.Default);
                }
                break;
            case PlayerState.Stopped:
                break;
        }
    }

    void EnterState()
    {
        switch (myState)
        {
            case PlayerState.Default:
                break;
            case PlayerState.DirtyArea:
                SetMovementLock(true, true);
                SetMovementLock(false, true);
                StartCoroutine(MoveToNextDirtyArea(currentDirtyArea.GetNextDirtyAreaTransform()));
                dirtyAreaVC.m_Priority = 11;
                myAnim.SetBool("DirtyArea", true);
                break;
            case PlayerState.Stopped:
                PlayerController.pc.horizontalMovementLocked = true;
                PlayerController.pc.verticalMovementLocked = true;
                GameManager.gm.FailLevel();
                myAnim.SetTrigger("DeathTrigger");
                break;
        }
    }

    void ExitState()
    {
        switch (myState)
        {
            case PlayerState.Default:
                break;
            case PlayerState.DirtyArea:
                SetMovementLock(true, false);
                SetMovementLock(false, false);
                dirtyAreaVC.m_Priority = 9;
                myAnim.SetBool("DirtyArea", false);
                myAnim.SetBool("SwipeRight", false);
                break;
            case PlayerState.Stopped:
                PlayerController.pc.horizontalMovementLocked = false;
                PlayerController.pc.verticalMovementLocked = false;
                break;
        }
    }

    void CleanDirtyArea()
    {
        if (Input.GetMouseButtonDown(0) && myState == PlayerState.DirtyArea)
        {

            myAnim.SetBool("SwipeRight", !myAnim.GetBool("SwipeRight"));
            currentDirtyArea.CleanDirtyArea();
            if(currentDirtyArea != null)
            {
                StartCoroutine(MoveToNextDirtyArea(currentDirtyArea.GetNextDirtyAreaTransform()));
            }
        }
    }

    IEnumerator MoveToNextDirtyArea(Transform _target)
    {
        Vector3 i_startPos = body.position;
        Vector3 i_endPos = new Vector3(body.position.x, body.position.y, _target.position.z);
        float i_timer = 0;
        while(i_timer < timeToNextDirtyArea)
        {
            i_timer += Time.deltaTime;
            body.position = Vector3.Lerp(i_startPos, i_endPos, dirtyAreaAnimCurve.Evaluate(i_timer / timeToNextDirtyArea));
        }
        yield return null;
    }

    public void SetMovementLock(bool _vertical, bool _lock)
    {
        if (_vertical)
        {
            verticalMovementLocked = _lock;
        }
        else
        {
            horizontalMovementLocked = _lock;
        }
    }

    public void GettingRevived()
    {
        myAnim.SetTrigger("ReviveTrigger");
        StartCoroutine(GettingRevivedCoroutine());
    }

    IEnumerator GettingRevivedCoroutine()
    {
        Vector3 i_startPosition = body.position;
        Vector3 i_endPosition = body.position + Vector3.forward * reviveOffsetZ;
        float i_timer = 0;
        while(i_timer < timeToRevive)
        {
            i_timer += Time.deltaTime;
            body.position = Vector3.Lerp(i_startPosition, i_endPosition, reviveAnimCurve.Evaluate(i_timer / timeToRevive));
            yield return new WaitForEndOfFrame();
        }
        yield return null;
        ChangeState(PlayerState.Default);
    }
}
