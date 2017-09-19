#Component Pong

This simple Pong using <a href="https://github.com/poppgames/Component-Pong/blob/master/ConsoleApp1/Pong/GameObjects/GameObject.cs"><code>GameObjects</code></a> and <a href="https://github.com/poppgames/Component-Pong/blob/master/ConsoleApp1/Pong/Components/Component.cs"><code>Components</code></a> similar to the Unity Game Engine. It uses a Console Application to render the game. See ,<a href="https://github.com/poppgames/Component-Pong/blob/master/ConsoleApp1/Pong/GameObjects/Board.cs"><code>Board</code></a>. 

Another cool aspect is that Inputs and Action have been abstracted out, and use a Register, the Unregister system using an <a href="https://github.com/poppgames/Component-Pong/blob/master/ConsoleApp1/Pong/Input/InputManager.cs"><code>InputManager</code></a>. This will make key-mapping easier to implement in the future

It demonstrates my understanding of how a low-level system operates, as well as my advanced skills in Object-Oritend-programming.

![Alt text](/ConsoleApp1/Screenshot.PNG?raw=true "In Game Screenshot")
- Components allow us to add multiple <a href="https://github.com/poppgames/Component-Pong/blob/master/ConsoleApp1/Pong/Pong.cs"><code>Balls</code> and <code>Paddles</code></a> (scalable)
