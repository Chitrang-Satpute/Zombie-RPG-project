using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas canvesGameObject;


    void Start()
    {
        canvesGameObject.enabled = false;   
    }

   public void DeathHandle()
    {
        canvesGameObject.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
