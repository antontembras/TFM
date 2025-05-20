using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public AudioSource[] allAudioSources;
    private List<float> audioSourcesTimeList = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioSource audioS in allAudioSources)
        {
            audioSourcesTimeList.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
                for (int i = 0; i < allAudioSources.Length; i++)
                {
                    allAudioSources[i].time = audioSourcesTimeList[i];
                    allAudioSources[i].Play();
                }
            }
            else
            {
                Time.timeScale = 0;                
                for (int i = 0; i< allAudioSources.Length; i++) {
                    audioSourcesTimeList[i] = allAudioSources[i].time;
                    allAudioSources[i].Stop();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0) {

            Time.timeScale = 1;
            SceneManager.LoadScene("MenuPrincipal");
        }
    }
}
