using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectablePan : MonoBehaviour
{
    public float clamp;
    public GameObject Stick1, Stick2;

    private void OnMouseUp()
    {
        Debug.Log("Selected!");
        if (GameManager.Instance.selecTedPan != null)
        {
            GameManager.Instance.selecTedPan.GetComponent<SelectablePan>().CloseOutlines();
        }
        GameManager.Instance.selecTedPan = gameObject;
        Stick1.GetComponent<Outline>().enabled = true;
        Stick2.GetComponent<Outline>().enabled = true;
        GameManager.Instance.MovementSlider.value = transform.localPosition.y;
        GameManager.Instance.SliderAndButton.gameObject.SetActive(true);
        if(GameManager.Instance.Tuto11.activeSelf)
        {
            GameManager.Instance.Tuto11.SetActive(false);
            GameManager.Instance.Tuto12.SetActive(true);
        }
        if(GameManager.Instance.Tuto21.activeSelf)
        {
            GameManager.Instance.Tuto21.SetActive(false);
            GameManager.Instance.Tuto22.SetActive(true);
        }
    }

    public void CloseOutlines()
    {
        Stick1.GetComponent<Outline>().enabled = false;
        Stick2.GetComponent<Outline>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

        }
    }
}