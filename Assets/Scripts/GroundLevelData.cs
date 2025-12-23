using UnityEngine;

[CreateAssetMenu(menuName = "Level/Ground Level Data")]
public class GroundLevelData : ScriptableObject
{
    public int totalGroundCount;

    //chão até ao portão, se tem 1 ou 2 botões, número de lados do botão
    public int g0_before, g0_buttons, g0_hp;
    public int g1_before, g1_buttons, g1_hp;
    public int g2_before, g2_buttons, g2_hp;
    public int g3_before, g3_buttons, g3_hp;
    public int g4_before, g4_buttons, g4_hp;
    public int g5_before, g5_buttons, g5_hp;
    public int g6_before, g6_buttons, g6_hp;

    public void GetData(int index, out int before, out int buttons, out int hp)
    {
        before = buttons = hp = 0;

        switch (index)
        {
            case 0: before = g0_before; buttons = g0_buttons; hp = g0_hp; break;
            case 1: before = g1_before; buttons = g1_buttons; hp = g1_hp; break;
            case 2: before = g2_before; buttons = g2_buttons; hp = g2_hp; break;
            case 3: before = g3_before; buttons = g3_buttons; hp = g3_hp; break;
            case 4: before = g4_before; buttons = g4_buttons; hp = g4_hp; break;
            case 5: before = g5_before; buttons = g5_buttons; hp = g5_hp; break;
            case 6: before = g6_before; buttons = g6_buttons; hp = g6_hp; break;
        }
    }
}
