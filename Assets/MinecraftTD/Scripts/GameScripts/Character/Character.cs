using System;
using UnityEngine;
public class Character : MonoBehaviour
{ 
    [SerializeField] public int healthPoint;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private GameObject smokeEffect;

    private bool isNPC = false;
    public event Action onTakeDamage;
    private void Start()
    {
        if (!gameObject.CompareTag("Player"))
        {
            isNPC = true;
        }
    }
    
    public void TakeDamage(int damage)
    { 
        animationController.TakeDamageAnimation();
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            Death();
        }
        if (isNPC)
        {
            onTakeDamage?.Invoke();
        }
    }

    public void Death()
    {
        Debug.Log($"{gameObject.name} погиб!");
        GameObject smoke = Instantiate(smokeEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(smoke, 1.5f);
    }
}
