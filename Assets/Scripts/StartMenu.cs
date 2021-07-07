using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private int _indexGameMap;

    public void ContinuePlaying()
    {
        SceneManager.LoadScene(_indexGameMap);
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        
        SceneManager.LoadScene(_indexGameMap);
    }
}
