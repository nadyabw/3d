using UnityEngine;

namespace Snowman
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float jumpForce = 10f;

        [Header("Hit Settings")]
        [SerializeField] private Transform hitObject;
        [SerializeField] private float hitRadius = 1f;
        [SerializeField] private LayerMask hitLayerMask;
        [SerializeField] private float maxHitForce = 10f;
        [SerializeField] private float hitHeight = 3f;

        private Rigidbody rb;
        [SerializeField]
        private float hitForce;
        private bool isHitGrowing;
    
        public float MaxHitForce => maxHitForce;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitObject.position, hitRadius);
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            HitUpdate();
        }

        void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            rb.AddForce(transform.forward * (vertical * speed));

            // rb.velocity = transform.forward * (vertical * speed);
            // transform.position += transform.forward * (vertical * speed * Time.deltaTime);
            transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.fixedDeltaTime);

            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            }
        }

        public float GetHitForce()
        {
            return hitForce;
        }

        private void HitUpdate()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                hitForce = 0;
                isHitGrowing = true;
            }

            if (Input.GetButton("Fire1"))
            {
                if (isHitGrowing)
                {
                    hitForce += maxHitForce * Time.deltaTime;

                    if (hitForce >= maxHitForce)
                    {
                        isHitGrowing = false;
                    }
                }
                else
                {
                    hitForce -= maxHitForce * Time.deltaTime;

                    if (hitForce <= 0)
                    {
                        isHitGrowing = true;
                    }
                }
            }
        
            if (Input.GetButtonUp("Fire1"))
            {
                HitPushables();
                hitForce = 0;
            }
        }

        private void HitPushables()
        {
            var colliders = Physics.OverlapSphere(hitObject.position, hitRadius, hitLayerMask);

            foreach (var coll in colliders)
            {
                var pushRb = coll.GetComponent<Rigidbody>();
                var direction = transform.forward;
                direction.y = hitHeight;
                pushRb.AddForce(direction.normalized * hitForce, ForceMode.Impulse);
            }
        }
    }
}
