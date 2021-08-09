using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

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
        if (Game.Instance.IsPaused)
            return;
            
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // for test purpose
/*        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayDeathVFX();
        }*/
    }

    private void Shoot()
    {
        AudioManager.Instance.PlaySFX(SFXType.Shoot, transform);

        Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
    }

    public void HandleDeath()
    {
        AudioManager.Instance.PlaySFX(SFXType.PlayerDeath, transform);
        PlayDeathVFX();
        DisableMovement();

        LevelManager.Instance.HandlePlayerDeath();
    }

    public void TeleportTo(Vector3 pos)
    {
        playerMovement.TeleportTo(pos);
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
