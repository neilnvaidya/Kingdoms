using Godot;
using System;


//Game handles communicaiton between top level nodes. instanced objects should connect their own signals/events.
public class Game : Node
{   RulersManager rulersManager;
    CollectivesManager collectivesManager;
    Player player;
    Enemy enemy;
    Map map;
    UI ui;
    CameraManager cameraManager;
    public override void _Ready()
    {
        rulersManager = GetNode<RulersManager>("RulersManager");
        collectivesManager = GetNode<CollectivesManager>("CollectivesManager");
        player = GetNode<Player>("RulersManager/Player");
        enemy = GetNode<Enemy>("RulersManager/Enemy");
        map = GetNode<Map>("Map");
        ui = GetNode<UI>("UI");
        cameraManager = GetNode<CameraManager>("Player/CameraManager");
        ConnectSignals();
    }

    void ConnectSignals()
    {
        player.Connect("LocalMousePositionChanged", cameraManager, "OnLocalMousePositionChanged");
        player.Connect("GlobalMousePositionChanged", map, "OnGlobalMousePositionChanged");
        cameraManager.Connect("PanCamera", ui, "OnPanCamera");
        // map.Connect("HoveredTileChanged", player, "OnHoveredTileChanged");
        map.HoverTileChangedEvent += player.HandleHoveredTileChangedEvent;
    }
}
