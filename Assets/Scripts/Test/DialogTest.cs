using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogSystem))]
public class DialogTest : MonoBehaviour
{
    public DialogSystem dialogSystem;
    
    void Start()
    {
        dialogSystem.UpdateDialog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
