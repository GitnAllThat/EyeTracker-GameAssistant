### Purpose:

Enhance gaming accessibility by automating actions based on where the user looks.
Designed for games requiring precise targeting, like area-based actions e.g. Wows grenade throw.

---
### How to Run:

First, open and run the Tobii Server.sln project (this is a C# solution file). This will start the eye-tracking server.
Then, run the Tobii Overlay.py script (Python file). This will receive the gaze data from the server and allow customization of keybinds and automation based on user gaze.

---
### Reason for Creation:
I developed this program to teach myself Python while working on a practical project. 
As I was learning Python, I wanted to challenge myself by integrating it with hardware (eye-tracking) and creating something functional that could be applied in a real-world context. 
This project allowed me to explore cross-language interaction between C# and Python, as well as to gain experience with game accessibility concepts and automation.

---
### Languages:

C#: Used to interface with the eye-tracking hardware, leveraging its licensing and library support.
Python: Enables flexible customization for keybinds, mouse movements, and clicking.

---
### Features:

Pulls real-time gaze data from the eye tracker using a C# program.
Sends the data via piping or other IPC (Inter-Process Communication) mechanisms to the Python program.
User-configurable keybinds in Python for automating mouse movements and clicks.

---
### Learning Outcome:

Showcases cross-language interaction (C# and Python) for a seamless solution.
Demonstrates practical use of licensing and hardware-specific APIs in C#.

---
