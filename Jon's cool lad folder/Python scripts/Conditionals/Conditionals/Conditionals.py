stringList = []
inputEntered = False
validInput = False
operatorList = ["+-x*/"]
questionString = "Please enter some maths. Separate with spaces, please. \n"

while True:
    #String List will be a list of lower case words, made from the user's input and split by whenever they use space
    while True:
        stringList = input(questionString).lower().split(" ")
        
        if (stringList[0] == "q") and len(stringList) == 1:
            quit()

        if len(stringList) == 3:
            break
        else:
            questionString = "Please enter a valid input. Don't forget spaces \n"
    
    #Now that we have a three word list, let's check that the first and third word are numbers
    try:
        for word in stringList[0::2]:
            tempInt = int(word)
    except:
        questionString = "Please enter valid numbers \n"
        continue

    #Run the relevant operator and output the answer
    if stringList[1] == "+":
        print(int(stringList[0]) + int(stringList[2]))

    elif stringList[1] == "-":
        print (int(stringList[0]) - int(stringList[2]))

    elif stringList[1] == "x":
        print(int(stringList[0]) * int(stringList[2]))

    elif stringList[1] == "/":
        print(int(stringList[0]) / int(stringList[2]))
    
    #If the second word isn't a valid operator, restart the process
    else:
        questionString = "Please enter a valid operator \n"
        continue

    questionString = "Press Q to quit, or do more maths to continue \n"