using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour
{
    public GameObject inimigoAtk;
    public Transform player;

    public int vida;
    
    Animator animator;
    NavMeshAgent agent;

    bool morte = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player.transform;

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(morte == false) { 
            IA();
        }
    }

    void IA() 
    {
        float distancia = Vector3.Distance(transform.position, player.position);
        
        if (distancia > 2)
        {
            agent.SetDestination(player.position);
            animator.SetBool("run", true);
            animator.SetBool("atk", false);
        }
        else if (distancia <= 2) 
        {
            animator.SetBool("run", false);
            animator.SetTrigger("atk");
        }

    }

    public void PerderVida(int dano) {
        if(morte == false) 
        { 
            vida -= dano;
            animator.SetTrigger("dano");
            if (vida <= 0) {
                Morte();
            }
        }
    }

    void Morte() {
        DesativarAtk();
        animator.SetBool("dano", false);
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        morte = true;
        animator.SetTrigger("die");
        agent.velocity = Vector3.zero;
    }

    public void AtivarAtk()
    {
        inimigoAtk.SetActive(true);
    }

    public void DesativarAtk()
    {
        inimigoAtk.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PlayerController playerController = collider.GetComponent<PlayerController>();
            playerController.PerderVida(1);
        }
    }

}
