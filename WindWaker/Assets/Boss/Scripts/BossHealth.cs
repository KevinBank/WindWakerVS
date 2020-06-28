using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private BossBehaviour behaviour;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject ui;

    public void TakeDamage(int damage)
    {
        anim.Play("BossHit");
        health -= damage;
        if (health <= 0)
            Death();
    }

    private void Death()
    {
        behaviour.enabled = false;
        rb.isKinematic = false;
        ui.SetActive(true);
    }
}
