using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class OptionsScript : MonoBehaviour
{

    public Toggle fullsscreenTog, vsyncTog;
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;
    public TMP_Text resolutionLabel;

    public List<string> qualityLevels = new List<string>();
    private int selectedQuality;
    public TMP_Text qualityLabel;




    // Start is called before the first frame update
    void Start()
    {
        fullsscreenTog.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        selectedQuality = QualitySettings.GetQualityLevel();
        UpdateQualityLabel();


        


        bool foundRes = false;

        for(int i = 0; i < resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;
                UpdateResLabel();
            }
        }

        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;
            resolutions.Add(newRes);
            selectedResolution = resolutions.Count - 1;
            UpdateResLabel();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()
    {
        if (selectedResolution > 0 )
        {
            selectedResolution--;
        }
        UpdateResLabel();
    }
    public void ResRight()
    {
        if (selectedResolution < resolutions.Count - 1)
        {
            selectedResolution++;
        }
        UpdateResLabel();
    }

    public void QualityLeft()
    {
        if (selectedQuality > 0)
        {
            selectedQuality--;
        }
        UpdateQualityLabel();
    }
    public void QualityRight()
    {
        if (selectedQuality < qualityLevels.Count - 1)
        {
            selectedQuality++;
        }
        UpdateQualityLabel();
    }


    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }


    public void UpdateQualityLabel()
    {
        qualityLabel.text = qualityLevels[selectedQuality].ToString();
    }
   

    public void Apply()
    {
        Screen.fullScreen = fullsscreenTog.isOn;

        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        QualitySettings.SetQualityLevel(selectedQuality, false);
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullsscreenTog.isOn);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}


