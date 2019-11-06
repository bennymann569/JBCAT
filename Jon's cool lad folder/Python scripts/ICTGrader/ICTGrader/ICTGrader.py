ictHomeworkMax = 25
assessmentMax = 50
ictFinalExamMax = 100

def CheckForValidFloatInput(inputStr, maxScore):
    while True:
        try:
            score = float(input(inputStr + "\n"))
            if score > maxScore and score >= 0:
                inputStr = "You can't that mark in this test"         
                continue
            return score
            break
        except:
            inputStr = "Please enter a number"

def ScoreString(score, maxScore):
    return str(score) + "/" + str(maxScore)

def CalcPercentage(score, maxScore):
    return str(score/maxScore * 100)

ictHomework = CheckForValidFloatInput("Enter your Homework score (Out of 25)", ictHomeworkMax)
assessment = CheckForValidFloatInput("Enter your Assessment score (Out of 50)", assessmentMax)
ictFinalExam = CheckForValidFloatInput("Enter your final exam score (Out of 100)", ictFinalExamMax)

homeWorkScore = ScoreString(ictHomework, ictHomeworkMax)
assessmentScore = ScoreString(assessment, assessmentMax)
finalExamScore = ScoreString(ictFinalExam, ictFinalExamMax)
totalScore = ScoreString((ictHomework + assessment + ictFinalExam), (ictHomeworkMax + assessmentMax + ictFinalExamMax))

homeWorkPercentage = CalcPercentage(ictHomework, ictHomeworkMax)
assessmentPercentage = CalcPercentage(assessment, assessmentMax)
ictFinalExamPercentage = CalcPercentage(ictFinalExam, ictFinalExamMax)
weightedPercentage = str((float(homeWorkPercentage) * 0.25) + (float(assessmentPercentage) * 0.35) + (float(ictFinalExamPercentage)) * 0.4)

titleLine = ["            ", "ICT Homework", "Assessment  ", "Final Exam  ", "Total       "]
scoreLine = ["Score  ", homeWorkScore, assessmentScore, finalExamScore, totalScore]
percentageLine = ["Percentage", homeWorkPercentage + "%", assessmentPercentage + "%", ictFinalExamPercentage + "%", weightedPercentage + "% (Weighted as: Homework: 25%, Assessment: 35%, Final Exam: 40%)"]

for row in zip(titleLine, scoreLine, percentageLine):
    print('  |  '.join(row))