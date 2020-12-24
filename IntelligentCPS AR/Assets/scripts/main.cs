using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class main : MonoBehaviour
{
    public Text title;
    // Start is called before the first frame update
    void Start()
    {
        title.text = login.Global.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
