using UnityEngine;

public class AxieFigure : MonoBehaviour
{
    private bool _isFlip = false;
    
    public bool FlipX { get => _isFlip; set => _isFlip = value; }
    
    public void SetAnimation(string animName, float timeScale = 1, bool loop = false)
    {
            
    }
}
