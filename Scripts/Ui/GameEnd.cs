using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ui
{
    public class GameEnd : MonoBehaviour
    {
        TextMeshProUGUI text;
        private Button Button;

        private void Start()
        {
            text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
            
        }
        
        private void DoGameEnd(bool isWin)
        {
            if (isWin)
            {
                text.text = "You Win!";
            }
            else
            {
                text.text = "You Lose!";
            }
            
        }
        private void GoStageSelect()
        {
            SceneManager.LoadScene("StageSelect");
        }
    }
}