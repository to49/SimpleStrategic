using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{ 
    public int healthPoint;
    
    private int _baseHealthPoint;
    
    public AnimationController animationController;

    public GameObject smokeEffect;
    
    
    private void Start()
    {
        _baseHealthPoint = healthPoint;
    }
    public void TakeDamage(int damage)
    {
        animationController.TakeDamageAnimation();
        healthPoint -= damage;
        
        Debug.Log($"{gameObject.name} получил {damage} урона! Оставшееся здоровье: {healthPoint}");
        if (healthPoint <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log($"{gameObject.name} погиб!");
        GameObject smoke = Instantiate(smokeEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(smoke, 200f);
    }
}
