using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class SelectablePan : MonoBehaviour
{
    public float clamp;
    public GameObject Stick1, Stick2;
    public GameObject canvasMove, canvasRot;

    private void Start()
    {
        GetComponent<Outline>().enabled = false;
        Stick1.GetComponent<Outline>().enabled = false;
        Stick2.GetComponent<Outline>().enabled = false;
        if(GameManager.Instance.currentLevel < 3)
        {
            canvasMove.SetActive(true);
        }
        if(GameManager.Instance.currentLevel > 4 && GameManager.Instance.currentLevel < 8)
        {
            canvasMove.SetActive(false);
            GameManager.Instance.MovementSlider.gameObject.SetActive(false);
            canvasRot.SetActive(true);
        }
    }

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
        GetComponent<Outline>().enabled = true;
        GameManager.Instance.MovementSlider.value = (transform.localPosition.y / 4) * 100;
        //if (GameManager.Instance.currentLevel > 4)
        //{
        //    GameManager.Instance.RotationSlider.value = (transform.localRotation.y / 2) * 100; 
        //}
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
        GetComponent<Outline>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

        }
    }
}