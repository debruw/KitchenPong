using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public GameObject Confetti;
    public GameObject[] humans;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Confetti.SetActive(true);
            SoundManager.Instance.playSound(SoundManager.GameSounds.Win);
            foreach (GameObject hmn in humans)
            {
                hmn.GetComponent<Animator>().SetTrigger("Cheer");
            }
            GameManager.Instance.Win();
        }
    }
}
