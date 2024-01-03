using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPanel : MonoBehaviour
{
    public GameObject panel;



    void Start()
    {
        panel.SetActive(false);
    }

    public void panelActivar()
    {
        if (panel != null)
        {
            bool IsActive = panel.activeSelf;
            panel.SetActive(!IsActive);


        }
    }

}
