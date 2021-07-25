using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject deathVFX;

    private PlayerMovement playerMovement;

    private static Player instance;

    public static Player Instance { get => instance;}

    private void Awake()
    {
        instance = this;

        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // for test purpose
        if(Input.GetKeyDown(KeyCode.X))
        {
            PlayDeathVFX();
        }
    }

    private IEnumerator UpdateRestart()
    {
        yield return new WaitForSeconds(3f);

        Restart();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HandleDeath()
    {
        PlayDeathVFX();

        DisableMovement();

        StartCoroutine(UpdateRestart());
    }

    private void PlayDeathVFX()
    {
        GameObject vfx1 = Instantiate(deathVFX, transform.position, transform.rotation, transform);
        GameObject vfx2 = Instantiate(deathVFX, transform.position, transform.rotation, transform);
        GameObject vfx3 = Instantiate(deathVFX, transform.position, transform.rotation, transform);

        vfx1.transform.Rotate(new Vector3(0f, 0f, 20f));
        vfx2.transform.Rotate(new Vector3(0f, 180f, 45f));
        vfx3.transform.Rotate(new Vector3(0f, 90f, 60f));
    }

    public void DisableMovement()
    {
        playerMovement.enabled = false;
    }

    public bool IsSameObject(GameObject gObj)
    {
        return gameObject == gObj;
    }
}
