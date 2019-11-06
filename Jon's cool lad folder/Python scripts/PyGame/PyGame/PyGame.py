from msvcrt import getch

#Bring in the classes we made for the game
from Room import Room
from Exit import Exit

roomDictionary = dict.fromkeys(['room1', 'room2', 'room3'])

currentRoom = Room(None, None)

outputLog = []

#Create the giant list of rooms. Inside here is gonna be a ton of text, so tread carefull!
def InstantiateRooms():
    global roomDictionary
    global currentRoom

    #Assign the dictionary of rooms its rooms' info.
    roomDictionary["room1"] = Room("This is our first test room", [Exit("n", "room2"), Exit("e", "room3")])
    roomDictionary["room2"] = Room("You have made it to the second room.", [Exit("s", "room1")])
    roomDictionary["room3"] = Room("Third room lads lads lads lads", [Exit("w", "room1")])

    #Assign the starting room to be the current room
    currentRoom = roomDictionary['room1']

def LoadRoom(roomToLoad):

    AddToOutput(roomToLoad.roomDesc)
    exitList = "Exits: "
    for x in roomToLoad.exits:
        exitList += x.direction + ", "

    AddToOutput(exitList[:len(exitList) -2])

def AttemptChangeRoom(wordsList):
    global currentRoom

    if len(wordsList) != 0:
        for x in currentRoom.exits:
            if wordsList[1] in x.direction:
                currentRoom = roomDictionary[x.roomName]
                LoadRoom(currentRoom)


def AddToOutput(stringIn):
    global outputLog
    outputLog.append(stringIn)

def DisplayOutput():
    global outputLog
    print("\n".join(outputLog))
    outputLog = []

def ParsePlayerInput(inputString):

    wordsList = inputString.split()
    if len(wordsList) != 0:
        if wordsList[0] == "go":
            AttemptChangeRoom(wordsList)
            return

    AddToOutput("I don't understand that command.")

def StartUp():

    InstantiateRooms()
    LoadRoom(currentRoom)
    DisplayOutput()


StartUp()

#Run an infinite loop until the player quits
while True:
    
    #key = ord(getch())
    #if key == 27: #Escape
    #    quit()  
    
    
    playerInput = input()
    ParsePlayerInput(playerInput)
    DisplayOutput()


    

