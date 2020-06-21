using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator m_Animator;
    Vector3 m_Movement;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();                          // GetComponent part of MonoBehaviour. Generic Method. Generic Methods have NORMAL--> () or TYPE PARAMETERS--> <>
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");                 // Value for horizontal axis
        float vertical = Input.GetAxis("Vertical");                     // Value for vertical axis

        m_Movement.Set(horizontal, 0f, vertical);                       // Set values of 3D vector(X,Y,Z). 0f just means 0.0 (0 as float)
        m_Movement.Normalize();                                         // Keep direction same but change magnitude to 1 or else go diagonal faster than along simple axis

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f); // Verify (horizontal != 0)
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);                     // (Parameter want to set value of, Value you want to set it to)
    }
}
