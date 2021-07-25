using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject portal;

    private int coinsToActivatePortal;

    private static LevelManager instance;

    public static LevelManager Instance { get => instance;}

    private void Awake()
    {
        instance = this;
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
}
