using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorDetect : MonoBehaviour
{
    public GameObject pnlMockup;
    // Start is called before the first frame update
    void Start()
    {
        if (!Application.isEditor)
        {
            pnlMockup.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
