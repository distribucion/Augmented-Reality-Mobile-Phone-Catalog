using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaweiVideo : MonoBehaviour
{
    public GameObject panelVideoHawei;



    void Start()
    {
        panelVideoHawei.SetActive(false);
    }

    public void panelActivar1()
    {
        if (panelVideoHawei != null)
        {
            bool IsActive = panelVideoHawei.activeSelf;
            panelVideoHawei.SetActive(!IsActive);


        }
    }
}
