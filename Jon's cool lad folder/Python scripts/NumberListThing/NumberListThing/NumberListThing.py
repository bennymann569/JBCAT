numList = [x for x in range(1, 51)]
numList.extend(x for x in range(51, 61))

print(numList[1:61:2])
print(numList[2:61:3])