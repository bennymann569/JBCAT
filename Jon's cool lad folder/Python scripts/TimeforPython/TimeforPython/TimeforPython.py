firstName = ""
lastName = ""
validFirstName = False
validLastName = False
bothNamesValid = False
inputString = "Please enter your first name. \n"

def CheckForValidInput(input):
    if (input == ""):
        return None, None;
    else:
        return True, input;

 #A loop that runs forever until both names are valid, then we go onto the second part of the code
while(bothNamesValid == False):
    #If the first name hasn't been entered...
    if (validFirstName == False and validLastName == False):
        validFirstName, firstName = CheckForValidInput(input(inputString))
    
    #If the first name has been entered...
    elif (validFirstName == True and validLastName == False):
    #If just the first name has been entered...
        inputString = "Please enter your last name. \n"
        validLastName, lastName = CheckForValidInput(input(inputString))

    #If both names have been entered...
    elif (validFirstName == True and validLastName == True):
        print(firstName, lastName + " is really gay gotteeem")
        bothNamesValid = True
    
    #If either name has been entered as a Null
    else:
        if (validFirstName == None):
            inputString = "Please enter a valid first name. \n"
            validFirstName, firstName = CheckForValidInput(input(inputString))
        
        elif (validLastName == None):
            inputString = "Please enter a valid last name. \n"
            validLastName, lastName = CheckForValidInput(input(inputString))
            
        else:
            print ("How the actual fuck have you got here?")

input("Press any key and watch the world descend to chaos :D")

number = 1
while (True):
    print (str(number))
    number *= 11