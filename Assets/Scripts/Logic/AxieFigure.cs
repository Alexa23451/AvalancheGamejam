using UnityEngine;

public class AxieFigure : MonoBehaviour
{
    public Animator animator;
    
    private readonly string WinAnim = "Win";
    private  readonly string AttactAnim = "Attack";
    
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
