dabFile = open ("testFile.txt", "r")
outputFile = ""

for line in range(10):
    if line % 2 == 0:
        outputFile += dabFile.readline()
    else:
        dabFile.readline()
dabFile = open("testFile.txt", "w")
dabFile.write(outputFile)
dabFile.close()