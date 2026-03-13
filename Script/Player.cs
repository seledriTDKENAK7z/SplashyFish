using UnityEngine;
using UnityEngine.InputSystem; // WAJIB ADA untuk sistem baru
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    public AudioSource swimSound;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        swimSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        direction = Vector3.zero;
    }

    private void Update()
    {
        // CARA BARU: Membaca sentuhan layar atau klik mouse di Unity 6 (New Input System)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame || 
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame))
        {
            // Cek supaya nggak loncat pas neken tombol UI
            if (!IsPointerOverUI())
            {
                Jump();
            }
        }

        // Space bar versi New Input System
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    // Fungsi pembantu untuk cek UI agar tidak bentrok
    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;
        
        // Untuk Mouse
        if (EventSystem.current.IsPointerOverGameObject()) return true;

        // Untuk Sentuhan HP
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            return EventSystem.current.IsPointerOverGameObject(0);
        }

        return false;
    }

    void Jump()
    {
        direction = Vector3.up * strength;
        if (swimSound != null) swimSound.Play();
    }

    private void AnimateSprite()
    {
        if (sprites.Length == 0) return;
        spriteIndex++;
        if (spriteIndex >= sprites.Length) spriteIndex = 0;
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager gm = Object.FindFirstObjectByType<GameManager>();
        if (other.CompareTag("Obstacle")) gm.GameOver();
        else if (other.CompareTag("Scoring")) gm.IncreaseScore();
    }
}