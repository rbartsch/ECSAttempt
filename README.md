# ECSAttempt

This is my first attempt at creating an Entity-component-system in C# (https://en.wikipedia.org/wiki/Entity%E2%80%93component%E2%80%93system)

There are problems with it that can be much better implemented on a second attempt, such as using node systems and better use of hash maps to 
keep track of entities with components like a RenderNode when it comes to creating, updating, and destroying them less error-prone and more effective performance wise

This is a basic implementation that allows one to add their own further system as needed and components. It is NOT suited for production at all, merely a learning attempt.

Framework used to as an abstraction layer for graphics/image loading and update loop:
MonoGame 3.6 (with DirectX)

To build you may need to install MonoGame 3.6  (http://www.monogame.net/downloads/) and Visual Studio 2017 or later. C# 7.0 is used (.NET Framework 4.7)