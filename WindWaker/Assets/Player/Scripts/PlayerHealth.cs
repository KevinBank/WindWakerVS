using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [Header("FullHeartIcons")]
    [SerializeField] private Texture fullHeartIcon;
    [SerializeField] private Vector2 fullHeartSize;
    [SerializeField] private Vector2 fullHeartPosition;
    [Tooltip("The space between the icons")]
    [SerializeField] private float fullHeartSpacing;
    [Space(10f)]
    [SerializeField] private ScaleMode fullHeartScaleMode;
    [SerializeField] private bool fullHeartAlphaBlend;
    [SerializeField] private float fullHeartImageAspect;

    [Header("EmptyHeartIcons")]
    [SerializeField] private Texture emptyHeartIcon;
    [SerializeField] private Vector2 emptyHeartSize;
    [SerializeField] private Vector2 emptyHeartPosition;
    [Tooltip("The space between the icons")]
    [SerializeField] private float emptyHeartSpacing;
    [Space(10f)]
    [SerializeField] private ScaleMode emptyHeartScaleMode;
    [SerializeField] private bool emptyHeartAlphaBlend;
    [SerializeField] private float emptyHeartImageAspect;

    [Header("Anim")]
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject ui;
    [SerializeField] private ZeldaController controller;

    private void OnGUI()
    {
        if (!fullHeartIcon || !emptyHeartIcon)
        {
            Debug.LogError("Assign a Texture in the inspector.");
            return;
        }

        Vector2 fullHeartPosition = this.fullHeartPosition;
        Vector2 emptyHeartPosition = this.emptyHeartPosition;
        
        //Draws the empry hearts
        for (int i = 0; i < maxHealth; i++)
        {
            if(i >= currentHealth)
            {
                GUI.DrawTexture(new Rect(emptyHeartPosition.x, emptyHeartPosition.y, emptyHeartSize.x, emptyHeartSize.y), emptyHeartIcon, emptyHeartScaleMode, emptyHeartAlphaBlend, emptyHeartImageAspect);
            }
            emptyHeartPosition = new Vector2(emptyHeartPosition.x + emptyHeartSpacing, emptyHeartPosition.y);
        }
        //draws the normal hearts
        for (int i = 0; i < currentHealth; i++)
        {
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            GUI.DrawTexture(new Rect(fullHeartPosition.x, fullHeartPosition.y, fullHeartSize.x, fullHeartSize.y), fullHeartIcon, fullHeartScaleMode, fullHeartAlphaBlend, fullHeartImageAspect);
            fullHeartPosition = new Vector2(fullHeartPosition.x + fullHeartSpacing, fullHeartPosition.y);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            TakeDamage(maxHealth);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Dealth();
    }

    private void Dealth()
    {
        currentHealth = 0;
        anim.Play("Death");
        controller.enabled = false;
        ui.SetActive(true);
    }

    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}