using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties : MonoBehaviour
{
    public GameObject[] humans;

    public void CheerPeoples()
    {
        foreach (GameObject hmn in humans)
        {
            hmn.GetComponent<Animator>().SetTrigger("Cheer");
        }
    }

    public void YellPeoples()
    {
        foreach (GameObject hmn in humans)
        {
            hmn.GetComponent<Animator>().SetTrigger("Yell");
        }
    }
}
