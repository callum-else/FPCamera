# FPCamera
FPCamera is a Unity package for first-person camera movement and rotation. It provides scripts for enhancing camera movement, head tilt, and rotation, as well as character movement control.

## Installation
To install FPCamera, follow these steps:

1. Clone or download this repository to your local machine.
2. Open your Unity project and go to Window > Package Manager.
3. Click on the + button and select Add package from disk....
4. Navigate to the FPCamera folder in the downloaded repository and select the package.json file.
5. Click on the Open button to add the package to your project.

## Usage
### First-person camera movement
The FPMovementController script controls the movement of a character in first-person view. It requires a Rigidbody component and a Transform for the character's vertical rotation. To use it, simply attach it to the character's game object and set the desired movement force and accuracy values in the inspector.

### First-person camera rotation
The FPCameraRotationController script controls the rotation of the first-person camera. It requires two Transform components for the camera's horizontal and vertical rotations. To use it, attach it to the camera's game object and set the desired cursor lock mode, look sensitivity, look accuracy, and maximum look angles in the inspector.

### First-person camera tilt
The CameraTiltMovementEnhancer script enhances the first-person camera movement by adding a head tilt effect. It requires a Transform component for the camera's position, a Transform component for the camera's look-at target, and a Transform component for the camera's Z-axis. To use it, attach it to the camera's game object and set the desired head tilt smoothing and head tilt amount values in the inspector.

### Interfaces and dependencies
The scripts in FPCamera use interfaces to enable easy integration with other scripts and systems. The interfaces used by each script are:

- IFPMovementControllerDependencies: used by FPMovementController to get the required Rigidbody and Transform components.
- IFPCameraRotationControllerDependencies: used by FPCameraRotationController to get the required Transform components for the camera's horizontal and vertical rotations.
- ICameraTileMovementEnhancerDependencies: used by CameraTiltMovementEnhancer to get the required Transform components for the camera's position, look-at target, and Z-axis.

### Input handling
All scripts in FPCamera use the SetInput method to handle input. This method takes a Vector2 parameter representing the input velocity or direction. It should be called by a separate input handling script or system to provide the input values to the camera and movement scripts.
