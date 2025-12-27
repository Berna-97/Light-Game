using System.Collections.Generic;
using UnityEngine;

// Este script cria um chão novo quando o player passa por um trigger presente no chão,
// o que cria um loop infinito
public class RollingGroundInfiniteVersion : MonoBehaviour
{

    public GameObject ground;
    public GameObject gate1;
    public GameObject gate2;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Trigger")) return;


        int chance = Random.Range(1, 5);
        int buttons = Random.Range(1, 3);
        int hp = Random.Range(1, 4);


        if (chance == 1)
        {
            SpawnGate(buttons, hp);


        }
        else
        {
            Instantiate(ground);
        }


    }




    private void SpawnGate(int buttonsNum, int buttonsHp)
    {

        switch (buttonsNum)
        {

            case 0:
                Instantiate(ground);
                break;
            case 1:
                GameObject gateOne = Instantiate(gate1);

                Transform blueSquare = gateOne.transform.Find("Gate/Blue Square");

                EnemyMoveScript script = blueSquare.GetComponent<EnemyMoveScript>();

                script.maxHealth = buttonsHp;
                script.SetHealthToMax();
                script.enabled = false;

                break;

            case 2:
                GameObject gateTwo = Instantiate(gate2);

                Transform blueSquare2 = gateTwo.transform.Find("Gate/Blue Square");
                EnemyMoveScript script2 = blueSquare2.GetComponent<EnemyMoveScript>();
                script2.maxHealth = buttonsHp;
                script2.SetHealthToMax();

                Transform blueSquare3 = gateTwo.transform.Find("Gate/Blue Square2");
                EnemyMoveScript script3 = blueSquare3.GetComponent<EnemyMoveScript>();
                script3.maxHealth = buttonsHp;
                script3.SetHealthToMax();



                break;

            default:
                Instantiate(ground);
                break;
        }
    }
}
