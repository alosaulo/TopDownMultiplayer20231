using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerAtk;
    public int dano;
    public float speedRotation;
    public float speed;
    public int vida;
    Rigidbody rb;
    Animator animator;
    bool morte = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(morte == false) { 
            Movimento();
            Atacar();
        }
    }

    void Movimento() {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(hAxis, 0, vAxis);

        rb.velocity = move.normalized * speed * Time.deltaTime;

        animator.SetFloat("Velocidade",move.sqrMagnitude);

        if (move != Vector3.zero)
        {
            Quaternion rotacao = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards
                (transform.rotation, rotacao, speedRotation * Time.deltaTime);
        }
    }

    void Atacar() {
        if (Input.GetButtonDown("Fire1")) { 
            animator.SetTrigger("atk");
        }
    }

    public void AtivarAtk() { 
        playerAtk.SetActive(true);
    }

    public void DesativarAtk() { 
        playerAtk.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Inimigo") { 
            Inimigo inimgo = collider.GetComponent<Inimigo>();
            inimgo.PerderVida(dano);
        }
    }

    public void PerderVida(int dano)
    {
        if (morte == false)
        {
            vida -= dano;
            animator.SetTrigger("dano");
            if (vida <= 0)
            {
                Morte();
            }
        }
    }

    void Morte()
    {
        animator.SetBool("dano", false);
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        morte = true;
        animator.SetTrigger("die");
    }
}
