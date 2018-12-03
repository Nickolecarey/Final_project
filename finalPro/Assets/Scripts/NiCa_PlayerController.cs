using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NiCa_PlayerController : MonoBehaviour {

    public float speed;
    public Text endText;
    public AudioClip lootClip;
    public AudioClip cloudClip;
    public GameObject explosion;
    public GameObject wakeText;

    private Rigidbody2D rb2d;
    private float timer;
    private int count;
    private bool facingRight = true;
    private AudioSource source;
    private bool isDead;

    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        endText.text = "";
        count = 0;
        isDead = false;
        source = GetComponent<AudioSource>();
        Destroy(wakeText, 3);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            Instantiate(explosion, transform.position, transform.rotation);
            count = count + 1;
            source.PlayOneShot(lootClip);
        }

        if (other.gameObject.CompareTag("Cloud"))
        {
            isDead = true;
            source.PlayOneShot(cloudClip);

            endText.text = "Uh-Oh!";
            StartCoroutine(ByeAfterDelay(2));
        }
    }

    void FixedUpdate()
    {
        if (isDead == false)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
            rb2d.velocity = movement * speed;

            if (facingRight == false && moveHorizontal > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveHorizontal < 0)
            {
                Flip();
            }
        }
        timer = timer + Time.deltaTime;
        if (timer >= 10)
        {
            isDead = true;
            endText.text = "Times Up!";
            StartCoroutine(ByeAfterDelay(2));
        }
        if (count >= 5)
        {
            endText.text = "All Stars Collected!";
            StartCoroutine(ByeAfterDelay(2));
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        //GameLoader.gameOn = false;
    }
}
