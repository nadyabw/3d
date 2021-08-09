using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private float restartDelay = 3f;

    private int coinsToActivatePortal;

    private static LevelManager instance;

    public static LevelManager Instance { get => instance;}

    private void Awake()
    {
        instance = this;
    }


    private IEnumerator UpdateRestart()
    {
        yield return new WaitForSeconds(restartDelay);

        Restart();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HandleCoinCreated()
    {
        coinsToActivatePortal++;
    }

    public void HandleCoinCollected()
    {
        coinsToActivatePortal--;

        if(coinsToActivatePortal == 0)
        {
            portal.SetActive(true);
        }
    }

    public void HandlePortalEntered()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void HandlePlayerDeath()
    {
        StartCoroutine(UpdateRestart());
    }
}
