using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILockMechanic
{
    
}

public class LockManager : MonoBehaviour, ITriggerObject, ILockMechanic
{
    public ParticleSystem _playerHit;
    [SerializeField] private Transform _playerHitVfxSpawn;
    
    public void OnTrigger(GameObject triggerObj)
    {
        if (triggerObj.TryGetComponent(out PlayerKeyLock playerKeyLock))
        { 
            triggerObj.transform.GetComponent<PlayerMovement>()._axieFigure.SetAnimationWin();
            SoundManager.Instance.Play(Sounds.LOCK);
            var playerHitVFX = Pooling.Instantiate(_playerHit, _playerHitVfxSpawn.position, _playerHitVfxSpawn.rotation); 
            playerHitVFX.Play();
            
            Pooling.Destroy(gameObject);
        }
    }
}