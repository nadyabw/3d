using UnityEngine;

namespace Snowman
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Vector3 startPosition;

        private Rigidbody rb;
    
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Reset();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Gate"))
            {
                Debug.LogError($"Hit gate");
                Reset();
            }
            else if (other.gameObject.CompareTag("ResetGround"))
            {
                Debug.LogError($"Hit ResetGround");
                Reset();
            }
        }

        private void Reset()
        {
            transform.position = startPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
