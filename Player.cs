using Godot;
using System;

public class Player : Ruler
{
    //Private
    Vector2 mousePosition;
    Vector2 delta_MousePosition;
    Viewport viewport;
    Tile hoveredTile;
    string hoveredTileDisplay;
    Tile lSelectedTile;
    string lSelectedTileDisplay;
    Tile rSelectedTile;
    string rSelectedTileDisplay;

    CameraManager cameraManager;


    //Debug Aids
    Sprite hoverSprite;
    Sprite lSelectedSprite;
    Sprite rSelectedSprite;

    Vector2 lSelectedSpriteRestPos = new Vector2(160, -64);
    Vector2 rSelectedSpriteRestPos = new Vector2(240, -64);

    //Public
    [Export] public Vector2 MousePosition { get => mousePosition; set => mousePosition = value; }
    [Export] public Vector2 Delta_MousePosition { get => delta_MousePosition; set => delta_MousePosition = value; }
    [Export] public Viewport Viewport { get => viewport; set => viewport = value; }
    [Export] public string HoveredTileDisplay { get => hoveredTileDisplay; set => hoveredTileDisplay = value; }


    //Delegates
    [Signal] public delegate void LocalMousePositionChanged(Vector2 pos);
    [Signal] public delegate void GlobalMousePositionChanged(Vector2 pos);


    public override void _Ready()
    {
        viewport = GetViewport();
        cameraManager = GetNode<CameraManager>("CameraManager");
        hoverSprite = GetNode<Sprite>("HoverSprite");

        lSelectedSprite = GetNode<Sprite>("LRSelectedSprite");
        rSelectedSprite = GetNode<Sprite>("RSelectedSprite");

    }

    //Input getter
    public override void _Input(InputEvent inputEvent)
    {
        base._Input(inputEvent);

        if (Input.IsActionJustPressed("left_click"))
        {
            GD.Print("left " + hoveredTile.AsString());
            lSelectedTile = hoveredTile;
            lSelectedTileDisplay = lSelectedTile.AsString();
            lSelectedSprite.Position = lSelectedTile.Position;
        }
        if (Input.IsActionJustPressed("right_click"))
        {
            GD.Print("right " + hoveredTile.AsString());
            rSelectedTile = hoveredTile;
            rSelectedTileDisplay = rSelectedTile.AsString();
            rSelectedSprite.Position = rSelectedTile.Position;
        }
    }

    //Called as fast as possible.
    public override void _Process(float delta)
    {
        base._Process(delta);
        UpdateMousePosition();
    }

    //Update the mouse position and emit MousePositionChanged signal
    void UpdateMousePosition()
    {
        Vector2 p = viewport.GetMousePosition();
        if (p != MousePosition)
        {
            Delta_MousePosition = p - MousePosition;
            MousePosition = p;
            EmitSignal("LocalMousePositionChanged", MousePosition);
            EmitSignal("GlobalMousePositionChanged", GetGlobalMousePosition());
        }
    }

    Vector2 GetGlobalMousePosition()
    {
        return MousePosition + cameraManager.Position;
    }

    public void HandleHoveredTileChangedEvent(Tile t)
    {
        hoverSprite.Position = t.Position;
        hoveredTile = t;
    }
}

