using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class refresh : MonoBehaviour
{
    public GameObject ButtonPanel;
    public GameObject JobButton;
    public void GetJobList()
    {
        //string user_id = login.Global.user_id;
        string user_id = "test";
        StartCoroutine(JobRequest("http://localhost:5000/ar/get_job/", user_id));
    }

    IEnumerator JobRequest(string url, string user_id)
    {
        string joburl = url + user_id + "/";
        UnityWebRequest uwr = UnityWebRequest.Get(joburl);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }

        JobsList JobList = new JobsList();
        JobList = JsonUtility.FromJson<JobsList>(uwr.downloadHandler.text);
        foreach (Job job in JobList.jobs)
        {
            GameObject newButton = Instantiate(JobButton) as GameObject;
            Transform ButtonText = newButton.transform.Find("Text");
            ButtonText.GetComponent<Text>().text = job.sentence;
            jobdetails JobDetails = newButton.GetComponent<jobdetails>();
            JobDetails.sentence = job.sentence;
            JobDetails.var = job.var;
            JobDetails.video = job.video;
            int LastChildIndex = ButtonPanel.transform.childCount - 1;
            int y = 300 * LastChildIndex;
            newButton.transform.position = new Vector3(0, y, 0);
            newButton.transform.SetParent(ButtonPanel.transform, false);
        }
        
    }

    [Serializable]
    public class Job
    {
        public string sentence;
        public string var;
        public string video;
    }
    [Serializable]
    public class JobsList
    {
        public Job[] jobs;
    }
   
}


