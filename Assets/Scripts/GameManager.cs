using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public GameObject soundManagerPrefab;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        Instantiate(soundManagerPrefab);
    }
    [HideInInspector]
    public GameObject selecTedPan;
    public bool isGameOver = false;

    #region UI Elements
    public Slider MovementSlider, RotationSlider;
    public GameObject SliderAndButton;
    public GameObject WinPanel, LosePanel;
    #endregion


    public void Win()
    {
        isGameOver = true;
        WinPanel.SetActive(true);
    }

    public void Lose()
    {
        isGameOver = true;
        LosePanel.SetActive(true);
    }

    public void MoveSelectedPan()
    {
        selecTedPan.transform.localPosition = new Vector3(selecTedPan.transform.localPosition.x, MovementSlider.value, selecTedPan.transform.localPosition.z);
    }

    public void RotateSelectedPan()
    {
        selecTedPan.transform.localRotation = new Quaternion(RotationSlider.value, selecTedPan.transform.localRotation.y, selecTedPan.transform.localRotation.z, selecTedPan.transform.localRotation.w);
    }

    public void OkayButtonClick()
    {
        selecTedPan.GetComponent<SelectablePan>().CloseOutlines();
        selecTedPan = null;
    }
}
