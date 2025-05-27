using UnityEngine;

namespace MinecraftTD.Scripts.GameScripts.AI
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 10;
        private int currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log($"{gameObject.name} получил {damage} урона! Оставшееся здоровье: {currentHealth}");

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log($"{gameObject.name} погиб!");
            Destroy(gameObject);
        } 
    }
}