year = 0
targetBalance = 0.0

def CheckForFloatInput(inputStr, outputStr):
    while True:
        try:
            return float(input(inputStr))
            break
        except:
            print(outputStr)


investment = CheckForFloatInput("Please enter how much you're going to invest\n£", "Please enter a valid number")

targetBalance = CheckForFloatInput("Please enter how much money you'd like to achieve through investment\n£", "Please enter a valid number")

currentMoney = investment
while currentMoney <= targetBalance:
    print ("In year " + str(year) + ", your balance will be: £" + str(currentMoney))
    year += 1
    currentMoney = currentMoney * 1.1

print("It will take you", str(year), "years to reach £" + str(targetBalance), "with your investment of £" + str(investment))