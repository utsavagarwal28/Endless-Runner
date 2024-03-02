using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Quaternion defaultRotation;

    [Header("Elements")]
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRunAnimation()
    {
        transform.rotation = defaultRotation;
        animator.Play("Spot_Running");
    }

    public float jumpSpeed;

    public void PlayJumpAnimation(/*float jumpDuration*/)
    {
        /*// Calculate animation speed multiplier based on jump duration
        float animationSpeedMultiplier = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / jumpDuration;

        // Set the "JumpSpeed" parameter in the animator controller
        animator.SetFloat("JumpSpeed", animationSpeedMultiplier);*/

        // Play the jump animation
        animator.Play("Jump");
    }

    /* public void PlayJumpUpAnimation()
     {
         animator.Play("JumpUp");

     }public void PlayJumpDownAnimation()
     {
         animator.Play("JumpDown");
     }*/
}