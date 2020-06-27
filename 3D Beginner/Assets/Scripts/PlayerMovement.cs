using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;                                       // Most character will ever turn is 3 radians. turnSpeed = 3 means take second for character to turn around. 6 means half second.
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;                        // Storing rotation

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();                          // GetComponent part of MonoBehaviour. Generic Method. Generic Methods have NORMAL--> () or TYPE PARAMETERS--> <>
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");                 // Value for horizontal axis
        float vertical = Input.GetAxis("Vertical");                     // Value for vertical axis

        m_Movement.Set(horizontal, 0f, vertical);                       // Set values of 3D vector(X,Y,Z). 0f just means 0.0 (0 as float)
        m_Movement.Normalize();                                         // Keep direction same but change magnitude to 1 or else go diagonal faster than along simple axis

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f); // Verify (horizontal != 0)
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);                     // (Parameter want to set value of, Value you want to set it to)

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);      // RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta). Last 2 are amount of change between starting and target vector. Change magnitude by 0. 
                                                                                                                            //Time.deltaTime is time since previous frame (e.g. 60 feames per sec means called 60 times per sec)
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);                   // MovePosition(Vector3 position) -- Move RigidBody towards position. Current position + movement vector(actual direction want to move) * magnitude of Animator's deltaPosition (change in position due to root motion that would have been applied to this fram)
        m_Rigidbody.MoveRotation(m_Rotation);                                                                               // Same as MovePosition but for rotation. Except directly setting rotation
    }
}
