using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public void OnTapPlayBtn()
    {
        SceneManager.LoadScene(2);
    }
}
