using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class refresh : MonoBehaviour
{
    public void GetJobList()
    {
        string user_id = login.Global.user_id;
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

        JobsList JobList = JsonUtility.FromJson<JobsList>(uwr.downloadHandler.text);
        Debug.Log(JobList[0]);
        foreach (Job jobs in JobList.Job)
        {
            print(jobs.sentence);
        }
    }

    [Serializable]
    public class JobsList
    {
        public List<Job> Job = new List<Job>();
    }
    [Serializable]
    public class Job
    {
        public string sentence;
        public string var;
        public string video;
    }
}


