using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float health = 20f;

    public void HandleDamage(float damageAmount)
    {
        health -= damageAmount;
        if(health > 0)
        {
            AudioManager.Instance.PlaySFX(SFXType.EnemyHit, transform);
            animator.SetTrigger("IsDamaged");
        }
        else
        {
            AudioManager.Instance.PlaySFX(SFXType.EnemyDeath);
            Destroy(gameObject);
        }
    }
}
