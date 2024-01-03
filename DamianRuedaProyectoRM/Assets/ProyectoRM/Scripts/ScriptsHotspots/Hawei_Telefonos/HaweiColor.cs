using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaweiColor : MonoBehaviour
{
    public GameObject panelColorHawei;



    void Start()
    {
        panelColorHawei.SetActive(false);
    }

    public void panelActivar1()
    {
        if (panelColorHawei != null)
        {
            bool IsActive = panelColorHawei.activeSelf;
            panelColorHawei.SetActive(!IsActive);


        }
    }
}
