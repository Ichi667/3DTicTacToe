using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe3D
{
    public class MenuController : MonoBehaviour
    {
        [Tooltip("Ім'я сцени з грою (без .unity)")]
        public string gameSceneName = "game";

        public void StartVsAI()
        {
            GameSettings.Mode = GameMode.SinglePlayer;
            SceneManager.LoadScene(gameSceneName);
        }

        public void Start2Players()
        {
            GameSettings.Mode = GameMode.TwoPlayers;
            SceneManager.LoadScene(gameSceneName);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
