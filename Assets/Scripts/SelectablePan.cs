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
    }

    public void CloseOutlines()
    {
        Stick1.GetComponent<Outline>().enabled = false;
        Stick2.GetComponent<Outline>().enabled = false;
    }

    public float fShakeImpulse = 0.0f;  //Camera Shake Impulse

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void FixedUpdate()
    {
        //make the camera shake if the fCamShakeImpulse is not zero
        if (fShakeImpulse > 0.0f)
            shakeObject();
    }

    /*
    *	FUNCTION: Set the intensity of camera vibration
    *	PARAMETER 1: Intensity value of the vibration
    */
    public void setObjectShakeImpulseValue(int iShakeValue)
    {
        if (iShakeValue == 1)
            fShakeImpulse = 0.2f;
        else if (iShakeValue == 2)
            fShakeImpulse = 0.03f;
        else if (iShakeValue == 3)
            fShakeImpulse = 1.3f;
        else if (iShakeValue == 4)
            fShakeImpulse = 1.5f;
        else if (iShakeValue == 5)
            fShakeImpulse = 1.3f;
    }

    /*
    *	FUNCTION: Make the camera vibrate. Used for visual effects
    */
    public void shakeObject()
    {
        transform.position += new Vector3(Random.Range(-fShakeImpulse, fShakeImpulse), 0, 0);
        fShakeImpulse -= Time.deltaTime * fShakeImpulse * 6.0f;
        if (fShakeImpulse < 0.01f)
            fShakeImpulse = 0.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            setObjectShakeImpulseValue(2);
        }
    }
}