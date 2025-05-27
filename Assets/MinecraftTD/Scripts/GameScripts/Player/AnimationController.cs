using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{ 
    public Animator animator; 
    public SpriteRenderer _spriteRenderer;
    public Color blinkColor;
   
    public void AttackAnimation(bool left)
    {
        switch (left)
        {
            case true:
                animator.SetTrigger("AttackL");
                break;
            case false:
                animator.SetTrigger("AttackR");
                break;
        }
    }

    public void TakeDamageAnimation()
    {
        _spriteRenderer.color = blinkColor;
        Invoke("ResetColor", 0.2f);
    }

    public void ResetColor()
    {
        _spriteRenderer.color = Color.white;
    }
}
