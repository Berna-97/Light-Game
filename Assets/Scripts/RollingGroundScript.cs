using System.Collections.Generic;
using UnityEngine;

// Este script cria um chão novo quando o player passa por um trigger presente no chão,
// o que cria um loop infinito
public class RollingGroundScript : MonoBehaviour
{

    public GameObject ground;
    public GameObject gate1;
    public GameObject gate2;
    public GameObject youWin;

    public int totalGroundCount;
    public int groundCount;
  
    private int[] groundBefore;
    private int[] buttonNum;
    private int[] buttonHp;

    private void Start()
    {
        groundCount = 0;

        groundBefore = new int[5];
        buttonNum = new int[5];
        buttonHp = new int[5];

        groundBefore[0] = 3; buttonNum[0] = 2; buttonHp[0] = 3;
        groundBefore[1] = 7; buttonNum[1] = 1; buttonHp[1] = 4;
        groundBefore[2] = 11; buttonNum[2] = 1; buttonHp[2] = 5;
        groundBefore[3] = 1; buttonNum[3] = 1; buttonHp[3] = 3;
        groundBefore[4] = 16; buttonNum[4] = 2; buttonHp[4] = 4;
        groundBefore[5] = 22; buttonNum[5] = 2; buttonHp[5] = 4;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger") && groundCount <= totalGroundCount)
        {
            bool spawned = false;

            for (int y = 0; y < groundBefore.Length; y++)
            {
                if (groundCount + 1 == groundBefore[y])
                {
                    SpawnGate(buttonNum[y], buttonHp[y]);
                    spawned = true;
                    break;
                }
            }

            if (!spawned)
            {
                Instantiate(ground);
            }
        }
        if (other.CompareTag("Trigger") && groundCount == totalGroundCount)
            {
                youWin.SetActive(true);           
            }

        groundCount++; 
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

                break;

            case 2:
                GameObject gateTwo = Instantiate(gate2);

                Transform blueSquare2 = gateTwo.transform.Find("Gate/Blue Square");
                Transform blueSquare3 = gateTwo.transform.Find("Gate/Blue Square2");

                EnemyMoveScript script2 = blueSquare2.GetComponent<EnemyMoveScript>();
                EnemyMoveScript script3 = blueSquare3.GetComponent<EnemyMoveScript>();

                script2.maxHealth = buttonsHp;
                script2.SetHealthToMax();

                script3.maxHealth = buttonsHp;
                script3.SetHealthToMax();

                break;

            default:
                Instantiate(ground);
                break;
        }
    } 
}
