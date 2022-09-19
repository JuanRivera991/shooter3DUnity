using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    public Text txtLives;
    public Text txtZombies;

    private Animator anima;
    private bool walking;
    Vector3 playerToMouse;
    int floorMask;
    float camRayLength = 100f;
    Rigidbody rb;
    Vector3 move;
    float timeLimit = 5f;

    public int lives = 10;
    public int zombiesCount = 0;
    public int zombiesXWin = 10;

    AudioSource audio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anima = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        floorMask = LayerMask.GetMask("floor");
        txtLives.text = "Lives: " + lives.ToString();
        txtZombies.text = "Zombies: " + zombiesCount.ToString();

    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Animation(h, v);
        MyRotation();
        Moving(h, v);

    }

    void Animation(float h, float v)
    {
        walking = h != 0f || v != 0f;
        anima.SetBool("isWalking", walking);
    }

    void Moving(float h, float v)
    {
        move.Set(h, 0f, v);
        move = move.normalized * speed * Time.deltaTime;

        if (rb)
        {
            rb.MovePosition(transform.position + move);
        }
    }

    void MyRotation()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }

    void Update()
    {
        if (lives <= 0)
        {
            if(timeLimit > 1)
            {
                timeLimit -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("Perdiste");
            }
        }

        if (zombiesCount >= zombiesXWin)
        {
            if (timeLimit > 1)
            {
                timeLimit -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("Ganaste");
            }
        }
    }

    public void Attack()
    {
        lives -= 1;
        txtLives.text = "Lives: " + lives.ToString();

        print(lives);
        if (lives <= 0) 
        { 
            anima.SetTrigger("death");
            GetComponent<CapsuleCollider>().isTrigger = true;
            GetComponent<CapsuleCollider>().enabled = false;


        } 
    }

    public void updateZombiesCount()
    {
        zombiesCount += 1;
        txtZombies.text = "Zombies: " + zombiesCount.ToString();
    }

}
