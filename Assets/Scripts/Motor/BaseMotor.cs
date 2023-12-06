using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMotor : MonoBehaviour
{
    protected CharacterController controller;
    protected BaseState state;
    protected Transform thisTransform;

    private float movementSpeed = 10f;
    private float gravity = 25.0f;
    private float terminalVelocity = 30.0f;
    private float baseJumpForce = 7.0f;
    //ray length
    private float groundRayDistance = 0.5f;
    //raycast will be cast at groundRayInnerOffset towards the center distance
    public float groundRayInnerOffset = 0.01f;

    public float MovementSpeed { get { return movementSpeed; } }
    public float Gravity { get { return gravity; } }
    public float TerminalVelocity { get { return terminalVelocity; } }
    public float VerticalVelocity { set; get; }
    public float JumpForce { get { return baseJumpForce; } }
    public Vector3 MoveVector { set; get; }
    public Quaternion RotationQuaternion { set; get; } //not sure, might check again


    protected abstract void UpdateMotor();

    protected virtual void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        thisTransform = transform;

    }
    private void Update()
    {
        UpdateMotor();
    }

    protected virtual void Move()
    {
        controller.Move(MoveVector * Time.deltaTime * 0.05f * GetComponent<Enemy>().BaseMovementSpeed);
    }

    protected virtual void Rotate()
    {
        // controller.Rotate
    }

    public void ChangeState(string StateName)
    {
        System.Type t = System.Type.GetType(StateName);
        state.Destruct();
        state = gameObject.AddComponent(t) as BaseState;
        state.Construct();
    }

    public virtual bool Grounded()
    {
        RaycastHit hit;
        Vector3 ray;

        float yRay = (controller.bounds.center.y - controller.bounds.extents.y) + 0.3f,
        centerX = controller.bounds.center.x,
        centerZ = controller.bounds.center.z,
        extentX = controller.bounds.extents.x - groundRayInnerOffset,
        extentZ = controller.bounds.extents.z - groundRayInnerOffset;
        //Middle rayCast
        ray = new Vector3(centerX, yRay, centerZ);
        Debug.DrawRay(ray, Vector3.down, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }
        ray = new Vector3(centerX + extentX, yRay, centerZ + extentZ);
        Debug.DrawRay(ray, Vector3.down, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }
        ray = new Vector3(centerX - extentX, yRay, centerZ + extentZ);
        Debug.DrawRay(ray, Vector3.down, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }
        ray = new Vector3(centerX - extentX, yRay, centerZ - extentZ);
        Debug.DrawRay(ray, Vector3.down, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }
        ray = new Vector3(centerX + extentX, yRay, centerZ - extentZ);
        Debug.DrawRay(ray, Vector3.down, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }
        return false;
    }
}
