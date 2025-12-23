using Godot;
using System;

/*
 extends Node3D

	signal province_selected

	#Nodes
	@onready var camera: Camera3D = $CameraSocket/Camera3D
	@onready var camera_socket: Node3D = $CameraSocket

	#Camera move
	@export_range(0,1000,1) var camera_move_speed:float = 500.0

	#Camera rotate
	var camera_rotation_direction:float = 0
	@export_range(0,10,0.1) var camera_rotation_speed:float = 0.20
	@export_range(0,20,1) var camera_base_rotation_speed:float = 6
	@export_range(0,10,1) var camera_socket_rotation_x_min:float = -1.60
	@export_range(0,10,1) var camera_socket_rotation_x_max:float = -0.20

	#Camera pan
	@export_range(0,32,4) var camera_automatic_pan_margin:int = 16
	@export_range(0,20,0.5) var camera_automatic_pan_speed:float = 18

	#Camera zoom
	var camera_zoom_direction:float = 0
	@export_range(0,1000,1) var camera_zoom_speed:float = 1000.0
	@export_range(0,100,1) var camera_zoom_min:float = 40.0
	@export_range(0,1000,1) var camera_zoom_max:float = 1000.0
	@export_range(0,2,1) var camera_zoom_speed_damp:float = 0.92

	#flags
	var camera_can_process:bool = true
	var camera_can_move_base:bool = true
	var camera_can_zoom:bool = true
	var camera_can_automatic_pan:bool = false
	var camera_can_rotate_base:bool = true
	var camera_can_rotate_socket_x:bool = true
	var camera_can_rotate_by_mouse_offfset:bool = true

	#Internal flag
	var camera_is_rotating_base:bool = false
	var camera_is_rotating_mouse:bool = false
	var mouse_last_position:Vector2 = Vector2.ZERO



	func _ready() -> void:
		pass
	
	func _process(delta:float) -> void:
		if !camera_can_process: return
		camera_base_move(delta)
		camera_zoom_update(delta)
		camera_automatic_pan(delta)
		camera_base_rotate(delta)
		camera_rotate_to_mouse_offsets(delta)


	#Moves the base of camera
	func camera_base_move(delta:float) -> void:
		if !camera_can_move_base: return
		var velocity_direction: Vector3 = Vector3.ZERO
	
		if Input.is_action_pressed("camera_forward"): velocity_direction -= transform.basis.z
		if Input.is_action_pressed("camera_backward"): velocity_direction += transform.basis.z
		if Input.is_action_pressed("camera_right"): velocity_direction += transform.basis.x
		if Input.is_action_pressed("camera_left"): velocity_direction -= transform.basis.x
	
		position += velocity_direction.normalized() * camera_move_speed  * delta


	func _unhandled_input(event: InputEvent) -> void:
	
			## Exit
		if Input.is_action_pressed("Exit"):
			get_tree().quit()
	
	
		#Camera Zoom
		if event.is_action("camera_zoom_in"):
			camera_zoom_direction = -1
		elif  event.is_action("camera_zoom_out"):
			camera_zoom_direction = 1
	
		#Camera rotations
		if event.is_action_pressed("camera_rotate_right"):
			camera_rotation_direction = -1
			camera_is_rotating_base = true
		elif event.is_action_pressed("camera_rotate_left"):
			camera_rotation_direction = 1
			camera_is_rotating_base = true
		elif event.is_action_released("camera_rotate_left") or event.is_action_released("camera_rotate_right"):
			camera_is_rotating_base = false
		
		if event.is_action_pressed("camera_rotate"):
			mouse_last_position = get_viewport().get_mouse_position()
			camera_is_rotating_mouse = true
		elif event.is_action_released("camera_rotate"):
			camera_is_rotating_mouse = false
	
		if event is InputEventMouseButton and event.button_index == MOUSE_BUTTON_LEFT and event.pressed:
			shoot_ray()


	func camera_zoom_update(delta:float) -> void:
		if !camera_can_zoom:return
	
		var new_zoom:float = clamp(camera.position.z + camera_zoom_speed * camera_zoom_direction * delta, camera_zoom_min, camera_zoom_max)
		camera.position.z = new_zoom
		camera_zoom_direction *= camera_zoom_speed_damp

	# Rotate the camera socket based on mouse offset
	func camera_rotate_to_mouse_offsets(delta:float) -> void:
		if !camera_can_rotate_by_mouse_offfset or !camera_is_rotating_mouse: return
	
		var mouse_offset:Vector2 = get_viewport().get_mouse_position()
		mouse_offset = mouse_offset - mouse_last_position
	
		mouse_last_position = get_viewport().get_mouse_position()
	
		#camera_base_rotate_left_right(delta,mouse_offset.x) #Remove comment to get y rotation on mouse
		camera_socket_rotate_x(delta,mouse_offset.y)
	
	
	#Rotates the camera base
	func camera_base_rotate(delta:float) -> void:
		if !camera_can_rotate_base or !camera_is_rotating_base : return
	
		#To rotate
		camera_base_rotate_left_right(delta, camera_rotation_direction * camera_base_rotation_speed)

	#Rotates the socket of the camera
	func camera_socket_rotate_x(delta:float, dir:float) -> void:
		if !camera_can_rotate_socket_x  : return
	
		var new_rotation_x:float = camera_socket.rotation.x
		new_rotation_x -= dir * delta * camera_rotation_speed
	
		new_rotation_x = clamp(new_rotation_x,camera_socket_rotation_x_min,camera_socket_rotation_x_max)
		camera_socket.rotation.x = new_rotation_x
	
	#Rotates the camera speed left or right
	func camera_base_rotate_left_right(delta:float, dir:float) -> void:
		rotation.y += dir * camera_rotation_speed * delta
	
	# Pans the camera automatically based on screen margins
	func camera_automatic_pan(delta:float) -> void:
		if !camera_can_automatic_pan: return
	
		var viewport_current:Viewport = get_viewport()
		var pan_direction:Vector2 = Vector2(-1,-1) #Starts negative
		var viewport_visible_rectangle:Rect2i = Rect2i(viewport_current.get_visible_rect())
		var viewport_size:Vector2i = viewport_visible_rectangle.size
		var current_mouse_position:Vector2 = viewport_current.get_mouse_position()
		var margin:float = camera_automatic_pan_margin #Shortcut var
	
		var zoom_factor:float = camera.position.z * 0.1
	
		#X pan
		if ((current_mouse_position.x < margin) or (current_mouse_position.x > viewport_size.x - margin)):
			if current_mouse_position.x > viewport_size.x/2.0:
				pan_direction.x = 1
			translate(Vector3(pan_direction.x * delta * camera_automatic_pan_speed * zoom_factor,0,0))
	
		#Y pan
		if ((current_mouse_position.y < margin) or (current_mouse_position.y > viewport_size.y - margin)):
			if current_mouse_position.y > viewport_size.y/2.0:
				pan_direction.y = 1
			translate(Vector3(0, 0, pan_direction.y * delta * camera_automatic_pan_speed * zoom_factor))
		
	func shoot_ray():
		var mouse_pos = get_viewport().get_mouse_position()
		var ray_length = 2000
		var from = camera.project_ray_origin(mouse_pos)
		var to = from + camera.project_ray_normal(mouse_pos) * ray_length
		var space = get_world_3d().direct_space_state
		var ray_query = PhysicsRayQueryParameters3D.new()
		ray_query.from = from
		ray_query.to = to
		var raycast_result = space.intersect_ray(ray_query)
		if !raycast_result.is_empty():
			province_selected.emit(Vector2(raycast_result.position.x,raycast_result.position.z))

 */

public partial class Player : Node3D
{
    [Signal]
    public delegate void ProvinceSelectedEventHandler(Vector2 position);

    // Nodes
    private Camera3D camera;
    private Node3D cameraSocket;

    // Camera move
    [Export(PropertyHint.Range, "0,1000,1")]
    public float CameraMoveSpeed { get; set; } = 500.0f;

    // Camera rotate
    private float cameraRotationDirection = 0;
    [Export(PropertyHint.Range, "0,10,0.1")]
    public float CameraRotationSpeed { get; set; } = 0.20f;
    [Export(PropertyHint.Range, "0,20,1")]
    public float CameraBaseRotationSpeed { get; set; } = 6f;
    [Export(PropertyHint.Range, "0,10,1")]
    public float CameraSocketRotationXMin { get; set; } = -1.60f;
    [Export(PropertyHint.Range, "0,10,1")]
    public float CameraSocketRotationXMax { get; set; } = -0.20f;

    // Camera pan
    [Export(PropertyHint.Range, "0,32,4")]
    public int CameraAutomaticPanMargin { get; set; } = 16;

    [Export(PropertyHint.Range, "0,20,0.5")]
    public float CameraAutomaticPanSpeed { get; set; } = 18f;

    // Camera zoom
    private float cameraZoomDirection = 0;
    [Export(PropertyHint.Range, "0,1000,0.1f")]
    public float CameraZoomSpeed { get; set; } = 1000.0f;

    [Export(PropertyHint.Range, "-50,100,0.1f")]
    public float CameraZoomMin { get; set; } = 0f;

    [Export(PropertyHint.Range, "0,1000,0.1f")]
    public float CameraZoomMax { get; set; } = 1000.0f;

    [Export(PropertyHint.Range, "0,2,0.92f")]
    public float CameraZoomSpeedDamp { get; set; } = 0.92f;

    // Flags
    private bool cameraCanProcess = true;
    private bool cameraCanMoveBase = true;
    private bool cameraCanZoom = true;
    private bool cameraCanAutomaticPan = false;
    private bool cameraCanRotateBase = true;
    private bool cameraCanRotateSocketX = false;
    private bool cameraCanRotateByMouseOffset = true;

    // Internal flags
    private bool cameraIsRotatingBase = false;
    private bool cameraIsRotatingMouse = false;
    private Vector2 mouseLastPosition = Vector2.Zero;

    public override void _Ready()
    {
        camera = GetNode<Camera3D>("CameraSocket/Camera3D");
        cameraSocket = GetNode<Node3D>("CameraSocket");
    }

    public override void _Process(double delta)
    {
        if (!cameraCanProcess) return;
        CameraBaseMove((float)delta);
        CameraZoomUpdate((float)delta);
        CameraAutomaticPan((float)delta);
        CameraBaseRotate((float)delta);
        CameraRotateToMouseOffsets((float)delta);
    }

    private void CameraBaseMove(float delta)
    {
        if (!cameraCanMoveBase) return;
        Vector3 velocityDirection = Vector3.Zero;

        if (Input.IsActionPressed("camera_forward")) velocityDirection -= Transform.Basis.Z;
        if (Input.IsActionPressed("camera_backward")) velocityDirection += Transform.Basis.Z;
        if (Input.IsActionPressed("camera_right")) velocityDirection += Transform.Basis.X;
        if (Input.IsActionPressed("camera_left")) velocityDirection -= Transform.Basis.X;

        Position += velocityDirection.Normalized() * CameraMoveSpeed * delta;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        // Exit
        if (Input.IsActionPressed("Exit"))
        {
            GetTree().Quit();
        }

        // Camera Zoom
        if (@event.IsAction("camera_zoom_in"))
        {
            cameraZoomDirection = -1;
        }
        else if (@event.IsAction("camera_zoom_out"))
        {
            cameraZoomDirection = 1;
        }

        // Camera rotations
        if (@event.IsActionPressed("camera_rotate_right"))
        {
            cameraRotationDirection = -1;
            cameraIsRotatingBase = true;
        }
        else if (@event.IsActionPressed("camera_rotate_left"))
        {
            cameraRotationDirection = 1;
            cameraIsRotatingBase = true;
        }
        else if (@event.IsActionReleased("camera_rotate_left") || @event.IsActionReleased("camera_rotate_right"))
        {
            cameraIsRotatingBase = false;
        }

        if (@event.IsActionPressed("camera_rotate"))
        {
            mouseLastPosition = GetViewport().GetMousePosition();
            cameraIsRotatingMouse = true;
        }
        else if (@event.IsActionReleased("camera_rotate"))
        {
            cameraIsRotatingMouse = false;
        }

        if (@event is InputEventMouseButton mouseButtonEvent &&
            mouseButtonEvent.ButtonIndex == MouseButton.Left &&
            mouseButtonEvent.Pressed)
        {
            ShootRay();
        }
    }

    /// <summary>
    /// We had to alter the Godot parameters for this to work correctly with C#. I'm still not quite sure why, but after some debugging, I learned that the calculation for the newZoom 
    /// wasn't working the same as it would in GDScript - so, what was happening? Well, based on some print statements, I've learned that, even though we were using GD properties to
    /// clamp the minimum value of the camera zoom to 0, the actual camera.Position.Z was going negative - this led to a behavior where the camera zoom was going under 0, then it was being
    /// clamped to 0, and we were stuck unable to zoom in. By removing the clamp call and adding a print statement, I discovered this, and after some fiddling with values to see what the actual 
    /// minimum would be un-clamped, I adjusted the property hints to allow a minZoom of -50, which seems to be the threshold between "seeing the map really close" and "going under the map and losing sight of everything".
    /// </summary>
    /// <param name="delta"></param>
    private void CameraZoomUpdate(float delta)
    {
        if (!cameraCanZoom) return;

        float newZoom = Mathf.Clamp(camera.Position.Z + CameraZoomSpeed * cameraZoomDirection * delta, CameraZoomMin, CameraZoomMax);
        //float newZoom = camera.Position.Z + CameraZoomSpeed * cameraZoomDirection * delta; //Mathf.Clamp(camera.Position.Z + CameraZoomSpeed * cameraZoomDirection * delta, CameraZoomMin, CameraZoomMax);
        //GD.Print(newZoom);

        camera.Position = new Vector3(camera.Position.X, camera.Position.Y, newZoom);
        cameraZoomDirection *= CameraZoomSpeedDamp;
    }

    private void CameraRotateToMouseOffsets(float delta)
    {
        if (!cameraCanRotateByMouseOffset || !cameraIsRotatingMouse) return;

        Vector2 mouseOffset = GetViewport().GetMousePosition() - mouseLastPosition;
        mouseLastPosition = GetViewport().GetMousePosition();

        // Uncomment below for Y rotation on mouse
        // CameraBaseRotateLeftRight(delta, mouseOffset.X);
        CameraSocketRotateX(delta, mouseOffset.Y);
    }

    private void CameraBaseRotate(float delta)
    {
        if (!cameraCanRotateBase || !cameraIsRotatingBase) return;

        CameraBaseRotateLeftRight(delta, cameraRotationDirection * CameraBaseRotationSpeed);
    }

    private void CameraSocketRotateX(float delta, float dir)
    {
        if (!cameraCanRotateSocketX) return;

        float newRotationX = cameraSocket.Rotation.X;
        newRotationX -= dir * delta * CameraRotationSpeed;
        newRotationX = Mathf.Clamp(newRotationX, CameraSocketRotationXMin, CameraSocketRotationXMax);
        cameraSocket.Rotation = new Vector3(newRotationX, cameraSocket.Rotation.Y, cameraSocket.Rotation.Z);
    }

    private void CameraBaseRotateLeftRight(float delta, float dir)
    {
        Rotation = new Vector3(Rotation.X, Rotation.Y + dir * CameraRotationSpeed * delta, Rotation.Z);
    }

    private void CameraAutomaticPan(float delta)
    {
        if (!cameraCanAutomaticPan) return;

        var viewportCurrent = GetViewport();
        var viewportVisibleRectangle = viewportCurrent.GetVisibleRect();
        var viewportSize = viewportVisibleRectangle.Size;
        var currentMousePosition = viewportCurrent.GetMousePosition();
        float margin = CameraAutomaticPanMargin;

        float zoomFactor = camera.Position.Z * 0.1f;

        // X pan
        if ((currentMousePosition.X < margin) || (currentMousePosition.X > viewportSize.X - margin))
        {
            float panX = currentMousePosition.X > viewportSize.X / 2.0f ? 1 : -1;
            Translate(new Vector3(panX * delta * CameraAutomaticPanSpeed * zoomFactor, 0, 0));
        }

        // Y pan
        if ((currentMousePosition.Y < margin) || (currentMousePosition.Y > viewportSize.Y - margin))
        {
            float panY = currentMousePosition.Y > viewportSize.Y / 2.0f ? 1 : -1;
            Translate(new Vector3(0, 0, panY * delta * CameraAutomaticPanSpeed * zoomFactor));
        }
    }

    private void ShootRay()
    {
        var mousePos = GetViewport().GetMousePosition();
        float rayLength = 2000;
        var from = camera.ProjectRayOrigin(mousePos);
        var to = from + camera.ProjectRayNormal(mousePos) * rayLength;
        var space = GetWorld3D().DirectSpaceState;
        var rayQuery = new PhysicsRayQueryParameters3D
        {
            From = from,
            To = to
        };
        var raycastResult = space.IntersectRay(rayQuery);
        if (raycastResult.Count > 0 && raycastResult.ContainsKey("position"))
        {
            var position = (Vector3)raycastResult["position"];
            EmitSignal(SignalName.ProvinceSelected, new Vector2(position.X, position.Z));
        }
    }
}
