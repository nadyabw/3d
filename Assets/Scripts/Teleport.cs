using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Teleport otherEndTeleport;
    [SerializeField] private GameObject teleportationVFX;

    private bool isPlayerJustTeleported;

    private void OnTriggerEnter(Collider other)
    {
        if (Player.Instance.IsSameObject(other.gameObject) && !isPlayerJustTeleported)
        {
            AudioManager.Instance.PlaySFX(SFXType.Teleport);
            TeleportPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Player.Instance.IsSameObject(other.gameObject) && isPlayerJustTeleported)
        {
            isPlayerJustTeleported = false;
        }
    }

    private void TeleportPlayer()
    {
        Instantiate(teleportationVFX, transform.position, Quaternion.identity);

        otherEndTeleport.HandlePlayerTeleportation();

        Player player = Player.Instance;
        Vector3 pos = player.transform.position;
        pos.x = otherEndTeleport.transform.position.x;
        pos.z = otherEndTeleport.transform.position.z;
        player.TeleportTo(pos);
    }

    public void HandlePlayerTeleportation()
    {
        isPlayerJustTeleported = true;
    }    
}
