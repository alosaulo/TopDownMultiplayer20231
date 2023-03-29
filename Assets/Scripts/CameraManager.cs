using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Pega o targetgroup, que se move ao meio de todos os objs q estão no targetgroup
    public CinemachineTargetGroup targetGroup;
    
    //estrutura para ativar e desativar a camera virtual, todo personagem tem uma
    public List<CameraData> Cameras;

    //Vetor que armazena os players
    PlayerController[] players;

    //verifica se é splitscreen ou não
    bool splitScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        //pega os jogadores no game manager
        players = GameManager.instance.Players.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        //Variável para calcular a média das distancia dos jogadores
        float distance = 0;
        
        //Soma a distância de todos os jogadores
        for (int i = 0; i < players.Length; i++)
        {
            distance += Vector3.Distance(players[i].transform.position, 
                targetGroup.transform.position);

        }

        //Calcula a média da distância, se a média dos jogadores for maior ou igual que 10 ele divide a tela
        if (distance / players.Length >= 10 && splitScreen == false)
        {
            splitScreen = true;
        }
        //se a média for menor que 10 ele divide a tela
        else if (distance / players.Length < 10 && splitScreen == true) 
        { 
            splitScreen= false;
        }


        //Cameras[0] é a camera que ve todos os personagens
        //Cameras[1] é a camera do P1
        //Cameras[2] é a camera do P2
        if (splitScreen)
        {
            /*Se for splitscreen desativa a camera que ve todos os personagens a ativa a camera de cada personagem*/
            Cameras[0].DesativarCamera();
            Cameras[1].AtivarCamera();
            Cameras[2].AtivarCamera();
        }
        else 
        {
            /*Se NÃO for splitscreen ativa a camera que ve todos os personagens a desativa a camera de cada personagem*/
            Cameras[0].AtivarCamera();
            Cameras[1].DesativarCamera();
            Cameras[2].DesativarCamera();
        }
    }
}
