using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ExplosionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.Instance.isGameOver)
        {
            return;
        }
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
            GameManager.Instance.Yell();
            StartCoroutine(WaitAndcontinue());
        }
        else if (collision.collider.CompareTag("Burner"))
        {
            // Melt the ball
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Instantiate(ExplosionEffect, transform);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
            StartCoroutine(WaitAndcontinue());
        }
        else
        {
            SoundManager.Instance.playSound(SoundManager.GameSounds.GroundHit);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.Instance.CollectedCoinCount += 5;
            GameManager.Instance.CoinText.text = GameManager.Instance.CollectedCoinCount.ToString();
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitAndcontinue()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.GameLose();
    }
}
