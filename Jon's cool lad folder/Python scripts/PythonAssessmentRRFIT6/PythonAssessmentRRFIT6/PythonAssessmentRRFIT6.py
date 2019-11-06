from DataForm import DataForm
import os

rawData =  open("RawData.txt", "r+")
database = []

def ExtractData():
    #Add a new entry to the database infinitely...
    while True:
        #Be sure to check the Dataform class to see what is being entered into it.
        database.append(DataForm(rawData.readline(), rawData.readline(), rawData.readline(), rawData.readline(), rawData.readline(), rawData.readline(), rawData.readline()))

        #...until an empty line is read, then stop the loop.
        if rawData.readline() == "":
            break

def PrintAllData(inputDB):

    #For every entry in the database we choose to print...
    for x in inputDB:
        PrintDataEntry(x)

def PrintDataEntry(dataForm):

    #Print out all the info in this DataForm
    outputString = ""
        
    outputString += "ID: " + dataForm.id
    outputString += "First name: " + dataForm.firstName
    outputString += "Last name: " + dataForm.lastName
    outputString += "Address Line 1: " + dataForm.address1
    outputString += "Address Line 2: " + dataForm.address2
    outputString += "Post code: " + dataForm.postCode
    outputString += "Phone number: " + dataForm.phoneNumber
    outputString += "---------------------------------------\n"

    print(outputString)

def saveData(inputDB):
    global rawData

    savingString = ""

    #For every member in the database, add all its info and space it
    for x in inputDB:
        savingString += x.id
        savingString += x.firstName
        savingString += x.lastName
        savingString += x.address1
        savingString += x.address2
        savingString += x.postCode
        savingString += x.phoneNumber
        savingString += "\n"

    rawData.seek(0)
    #Overwrite the original file, making sure to get rid of the extra carriage return at the end
    rawData.write(savingString[:-1])



def CheckForValidInput(inputString, dataType):
    #We just use this function to make sure the input entered by the user is the correct kind of input. eg: A phone number is 11 integers
    while True:   
        if dataType.lower() == "phonenumber":
            try:
                phoneNumber = int(input(inputString + "\n"))
                canContinue = True
            except:
                inputString = "Please enter a valid number"
                canContinue = False
            
            if canContinue == True:
                #We use this to count how many digits in the phone number. It sould have 11
                numOfDigits = 0
                tempInt = phoneNumber
                while (tempInt > 0):
                    tempInt = tempInt // 10
                    numOfDigits += 1

                if numOfDigits == 11:
                    #It's important to keep any numerical entries saved as a string in the database, since that's how it's going to be written back in a save, plus it lets us search for it easier
                    return str(phoneNumber)
                    break
                else:
                    inputString = "Please enter a full phone number, no international codes, no spaces"

        elif dataType.lower() == "string":
            try:
                return str(input(inputString + "\n"))
                break
            except:
                inputString = "Please enter valid text"

        elif dataType.lower() == "id" or dataType.lower() == "int":
            try:
                number = int(input(inputString + "\n"))

                if dataType.lower() == "id":
                #If the user enters an ID that is outside the range of the the database's size...
                    if number > len(database) and number < 0:
                        inputString = "Please enter a valid ID"
                    else:
                        return str(number)
                        break
                else:
                    return str(number)                 
                    break
            except:
                if dataType.lower() == "id":
                    inputString = "Please enter a valid ID"
                elif dataType.lower() == "int":
                    inputString = "Please enter a valid number"


def AddUser():

    #This looks complicated, but it just auto gets the ID by adding one to the length of the list, then asks the user for their input on the information, making sure it's valid while they're at it.
    database.append(DataForm(str(len(database) +1) + "\n",
                    CheckForValidInput("Please enter your first name", "string") + "\n",
                    CheckForValidInput("Please enter your last name", "string") + "\n",
                    CheckForValidInput("Please enter the first line of your address", "string") + "\n",
                    CheckForValidInput("Please enter the second line of your address", "string") + "\n",
                    CheckForValidInput("Please enter your Post Code", "string") + "\n",
                    str(CheckForValidInput("Please enter your phone number, no spaces", "phonenumber")) + "\n"
                    ))

def RemoveUser():
    #Make sure an int is given for the user ID, then check every member of the databse until a matching ID is found
    remUserID = CheckForValidInput("Please enter the user ID of the user you'd like to delete. '0' will skip this ", "id")

    for x in database:
        if x.id == remUserID + "\n":
            database.remove(x)

def DisplayUser():

    while True:
    #Figure out what the user wants to search by
        searchVariableType = CheckForValidInput("Would you like search by:\n[1]: Seach by ID\n[2]: Search by first name\n[3]: Search by last name\n[4]: Search by post code\n[5]: Search by phone number\nOr type q to quit", "int")
        searchValue = CheckForValidInput("Please now enter the data you'd like to search for", "string")
        searchValue += "\n"

        #For each of these types, search every member of the databse for any matches, and print if any are found
        if searchVariableType == "1":
            for x in database:
                if x.id == searchValue:
                    PrintDataEntry(x)
        if searchVariableType == "2":
            for x in database:
                if (x.firstName.find(searchValue) != -1):
                    PrintDataEntry(x)
                else:
                    print ("No match found")
        if searchVariableType == "3":
            for x in database:
                if (x.lastName.find(searchValue) != -1):
                    PrintDataEntry(x)
                else:
                    print ("No match found")
        if searchVariableType == "4":
            for x in database:
                if (x.postCode.find(searchValue) != -1):
                    PrintDataEntry(x)
                else:
                    print ("No match found")
        if searchVariableType == "5":
            for x in database:
                if (x.phoneNumber.find(searchValue) != -1):
                    PrintDataEntry(x)
                else:
                    print ("No match found")

        if searchVariableType == "q":
            break


ExtractData()
AddUser()
PrintAllData(database)
RemoveUser()
PrintAllData(database)
DisplayUser()
saveData(database)

