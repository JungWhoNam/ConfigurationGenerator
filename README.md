# Display Configuration File Generator
> This Unity project is part of a larger project called [Immersive OSPray](https://github.com/jungwhonam-tacc/Whitepaper).

## Overview
This Unity project provides features to form displays in a spatial layout and save the configuration in a JSON file. The JSON file is used in [another application](https://github.com/JungWhoNam/ospray_studio/tree/jungwho.nam-feature-immersive-latest) to configure its virtual cameras and windows.
- See `Assets/Scenes/SampleScene` for example display configurations
- See `Assets/config` for example JSON files

This project facilitates the creation of the JSON file by offering a graphical assistant for specifying and arranging displays. For instance, users can utilize 3D object manipulation features in the Unity Editor to specify the positions of display corners and arrange display objects. Additionally, the project provides features for placing these display objects into a spatial layout and scaling a master display to fit all other displays.

## Specifying `Display`

<div id="image-table">
    <table>
	    <tr>
    	    <td style="padding:4px">
        	    <img src="Images/Config%20Generator%20-%20display0.png" width="400"/>
      	    </td>
            <td style="padding:14px">
            	<img src="Images/Config%20Generator%20-%20display.png" width="400"/>
            </td>
        </tr>
    </table>
</div>

`Display` object contains values that will be saved in the JSON object in the configuration file. A user utilizes the Unity Editor to specify the positions of the three display corners and the eye by referencing Unity objects in a 3D view. Typically, the corners and eye objects are placed under the Display object to group these objects as one entity.

In our current implementation, we assume one camera view per window and support specifying which display to show the window. Furthermore, as our camera projection plane replicates a physical display, we provide an option to input mullion values to account for display frames.


## Arranging `Display` objects

<div id="image-table">
    <table>
	    <tr>
    	    <td style="padding:4px">
        	    <img src="Images/Config%20Generator%20-%203D.png" width="800"/>
      	    </td>
            <td style="padding:4px">
            	<img src="Images/Config%20Generator%20-%20set%20up.png" width="290"/>
            </td>
        </tr>
    </table>
</div>

A user arranges these `Display` objects to form tiled-display walls. `Config` script provides features for arranging these `Display` objects into a single entity and saving them to a JSON file. Additionally, this script provides features for placing these display objects into a spatial layout and scaling a master display to fit all other displays. 
- `Master Display`: This `Display` object represents the master display.
- `Display Containers`: Each GameObject in the list contains GameObject(s) with attached `Display` components. Essentially, each GameObject in the list groups `Display` objects, enabling users to move these objects as a single unit.
- `Move the eyes of Display(s)`: This button relocates the eyes of all `Display` objects to a specific position.
- `Position containers around the Y-Axis`: The system shown in the figure comprises six wall displays forming a hemisphere. This feature repositions the groups defined in `Display Containers` to shape a hemisphere. `From Dir` and `To Dir` set the hemisphere's starting and ending points.
- `Reset the master display`: Since the master node manages user inputs, the view displayed in the node should encompass all other views. This button scales the master display to fit all other displays, adjusting the screen height value accordingly. The checkbox sets the z-value of the display to either the minimum or maximum z-value of all other displays.
- `Save the configuration to a JSON file`: This button saves the configuration to a JSON file, with the file location specified in `File name`. It might be necessary to negate output values because a rendering application could use a different coordinate system.


## Display Configuration JSON File

> See `Assets/config` for example JSON files.

This is a snippet of an example JSON file. 

```
[
    ...

    {
		"hostName": "localhost",
	
		"topLeft": [-0.178950, 0.122950, 1.000000],
		"botLeft": [-0.178950, -0.122950, 1.000000],
		"botRight": [0.178950, -0.122950, 1.000000],
		"eye": [0.000000, 0.000000, 2.000000],
		"mullionLeft": 0.006320,
		"mullionRight": 0.006320,
		"mullionTop": 0.015056,
		"mullionBottom": 0.015056,
	
		"display": 0,
		"screenX": 0,
		"screenY": 0,
		"screenWidth": 1024,
		"screenHeight": 640,
		"decorated": true,
		"showUI": true,
		"scaleRes": 0.250000,
		"scaleResNav": 0.10000,
		"lockAspectRatio": true
	},

    ...
]
```

The JSON configuration file comprises an array of JSON objects. Each object - surrounded by curly brackets - contains information about an off-axis projection camera and the window that shows the camera view.
- ```hostName``` specifies the host responsible for this JSON object. 
- ```topLeft```, ```botLeft```, and ```botRight``` are positions of three corners of a projection plane, i.e., a physical display.
- ```eye``` is the camera position. 
- Four keys that start with ```mullion``` are sizes of mullions on four sides of a display. These values are used to shrink the projection plane, accounting for display frames.  
- ```display``` sets which display to show the window.
- Four keys that start with ```screen``` set the position and size of a window in screen coordinates.

- `decorated` specifies whether the window will have decorations such as a border, a close widget, etc. 
- `showUI` specifies whether the window will have the menu bar.
- `scaleRes` specifies the resolution scale of the rendering. 
- `scaleResNav` specifies the resolution scale when the camera is moving.
- `lockAspectRatio` preserves the relative width and height when you resize the window.
