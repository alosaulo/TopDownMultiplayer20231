using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public int vida;
    Animator animator;
    bool morte = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        animator.SetBool("dano", false);
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        morte = true;
        animator.SetTrigger("die");
    }

}
