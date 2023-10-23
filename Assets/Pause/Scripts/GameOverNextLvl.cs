using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverNextLvl : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadScene("GameOver-NextLvlPanel");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Chamber");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
