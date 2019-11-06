def ex1():
    for i in range(1500, 2701):
        if i % 7 == 0 and i % 5 == 0:
            print(i)

def ex2():
    for i in range(0, 7):
        if i == 3 or i == 6:
            continue
        else:
            print(i)

def ex3():
    for i in range(1, 51):
        if i%3 == 0 and i%5 == 0:
            print("FizzBuzz")
        elif i%3 == 0:
            print("Fizz")
        elif i%5 == 0:
            print("Buzz")
        else:
            print(i)

def ex4():
    outputString = ""
    
    for i in range(100,401):
        isAllEven = True        
        for digit in str(i):
            if int(digit) % 2 != 0:
                isAllEven = False

        if isAllEven == True:
            outputString += ", " + str(i)
      
    print(outputString[2:])

def ex5():
    queryString = "Please enter a number\n"
    while True:
        try:
            inputNumber = int(input(queryString))
            break
        except:
            queryString = "Please enter a valid number\n"
    isPrime = True

    for integer in range (2, inputNumber):
        if inputNumber % integer == 0:
            isPrime = False

    if isPrime == True:
        print ("This number is prime")
    else:
        print ("This number isn't prime")

def ex6():
    queryString = "Please enter a temperature\n"
    while True:
        try:
            inputNumber = float(input(queryString))
            break
        except:
            queryString = "Please enter a valid number\n"
    
    queryString = "Is this (C)elcius or (F)arenheit? (C or F)\n"
    while True:
        cOrF = input(queryString).upper()
        if cOrF == "C":
            print(str(inputNumber) + "'C = " + str(inputNumber * 1.8 + 32) + "'F")
            break
        elif cOrF == "F":
            print(str(inputNumber) + "'F = " + str((inputNumber -32) / 1.8) + "'C")
            break
        else:
            queryString = "Please enter (C)elcius or (F)arenheit\n"

def ex7():
    while True:
        userPassword = input("Please enter your password to be checked for validity\n")
        passwordIsValid = True
        outputLog = "Password is invalid because: \n"

        if len(list(userPassword)) < 6:
            outputLog += "\nThe password is under 6 characters, "
            passwordIsValid = False
        if len(list(userPassword)) > 16:
            outputLog += "\nThe password is over 16 characters, "
            passwordIsValid = False
        
        numOfLowerCase = 0
        numOfUpperCase = 0
        numOfNumbers = 0
        numOfSpecialChar = 0

        for char in userPassword:
            if char.islower():
                numOfLowerCase += 1
            if char.isupper():
                numOfUpperCase += 1
            if char.isnumeric:
                numOfNumbers += 1
            specialCharList = ["$", "#", "@"]
            for x in specialCharList:
                if char == x:
                    numOfSpecialChar += 1
        
        if numOfLowerCase < 1:
            outputLog += "\nThere aren't any lowercase characters, "
            passwordIsValid = False
        if numOfUpperCase < 1:
            outputLog += "\nThere aren't any upper case characters, "
            passwordIsValid = False
        if numOfNumbers < 1:
            outputLog += "\nThere aren't any numbers, "
            passwordIsValid = False
        if numOfSpecialChar < 1:
            outputLog += "\nThere aren't any special characters ($, # or @), "
            passwordIsValid = False
        
        if passwordIsValid == True:
            print("This password is valid!")
            break
        else:
            print(outputLog[:len(outputLog) - 2])


            


while True:

    myInput = str(input("Please chose which exercise you want to display (E.g: 2), Or type Q to quit\n")).lower()
    if myInput == "1":
        ex1()
    elif myInput == "2":
        ex2()
    elif myInput == "3":
        ex3()
    elif myInput == "4":
        ex4()
    elif myInput == "5":
        ex5()
    elif myInput == "6":
        ex6()
    elif myInput == "7":
        ex7()
    elif myInput == "q":
        exit()