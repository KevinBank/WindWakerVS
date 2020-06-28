using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZeldaController : MonoBehaviour
{
    #region Variables
    [Header("Values")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollDuration;
    [Header("GameObject")]
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject cam;
    [SerializeField] private Transform target, head;
    [Header("Anim")]
    [SerializeField] private GameObject walkingModel;
    [SerializeField] private GameObject lockonModel;
    [SerializeField] private GameObject rollModel;

    private float rollSpeedIns;
    private Vector3 newForward;
    private Vector2 move;
    private Vector3 dashDir;
    private Vector2 inputMove;
    [SerializeField] private MovementState state = MovementState.NORMAL;
    #endregion

    void Update()
    {
        switch (state)
        {
            case MovementState.NORMAL:
                Movement();
                RotateToMoveDiraction();
                break;
            case MovementState.LOCKED_ON:
                Movement();
                RotateToTarget();
                break;
            case MovementState.DASH:
                Dashing();
                break;
            default:
                Movement();
                RotateToMoveDiraction();
                break;
        }
    }

    private void Movement()
    {
        cam.GetComponent<ZeldasCameraController>().CameraSpeed = movementSpeed;
        move = new Vector2(inputMove.x, inputMove.y) * movementSpeed;
        newForward = cam.transform.rotation * new Vector3(move.x, 0, move.y);
        transform.position += newForward * Time.deltaTime;

        if(state == MovementState.NORMAL)
        {
            walkingModel.SetActive(true);
            lockonModel.SetActive(false);
            rollModel.SetActive(false);
            
            if(inputMove == Vector2.zero)
            {
                walkingModel.GetComponent<Animator>().SetBool("Idle", true);
                walkingModel.GetComponent<Animator>().SetBool("Walking", false);
            }
            else
            {
                walkingModel.GetComponent<Animator>().SetBool("Idle", false);
                walkingModel.GetComponent<Animator>().SetBool("Walking", true);

                walkingModel.GetComponent<Animator>().SetFloat("X", inputMove.x);
                walkingModel.GetComponent<Animator>().SetFloat("Y", inputMove.y);
            }
        }
        if(state == MovementState.LOCKED_ON)
        {
            walkingModel.SetActive(false);
            lockonModel.SetActive(true);
            rollModel.SetActive(false);
            if (inputMove == Vector2.zero)
            {
                lockonModel.GetComponent<Animator>().SetBool("isIdle", true);
                lockonModel.GetComponent<Animator>().SetBool("isMoving", false);
            }
            else
            {
                lockonModel.GetComponent<Animator>().SetBool("isIdle", false);
                lockonModel.GetComponent<Animator>().SetBool("isMoving", true);
                if (inputMove.x < 0)
                    lockonModel.transform.localScale = new Vector3(-0.08543245f, 0.08543245f, 0.08543245f);
                else
                    lockonModel.transform.localScale = new Vector3(0.08543245f, 0.08543245f, 0.08543245f);
            }
        }
    }

    private void RotateToMoveDiraction()
    {
        playerModel.transform.LookAt(playerModel.transform.position + newForward);
    }
    private void RotateToTarget()
    {
        head.LookAt(target);
        playerModel.transform.eulerAngles = new Vector3(0, head.eulerAngles.y, 0);
    }

    private MovementState oldState;
    public void StartDash()
    {
        if(state != MovementState.DASH)
        {
            oldState = state;
            state = MovementState.DASH;
            rollSpeedIns = rollSpeed;
            newForward = cam.transform.rotation * new Vector3(inputMove.x, 0, inputMove.y);
            dashDir = newForward;
            if(state == MovementState.NORMAL && inputMove != Vector2.zero)
            {
                Dashing();
            }
        }
    }
    private void Dashing()
    {
        if (oldState == MovementState.NORMAL)
        {
            walkingModel.SetActive(false);
            lockonModel.SetActive(false);
            rollModel.SetActive(true);
            rollModel.GetComponent<Animator>().SetBool("Roll", true);
        }
        if (oldState == MovementState.LOCKED_ON)
        {
            Debug.Log("dd");
            walkingModel.SetActive(false);
            lockonModel.SetActive(true);
            rollModel.SetActive(false);
            lockonModel.GetComponent<Animator>().SetBool("Dash", true);
        }
        transform.position += new Vector3(dashDir.x, 0, dashDir.z) * rollSpeedIns * Time.deltaTime;
        rollSpeedIns -= rollSpeedIns * rollDuration * Time.deltaTime;
        if (rollSpeedIns < 10f)
        {
            state = oldState;
            rollModel.GetComponent<Animator>().SetBool("Roll", false);
            lockonModel.GetComponent<Animator>().SetBool("Dash", false);
        }
    }

    public Vector2 InputMove { set { inputMove = value; } }
    public MovementState State { set { state = value; } }
    public Transform Target { set { target = value; } }
}