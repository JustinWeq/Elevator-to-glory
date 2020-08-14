using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager
{
    private static GlobalManager manager = null;
    private Camera camera;
    private player_controller_script player_controller;
    private game_manager _game_manager;
    private Canvas canvas;

    private GlobalManager()
    {
        player_controller = GameObject.Find("PlayerController").GetComponent<player_controller_script>();
        camera = GameObject.FindObjectOfType<Camera>();
        //_game_manager = GameObject.Find("GameManager").GetComponent<game_manager>();
        canvas = GameObject.FindObjectOfType<Canvas>();

    }

    public static GlobalManager GetGlobalManager()
    {
        if (manager == null)
            manager = new GlobalManager();
        return manager;
    }

    public Camera GetCamera()
    {
        return camera;
    }

    public player_controller_script GetPlayerController()
    {
        return player_controller;
    }

    public game_manager GetGameManager()
    {
        return _game_manager;
    }

    public Canvas GetCanvas()
    {
        return canvas;
    }
}
