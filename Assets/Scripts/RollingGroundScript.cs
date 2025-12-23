using System.Collections.Generic;
using UnityEngine;

// Este script cria um chão novo quando o player passa por um trigger presente no chão,
// o que cria um loop infinito
public class RollingGroundScript : MonoBehaviour
{
    public GroundLevelData levelData;

    public GameObject ground;
    public GameObject gate1;
    public GameObject gate2;
    public GameObject youWin;

    private int groundCount;

    private void Start()
    {
        groundCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Trigger")) return;

        bool spawned = false;

        for (int i = 0; i < levelData.totalGroundCount; i++)
        {
            levelData.GetData(i, out int before, out int buttons, out int hp);

            if (groundCount + 1 == before)
            {
                SpawnGate(buttons, hp);
                spawned = true;
                break;
            }
        }

        if (!spawned && groundCount < levelData.totalGroundCount)
            Instantiate(ground);

        if (groundCount == levelData.totalGroundCount)
            youWin.SetActive(true);

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
                script.enabled = false;

                break;

            case 2:
                GameObject gateTwo = Instantiate(gate2);

                Transform blueSquare2 = gateTwo.transform.Find("Gate/Blue Square");
                Debug.Log(blueSquare2);
                EnemyMoveScript script2 = blueSquare2.GetComponent<EnemyMoveScript>();
                Debug.Log(script2);
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
