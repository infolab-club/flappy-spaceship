using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMain : MonoBehaviour
{
    public void NewGame()
    {
        GameData.Score = 0;
        GameData.MaxScore = 0;
        SceneManager.LoadScene("Game");
    }
}
