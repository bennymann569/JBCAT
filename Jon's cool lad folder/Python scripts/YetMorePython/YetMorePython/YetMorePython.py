listOfWords = []
inputtingStrings = True

while (inputtingStrings == True):
    inputString = input("Please enter a word to add to the list or 'done' \n").lower()
    if (inputString != "done"):
        listOfWords.append(inputString)
    else:
        inputtingStrings = False

print("The words sorted into alphabetical order are: ")
for word in listOfWords:
    print(word)