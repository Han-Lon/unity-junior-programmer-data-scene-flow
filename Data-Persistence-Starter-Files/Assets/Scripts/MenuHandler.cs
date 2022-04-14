using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuHandler : MonoBehaviour
{
    public InputField nameText;


    public void StartGame()
    {
        DataManager.Instance.currentPlayerName = nameText.text;
        
        SceneManager.LoadScene(1);
    }
}
