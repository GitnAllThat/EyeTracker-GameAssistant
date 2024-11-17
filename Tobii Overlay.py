from typing import List
import functools            #Simplifies storing and using functions if stored functions are from another class


import math

from pynput.keyboard import Key, KeyCode
from pynput.keyboard import Listener as KeyListener
from pynput.mouse import Listener as MouseListener
from pynput.mouse import Button

from pynput import mouse




import pygame, sys
from pygame.locals import *
import pyautogui

import win32api, win32con, win32gui, win32print

import win32pipe, win32file


import re



class InputButton:
    def __init__(self, key):
        self.key = key
        self.pressed = False
        self.released = False

    def press(self):
        self.pressed = True
        self.released = False

    def release(self):
        self.pressed = False
        self.released = True

    def reset(self):
        self.pressed = False
        self.released = False


class Keybind:
    def __init__(self, input_button, action=None):
        self.input_button: InputButton = input_button
        self.keybind_action = action



    def run_keybind_action(self):
        if self.keybind_action is not None:
            self.keybind_action()










class KeybindManager:
    def __init__(self):
        self.keybinds: List[Keybind] = []  # Explicitly annotate the type of keybindings
        self.mouse_button_right = InputButton(Button.right)
        self.mouse_button_left = InputButton(Button.right)
        self.test = False

    def add_keybinding(self, key, action=None):
        self.keybinds.append(Keybind(InputButton(key), action))

    def remove_keybinding(self, key):
        self.keybinds = [keybind for keybind in self.keybinds if keybind.input_button != key]  #basically the first "binding" is the newList

    def handle_key_release(self, key):
        for keybind in self.keybinds:
            if keybind.input_button.key == key:
                keybind.run_keybind_action()
                



    def click_at_gazePos(self):
        # Release the right mouse button if it is down
        if self.mouse_button_right.pressed:
            self.simulate_mouse_release('right')

        pyautogui.moveTo(averagedCoords)
        pyautogui.click()

        # Press the right mouse button again if it was originally down
        if self.mouse_button_right.pressed:
            self.simulate_mouse_down('right')      



    def simulate_mouse_down(self, button_name):
        self.pause_listener()
        pyautogui.mouseDown(button=button_name)
        self.resume_listener()

    def simulate_mouse_release(self, button_name):
        self.pause_listener()
        pyautogui.mouseUp(button=button_name)
        self.resume_listener()

    def pause_listener(self):
        global pause_mouse_listener
        pause_mouse_listener = True

    def resume_listener(self):
        global pause_mouse_listener
        pause_mouse_listener = False

def exit_program(message):
    print(message)
    global gameRunning
    gameRunning = False


def on_click(x, y, button, pressed):
    global pause_mouse_listener
    if not pause_mouse_listener:
        global keybind_manager  # Declare as global to modify it inside the function
        if button == Button.right:
            if pressed:
                keybind_manager.mouse_button_right.pressed = True
            else:
                keybind_manager.mouse_button_right.released = True

        if button == Button.left:
            if pressed:
                keybind_manager.mouse_button_left.pressed = True
            else:
                keybind_manager.mouse_button_left.released = True


        #print("Right button Pressed is:", keybind_manager.mouse_button_right.pressed,"Right button Released is:", keybind_manager.mouse_button_right.released)
        if pressed:
            print(f'Mouse button {button} pressed at ({x}, {y})')
        else:
            print(f'Mouse button {button} released at ({x}, {y})')
    else:
        print("Simulated Mouse Click")
    





def on_press(key):
    print('{0} pressed'.format(key))

def on_release(key):
    print('{0} release'.format(key))
    
    keybind_manager.handle_key_release(key)









        













#Gets the screen Resolution. https://stackoverflow.com/questions/73268410/getting-display-resolution-with-python-isnt-accurate
def get_dpi():
  hDC = win32gui.GetDC(0)
  HORZRES = win32print.GetDeviceCaps(hDC, win32con.DESKTOPHORZRES)
  VERTRES = win32print.GetDeviceCaps(hDC, win32con.DESKTOPVERTRES)
  return [HORZRES,VERTRES]



def UpdateGazeCoordinates(coordinates):

    try:
        data = win32file.ReadFile(handle, 64 * 1024)  # Maximum buffer size
        gazeData = data[1].decode().strip()
        gazeData = re.findall(regExPattern, gazeData)         #returns a list of tuples

        if len(gazeData) == 1:
            gazeData = [float(coord) for coord in gazeData[0]]    #Convert coordinates to float
            coordinates[0] = gazeData[0]
            coordinates[1] = gazeData[1]
            clamp_coordinates_to_screen_bounds(coordinates,screensize)

    except win32file.error as e:
        if e.winerror == 231:
            # Pipe is busy, wait and retry
            print("The Pipe i busy.")
            win32pipe.WaitNamedPipe(pipe_name, 5000)
        else:
            # Handle other errors
            print("Error:", e)
            global gameRunning
            gameRunning = False

    



#Out of bound coordinate cause error using Blit
def clamp_coordinates_to_screen_bounds(coordinates, screen_bounds):
    coordinates[0] = max(0, min(coordinates[0], screen_bounds[0] - 1))  #clamp x coordinate
    coordinates[1] = max(0, min(coordinates[1], screen_bounds[1] - 1))  #clamp y coordinate




def smooth_coordinates(current_coords_list, previous_smoothed_coords_list):
    smoothed_coords_list = []
    total_weight = 0

    # Compute total weight using smooth step
    for i in range(len(current_coords_list)):
        weight = (i + 1) ** 2  # Quadratic smooth step
        total_weight += weight

    # Apply exponential smoothing with smooth step
    for i in range(len(current_coords_list)):
        current_coords = current_coords_list[i]
        previous_smoothed_coords = previous_smoothed_coords_list[i]
        weight = (i + 1) ** 2  # Quadratic smooth step

        # Smoothed coordinate calculation without enumerate
        smoothed_coords =(
                            (weight * current_coords[0] + (total_weight - weight) * previous_smoothed_coords[0]) / total_weight,
                            (weight * current_coords[1] + (total_weight - weight) * previous_smoothed_coords[1]) / total_weight
                        )
        smoothed_coords_list.append(smoothed_coords)

    return smoothed_coords_list


#Calculate the Euclidean distance between two points.
def get_distance(point1, point2):
    return math.sqrt((point2[0] - point1[0])**2 + (point2[1] - point1[1])**2)







#print(dir(win32api))   #Prints what win32api contains

























screensize = get_dpi()


pygame.init()

screen = pygame.display.set_mode((screensize[0],screensize[1]),pygame.HWSURFACE|pygame.NOFRAME)



orcCursor = pygame.image.load("Orc Cursor Red.png").convert_alpha()

fuchsia = (255, 0, 128)  # Transparency color
hwnd = pygame.display.get_wm_info()["window"] # Handle
styles = win32gui.GetWindowLong(hwnd, win32con.GWL_EXSTYLE)
styles = styles | win32con.WS_EX_LAYERED | win32con.WS_EX_TRANSPARENT

win32gui.SetWindowLong(hwnd, win32con.GWL_EXSTYLE, styles)
win32gui.SetLayeredWindowAttributes(hwnd, win32api.RGB(*fuchsia), 0, win32con.LWA_COLORKEY)      #Sets transparency(In this case Fuchsia)
win32gui.SetWindowPos(hwnd, win32con.HWND_TOPMOST, 0, 0, screensize[0], screensize[1], 0)        #Makes the window Top Most

screen.fill(fuchsia)
pygame.display.flip()












# Create a listeners for keyboard and mouse events
key_listener  = KeyListener(on_press=on_press, on_release=on_release)
key_listener .start()

mouse_listener = MouseListener(on_click=on_click)
mouse_listener.start()

pause_mouse_listener = False



gameRunning = True  #Global Variable To Close Program

gazeCoordinates = [0,0]
averagedCoords = [0,0]
totalCoordsToAverage = 10
gazeCoordinatesListCurrent = [] 
gazeCoordinatesListPrevious = [] 
threshold_distance = 150




##Setup Keybinds
keybind_manager = KeybindManager()
# Use functools.partial to bind the function to the instance and set arguments and keyword arguments. e.g.:
#bound_function = functools.partial(another_instance.my_function, "arg1", "arg2", kwarg1="value1", kwarg2="value2")     #An Example if keybind_manager.click_at_gazePos had arguments and keywords
bound_function = functools.partial(keybind_manager.click_at_gazePos)
keybind_manager.add_keybinding(KeyCode.from_char('f'), bound_function)
bound_function = functools.partial(exit_program, "Exiting Program")
keybind_manager.add_keybinding(Key.esc, bound_function)   





# Define the regex pattern to match integers and decimal numbers
regExPattern = r"X:([\d.]+), Y:([\d.]+)"



pipe_name = r'\\.\pipe\mypipe'
handle = None  # Define handle outside try block




















try:
    # Connect to the named pipe
    handle = win32file.CreateFile(
        pipe_name,
        win32file.GENERIC_READ,  # Open for reading
        0,
        None,
        win32file.OPEN_EXISTING,
        0,
        None
    )

    print("Connected to the named pipe.")

    #Game Loop
    while gameRunning:
        # Receive coordinates if available
        UpdateGazeCoordinates(gazeCoordinates)
        
        gazeCoordinatesListPrevious = gazeCoordinatesListCurrent.copy()
        gazeCoordinatesListCurrent.append(gazeCoordinates.copy())
        if len(gazeCoordinatesListPrevious) != len(gazeCoordinatesListCurrent):
            gazeCoordinatesListPrevious = gazeCoordinatesListCurrent.copy()

        if len(gazeCoordinatesListCurrent) > totalCoordsToAverage:
            gazeCoordinatesListCurrent.pop(0)


        if len(gazeCoordinatesListCurrent) >= 2:
            # Check if the distance between the oldest and newest points exceeds the threshold
            oldest_point = gazeCoordinatesListCurrent[0]  # Oldest point
            newest_point = gazeCoordinatesListCurrent[-1]  # Newest point
            if get_distance(oldest_point, newest_point) > threshold_distance:
                # Clear the list if the distance exceeds the threshold
                gazeCoordinatesListCurrent = [gazeCoordinates.copy()]
            else:
                smooth_coordinates(gazeCoordinatesListCurrent, gazeCoordinatesListPrevious)
                

        averagedCoords = [sum(coords) / len(gazeCoordinatesListCurrent) for coords in zip(*gazeCoordinatesListCurrent)]


        #print("current:", gazeCoordinatesListCurrent)
        #print("previous:", gazeCoordinatesListPrevious)
        #print("Coords:",gazeCoordinates," and after smoothing:", averagedCoords)

        pygame.event.get()  #You need to regularly make a call to one of four functions in the pygame.event module in order for pygame to internally interact with your OS. Otherwise the OS will think your game has crashed.
        




        screen.fill(fuchsia)
        screen.blit(orcCursor,averagedCoords)
        #pygame.display.update()
        pygame.display.flip()

except Exception as e:
    print("Error:", e)

finally:
    # Close the named pipe handle if it's not None
    if handle is not None:
        win32file.CloseHandle(handle)

    #WARNING PROBABLY MAKE A DISPOSE FUNCTION
    key_listener.stop()
    mouse_listener.stop()



