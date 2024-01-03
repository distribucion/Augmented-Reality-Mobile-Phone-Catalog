using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerVideo : MonoBehaviour
{
    public GameObject panel1;



    void Start()
    {
        panel1.SetActive(false);
    }

    public void panelActivarUno()
    {
        if (panel1 != null)
        {
            bool IsActive = panel1.activeSelf;
            panel1.SetActive(!IsActive);


        }
    }
}
