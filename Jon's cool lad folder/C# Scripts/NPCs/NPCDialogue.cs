/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string inputString;
    public string outputString;

    List<string> inputs = new List<string>();
    List<string> answer = new List<string>();
    List<string> answerA = new List<string>();
    List<string> answerB = new List<string>();
    List<string> answerC = new List<string>();

    public void populateLists()
    {

        inputs.Add("A");
        inputs.Add("B");
        inputs.Add("C");

        answer.Add("AnswerA");
        answer.Add("AnswerB");
        answer.Add("AnswerC");

        answerA.Add("AnswerA1");
        answerA.Add("AnswerA2");
        answerA.Add("AnswerA3");

        answerB.Add("AnswerB1");
        answerB.Add("AnswerB2");
        answerB.Add("AnswerB3");

        answerC.Add("AnswerC1");
        answerC.Add("AnswerC2");
        answerC.Add("AnswerC3");
    }


    public void sayResponse()
    {
        switch (inputString)
        {
            case inputs[0]:
                //Answer A to game
                outputString = answerA;
            case inputs[1]:
                //answer B to game
                outputString = answerB;
            case inputs[2]:
                // answer C
                outputString = answerC;
            default:
                break;
        }

    }
}
*/