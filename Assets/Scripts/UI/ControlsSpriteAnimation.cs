using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsSpriteAnimation : MonoBehaviour
{

    [Header("Animators")]
    [SerializeField] Animator attackAnimator;
    [SerializeField] Animator jumpAnimator;
    [SerializeField] Animator moveAnimator;

    // Update is called once per frame
    void Update()
    {
        moveAnimator.SetBool("Move",true);
        jumpAnimator.SetBool("Jump",true);
        attackAnimator.SetBool("Attack", true);
    }
}
