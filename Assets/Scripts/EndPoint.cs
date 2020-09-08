using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public GameObject Confetti;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Confetti.SetActive(true);
            SoundManager.Instance.playSound(SoundManager.GameSounds.Win);
            GameManager.Instance.Cheer();
            StartCoroutine(WaitAndcontinue());
        }
    }

    IEnumerator WaitAndcontinue()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.GameWin();
    }
}
