using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public string PlayerNumber;
    public Image healthBar;

    public GameObject playerAtk;
    public int dano;
    public float speedRotation;
    public float speed;
    public int vida;
    int maxVida;
    Rigidbody rb;
    Animator animator;
    bool morte = false;
    // Start is called before the first frame update
    void Start()
    {
        maxVida = vida;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        VerificaVida();
        if(morte == false) { 
            Movimento();
            Atacar();
        }
    }

    void Movimento() {
        float hAxis = Input.GetAxis("Horizontal"+PlayerNumber);
        float vAxis = Input.GetAxis("Vertical" + PlayerNumber);

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
        if (Input.GetButtonDown("Fire1" + PlayerNumber)) { 
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

    void VerificaVida() {
        healthBar.fillAmount = (float)vida / (float)maxVida;
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
