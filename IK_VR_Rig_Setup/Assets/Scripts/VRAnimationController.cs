using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class VRAnimationController : MonoBehaviour
{
    [SerializeField] private InputActionReference move;

    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        move.action.started += AnimateLegs;
        move.action.canceled += StopAnimation;
    }

    private void StopAnimation(InputAction.CallbackContext obj)
    {
        animator.SetBool("IsWalking", false);
        animator.SetFloat("animSpeed", 0);
    }

    private void AnimateLegs(InputAction.CallbackContext obj)
    {
        bool isMovingForward = move.action.ReadValue<Vector2>().y > 0;

        if(isMovingForward) 
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("animSpeed", 1);
        }
        else
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("animSpeed", -1);
        }
    }

    private void OnDisable()
    {
        move.action.started -= AnimateLegs;
        move.action.canceled -= StopAnimation;

    }


}
