using Assets.Scripts.utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_manager : MonoBehaviour
{
    private GameObject[] players = new GameObject[4];
    private List<unit_control_script> player_units;
    private bool[] is_bot;
    private int register_player_count;
    // Start is called before the first frame update
    void Awake()
    {
        register_player_count = 0;
        //set up the registered player count

        //set up the is bot array
        is_bot = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            is_bot[i] = false;
        }

        //add self to the global manager
        GlobalManager.GetGlobalManager().SetGameManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterPlayer(GameObject player)
    {
        players[register_player_count++] = player;
        is_bot[register_player_count - 1] = player.GetComponent<unit_control_script>() != null;
    }

    public GameObject GetPlayer(int id)
    {
        return players[id];
    }

    public static game_manager GetGameManager()
    {
        return GlobalManager.GetGlobalManager().GetGameManager();
    }


}
