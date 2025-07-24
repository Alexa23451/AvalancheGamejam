using UnityEngine;

public class AxieFigure : MonoBehaviour
{
    public Animator animator;
    
    public readonly string IdleAnim = "Idle";
    public readonly string WinAnim = "Win";
    public readonly string AttactAnim = "Attack";
    
    private SpriteRenderer _renderer;
    private bool _isFlip = false;
    
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    public bool FlipX { get => !_renderer.flipX; set => _renderer.flipX = !value; }
    
    public void SetAnimation(string animName, float timeScale = 1, bool loop = false)
    {
          
    }
    public void SetAnimationWin()
    {
        animator.SetTrigger(WinAnim);
    }
    
    public void SetAnimationAttack()
    {
        animator.SetTrigger(AttactAnim);
    }
    
}
