# GameDevelopment
The coursework portfolio

#### Scene construction

Contains scene cosntruction, use of assets, scripts of trigger events.
![](https://github.com/RiverLeeGitHub/GameDevelopment/blob/master/Scene%20construction/demo.gif?raw=true)

#### Animation with/without code

Scene switching between to animations: one is an animation created through keyframing, and one is an animation with script and changes the properties of the ball according to the user's operations.
![](https://github.com/RiverLeeGitHub/GameDevelopment/blob/master/Animation%20with%20or%20without%20codes/demo.gif?raw=true)

#### ODE methods for simulation

Uses Ordinary Differential Equation (ODE) to simulate the movement of a pendulum. The methods included in the codes contain: 
* Euler's method
* The improved Euler's method (or known as Heun's method or trapezoidal rule method)
* Runge-Kutta integration
* Semi-implicit Euler method (or known as symplectic Euler)

The GIF below shows the simulation based on Runge-Kutta integration.
![](https://github.com/RiverLeeGitHub/GameDevelopment/blob/master/ODE%20simulation/demo.gif?raw=true)

#### Articulated bodies

This module contains the implementation of animation controllers and articulated bodies. The character can play different animations when taking relative movements: walking, running and jumping. And these animations can be transferred from one to another instantly. When it comes to the articulate bodies, I use three different objects to manifest the implementation of "spring joint", "fixed joint" and "hinge joint".

