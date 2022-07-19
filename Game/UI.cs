using Godot;
using System;

public class UI : Control
{
    public override void _Ready()
    {
    }

    void OnPanCamera (Vector2 delta) {
        RectPosition += delta;
    }
}
