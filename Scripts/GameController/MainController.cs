using UnityEngine;

namespace Ui
{
    public class MainController : MonoBehaviour
    {
        public void OnClickStart()
        {
            // Load the game scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelect");
        }
        public void OnClickExit()
        {
            // Quit the game
            Application.Quit();
        }
    }
}