using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public Text message;

    //public class userInfo
    //{
    //    public string userID;
    //    public string password;
    //}
    public void loginUser()
    {
        //Debug.Log(username.text);
        //userInfo loginInfo = new userInfo();
        //loginInfo.userID = username.text;
        //loginInfo.password = password.text;

        //string json = JsonUtility.ToJson(loginInfo);
        StartCoroutine(LogInRequest("http://localhost:5000/ar/login/", username.text, password.text));
    }

    IEnumerator LogInRequest(string url, string username, string password)
    {

        //UnityWebRequest uwr = UnityWebRequest.Get(url);
        //yield return uwr.SendWebRequest();
        //if (uwr.isNetworkError || uwr.isHttpError)
        //{
        //    Debug.Log(uwr.error);
        //    yield break;
        //}

        //string SetCookie = uwr.GetResponseHeader("cookie");
        //Debug.Log(SetCookie);
        //Regex rxCookie = new Regex("csrftoken=(?<csrf_token>.{64});");
        //MatchCollection cookieMatches = rxCookie.Matches(SetCookie);
        //string csrfCookie = cookieMatches[0].Groups["csrf_token"].Value;


        //UnityWebRequest uwrpost = UnityWebRequest.Post(url, json);
        //byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        //uwrpost.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        //uwrpost.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        //uwrpost.SetRequestHeader("Content-Type", "application/json");
        //uwrpost.SetRequestHeader("referer", url);
        //uwrpost.SetRequestHeader("cookie", "csrftoken=" + csrfCookie);
        //uwrpost.SetRequestHeader("X-CSRFToken", csrfCookie);

        string loginurl = url + username + "/" + password + "/";
        UnityWebRequest uwr = UnityWebRequest.Get(loginurl);


        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }

        if (uwr.downloadHandler.text == "Login Error")
        {
            message.text = "UserName or Password Wrong";
        } 
        else
        {
            User user = JsonUtility.FromJson<User>(uwr.downloadHandler.text);
            Global.name = user.name;
            Global.user_id = user.user_id;
            Global.position = user.position;
            Global.is_admin = user.is_admin;
            Debug.Log(Global.name);
            SceneManager.LoadScene("MainPage");
        }
    }
    private class User
    {
        public string user_id;
        public string name;
        public string position;
        public bool is_admin;
    }
    [Serializable]
    public static class Global
    {
        public static string user_id;
        public static string name;
        public static string position;
        public static bool is_admin;
    }
}
