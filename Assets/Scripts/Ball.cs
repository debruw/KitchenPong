using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Metal"))
        {
            SoundManager.Instance.playSound((SoundManager.GameSounds)(Random.Range((int)SoundManager.GameSounds.Metal1, (int)SoundManager.GameSounds.Metal3)));
        }
        else if (collision.collider.CompareTag("Ceramic"))
        {
            SoundManager.Instance.playSound(SoundManager.GameSounds.Creamic1);
        }
        else if (collision.collider.CompareTag("Breakable"))
        {
            // game lose
            // mom anger to camera
            
        }
        else
        {
            SoundManager.Instance.playSound(SoundManager.GameSounds.GroundHit);
        }
    }
}
