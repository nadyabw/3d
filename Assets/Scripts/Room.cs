using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    [SerializeField] private UnityEvent onToggleOn;
    [SerializeField] private UnityEvent onToggleOff;
    [SerializeField] private bool isToggleOn;
    [SerializeField] private Transform doorTransform;

    private bool isPlayerInRoom;

    private void Awake()
    {
        SetToggle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRoom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRoom = false;
        }
    }

    private void Update()
    {
        if (!isPlayerInRoom) return; 
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AudioManager.Instance.PlaySFX(SFXType.Door, doorTransform);
            ChangeToggle();
        }
    }

    private void ChangeToggle()
    {
        isToggleOn = !isToggleOn;

        SetToggle();
    }

    private void SetToggle()
    {
        if (isToggleOn)
        {
            onToggleOn?.Invoke();
        }
        else
        {
            onToggleOff?.Invoke();
        }
    }
}
