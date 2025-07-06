using Godot;

namespace Imagezoom;

public partial class ZoomableTextureRect : TextureRect
{
	private static StringName ZoomInAction = "zoom in";
	private static StringName ZoomOutAction = "zoom out";

	[Export] private float _zoomSpeed = 0.1f;
	
	[Export] private float _maxZoom = 100f;
	[Export] private float _minZoom = 1f;

	private float Zoom
	{
		get => Scale.X;
		set => Scale = Vector2.One * value;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		bool zoomedIn = Input.IsActionPressed(ZoomInAction);
		bool zoomedOut = Input.IsActionPressed(ZoomOutAction);
		bool zoomed = zoomedIn || zoomedOut;

		if (zoomed)
			DoZoom(zoomedIn ? 1 : -1);
	}
	
	private void DoZoom(float d)
	{
		Vector2 mousePosOnBefore = GetLocalMousePosition();
		
		float dZoom = d * _zoomSpeed;
		
		Zoom *= 1 + dZoom;
		Zoom = float.Clamp(Zoom, _minZoom, _maxZoom);

		Vector2 mousePosAfter = GetLocalMousePosition();
		
		Vector2 diff = mousePosAfter - mousePosOnBefore;
		Position += diff * Zoom;
	}
}
