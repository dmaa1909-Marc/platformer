# platformer
Learning unity and using github.
This documentation is provided on the back of my current understanding of methods and the unity.API based on Acacia Developer's youtube tutorials.
This man deserves som credit for a well executed turorial and great explanations on the subject.
Acacia's youtube channel (currently 4k subs | Video has <4k views): https://www.youtube.com/channel/UCXUiajdy-DV4D_aQ2wr-7Sg


--Movement--
The code also has some documentation on different parameters and explanations to different methods, that doesn'r require as much background knowledge.


Verical: W/S
Horizontal: D/A

Vertical and horizontal values are 1 / -1, based on which key is pressed, when no key is pressed the value is defaulted to 0.
We will move the player using these inputs and multiplying them with a Vector3, which represents a direction in 3D space.
This means that if W is pressed the vector has a size of one, if no key is pressed is has a size of zero and if S is pressed it has a size of -1.

The vector's forwards/backwards/left/right-movement is based off the orientation of the camera, and the camera's up axis also rotates the parent object.
This ensures the foward vector of the object always is facing the direction of the camera.
If we did not implement this, it would result in a mess when the player would move- and look around.

By using the getAxisRaw method we ensure to not have the player "sliding" when the wasd buttons is no longer pressed.
In other words the value is instantly put at the value, when a butten is pressed or released.
This is due to the regular getAxis method having a smoothness effect between transitioning between the values.

Now we're adding a smoothness effect to the movement and look of the player.
This may sound contradicory to why we used the getAxisRaw method, but if there is no smoothness, the feel of the movement will be very rigid and stiff.
This way also let's us control the amount of smoothness we want to apply.

Applying gravity in the script instead of the unity engine itself to not fiddle with the gravity of the entire game.