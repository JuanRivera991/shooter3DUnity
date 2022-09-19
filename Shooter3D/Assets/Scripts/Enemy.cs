using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anima;
    private bool playerRange;
    private Player player;
    private bool isDeath;
    private UnityEngine.AI.NavMeshAgent nav;
    private bool playerDeath;
    //
    private float timer;
    private float timerAttack = 2f;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anima = GetComponent<Animator>();
        GameObject playerTMP = GameObject.Find("Player");

        if(playerTMP != null)
        {
            player = playerTMP.GetComponent<Player>();
        }
    }

    
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            anima.SetTrigger("death");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            anima.SetTrigger("eat");
        }
        */

        if (isDeath == false)
        {
            nav.SetDestination(player.transform.position);

            timer += Time.deltaTime;

            if (timer >= timerAttack && playerRange && playerDeath == false)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        timer = 0f;
        if (player.lives > 0)
        {
            player.Attack();
        }
        else
        {
            playerDeath = true;
            anima.SetTrigger("eat");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerRange = true;
            anima.SetBool("attack", true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerRange = false;
            anima.SetBool("attack", false);
        }
    }

    public void takeDamage()
    {
        if (isDeath == false)
        {
            audio.Play();
            player.updateZombiesCount();
            anima.SetTrigger("death");
            nav.enabled = false;
            isDeath = true;
        }
    }
}
