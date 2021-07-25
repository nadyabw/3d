using System.Collections;
using UnityEngine;

public class VFXDestroyer : MonoBehaviour
{
    [SerializeField] private float destructionDelay = 3f;

    private void Start()
    {
        StartCoroutine(UpdateDestructionDelay());
    }

    private IEnumerator UpdateDestructionDelay()
    {
        yield return new WaitForSeconds(destructionDelay);

        Destroy(gameObject);
    }
}
