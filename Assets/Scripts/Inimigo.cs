using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour
{
    public GameObject inimigoAtk;
    public Transform playerTarget;

    public int vida;
    
    Animator animator;
    NavMeshAgent agent;

    PlayerController[] players;

    bool morte = false;
    // Start is called before the first frame update
    void Start()
    {
        players = GameManager.instance.Players.ToArray();

        playerTarget = players[0].transform;

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
        GetPlayerProx();

        float distancia = Vector3.Distance(transform.position, playerTarget.position);
        
        if (distancia > 2)
        {
            agent.SetDestination(playerTarget.position);
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
        animator.SetTrigger("die");
        animator.SetBool("dano", false);
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        morte = true;
        agent.isStopped = true;
        DesativarAtk();
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

    void GetPlayerProx() {
        float distancia = float.MaxValue;
        for (int i = 0; i < players.Length; i++)
        {
            float distanciaPlayer = Vector3.Distance(transform.position, 
                players[i].transform.position);
            
            if (distanciaPlayer < distancia && !players[i].estahMorto()) 
            { 
                distancia = distanciaPlayer;
                playerTarget = players[i].transform;
            }
        }
    }

}
