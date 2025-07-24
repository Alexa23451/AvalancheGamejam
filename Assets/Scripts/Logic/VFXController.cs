using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;

public class VFXController : MonoBehaviour
{
    [SerializeField] Animator vfxSke;
    [SerializeField] AutoDisable autoDisable;
    public string _animName;

    private void OnEnable()
    {
        SetAnimation(_animName, false, 1, autoDisable.DisableObj); 
    }

    public void SetAnimation(string name, bool loop, float timeScale = 1, Action callback = null)
    {
        
    }
}
