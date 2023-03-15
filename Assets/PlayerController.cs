using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerAtk;
    public int dano;
    public float speedRotation;
    public float speed;
    Rigidbody rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
        Atacar();
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
            animator.SetTrigger("Atk");
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

}
