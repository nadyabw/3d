using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (Player.Instance.IsSameObject(other.gameObject))
        {
            LevelManager.Instance.HandlePortalEntered();
        }
    }
}
