using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class loadjob : MonoBehaviour
{
    public GameObject ClickedButton;
    private jobdetails details;
    public void LoadJobScene()
    {
        ClickedButton = EventSystem.current.currentSelectedGameObject;
        details = ClickedButton.GetComponent<jobdetails>();
        JobDetails.sentence = details.sentence;
        JobDetails.var = details.var;
        JobDetails.video = details.video;
        // SceneManager.LoadScene("Job");
    } 

    public static class JobDetails
    {
        public static string sentence;
        public static string var;
        public static string video;
    }
}
