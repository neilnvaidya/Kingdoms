using Godot;
using System;

public class CameraManager : Node2D
{

    Vector2 viewportSize;
    Camera2D camera;

    Vector2 mouseLocalPos = Vector2.Zero;
    Vector2 mouseDeltaPos = Vector2.Zero;

    Vector2 maxOffset;
    Vector2 offsetBuffer = Vector2.One * 128;

    const float moveSpeed = 16f;

    const float moveCameraDistance = 64f;

    [Signal] public delegate void PanCamera(Vector2 delta);

    public override void _Ready()
    {
        viewportSize = GetViewport().Size;
        
        camera = GetNode<Camera2D>("Camera2D");
        camera.Position = viewportSize / 2f;

        Vector2 mapEnd = -GetNode<Map>("/root/Game/Map").EndPointPosition;

        maxOffset = -mapEnd - viewportSize + offsetBuffer;

    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        moveCamera();
    }

    void moveCamera()
    {
        bool nearLeftSide = mouseLocalPos.x < moveCameraDistance;
        bool nearRightSide = mouseLocalPos.x > (viewportSize.x - moveCameraDistance);
        bool nearTopSide = mouseLocalPos.y < moveCameraDistance;
        bool nearBottomSide = mouseLocalPos.y > (viewportSize.y - moveCameraDistance);
        Vector2 moveVec = Vector2.Zero;

        if (nearLeftSide) moveVec.x -= moveSpeed;
        if (nearRightSide) moveVec.x += moveSpeed;
        if (nearBottomSide) moveVec.y += moveSpeed;
        if (nearTopSide) moveVec.y -= moveSpeed;

        Vector2 newPos = Position + moveVec;

        if (InBox(newPos, -offsetBuffer, maxOffset))
        {
            Position += moveVec;
            EmitSignal("PanCamera", moveVec);
        }
        

    }
    public void OnLocalMousePositionChanged(Vector2 pos)
    {
        mouseLocalPos = pos;
    }

    bool InBox(Vector2 point, Vector2 topLeftcorner, Vector2 bottomRightCorner) //uses orign as top left, box as bottom right, point as the position that is being compared
    {
        return (point.x <= bottomRightCorner.x &&
                point.x >= topLeftcorner.x &&
                point.y <= bottomRightCorner.y &&
                point.y >= topLeftcorner.y
                );
    }
}
