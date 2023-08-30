using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// toujours mettre un " " apres les ( )

[System.Serializable]
public class Combat_String_to_Formula
{
    public Chara_BaseStats caster;
    public Chara_BaseStats target;
    //public string formulaInString = "je mappel banane";

    // check all word in fornula
    [SerializeField] List<string> wordList = new List<string>();
    // 
    [SerializeField] List<string> dicoKey = new List<string>();
    [SerializeField] List<float> formula2 = new List<float>();
    [SerializeField] List<string> operatorList = new List<string>();
    public float formualeFinal;

    Dictionary<string, float> dicoData = new Dictionary<string, float>();
    //Dictionary<string, string> dicoSymbols = new Dictionary<string, string>();

    public float StartSetFormula(string formulaInString, Chara_BaseStats caster, Chara_BaseStats target)
    {
        ClearList();
        this.caster = caster;
        this.target = target;
        //Debug.Log(caster);
        //Debug.Log(target);
        SetupDico();
        //DivideWord(formulaInString);
        //CompareString();
        //return BuildFormula();
        return GetResult(0, ConvertData(formulaInString, dicoData));
    }


    #region methode de raff

    private string ConvertData(string functionToConvert, Dictionary<string, float> dico)
    {
        foreach (KeyValuePair<string, float> item in dico)
        {
            if (functionToConvert.Contains(item.Key))
            {
                //Debug.Log("Stat  = " + item.Key + "  Value  =  " + item.Value.ToString());
                functionToConvert = functionToConvert.Replace(item.Key, item.Value.ToString());
            }
        }

        return functionToConvert;
    }

    private float GetResult(int pos, string function)
    {
        string sentence = "";

        int bracketsAmount = 0;

        for (int i = pos; i < function.Length; i++)
        {
            if (function[i].ToString() == "(")
            {
                sentence += GetResult(i + 1, function);

                do
                {
                    if (function[i].ToString() == "(")
                    {
                        bracketsAmount++;
                    }

                    if (function[i].ToString() == ")")
                    {
                        bracketsAmount--;
                    }

                    i++;
                } while (bracketsAmount != 0);
            }

            if (i >= function.Length)
            {
                break;
            }

            if (function[i].ToString() == ")")
            {
                break;
            }
            sentence += function[i];
        }

        //Debug.Log("sentence = " + sentence);
        //Debug.Log(SplitCalcul(sentence));
        //Debug.Log("split calcul sentence = " + CalculatePriority(SplitCalcul(sentence)));
        //return (sentence);
        return CalculatePriority(SplitCalcul(sentence));
    }

    private string[] SplitCalcul(string calcul)
    {
        List<string> words = new List<string>();

        string newWord = "";

        calcul = calcul.Replace(" ", "");
        //calcul = calcul.Replace("  ", "");
        //calcul = calcul.Replace("   ", "");

        for (int i = 0; i < calcul.Length; i++)
        {
            if (!float.TryParse(calcul[i].ToString(), out float value))
            {
                words.Add(newWord);
                words.Add(calcul[i].ToString());
                newWord = "";
                continue;
            }

            newWord += calcul[i].ToString();
            //Debug.Log(newWord);
        }

        words.Add(newWord);

        return words.ToArray();
    }

    private float CalculatePriority(string[] section)
    {
        List<float> values = new List<float>();
        List<string> operators = new List<string>();

        //string result = "";

        for (int i = 0; i < section.Length; i++)
        {
            if (float.TryParse(section[i], out float value))
            {
                values.Add(value);
            }
            else if (section[i] != " " && section[i] != "")
            {
                operators.Add(section[i]);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            //for (int j = 0; j < operators.Count - 1; j++) this was the original bugged version, keep it in case
            for (int j = 0; j < operators.Count; j++)
            {
                bool checker = false;
                switch (i)
                {
                    case 0:
                        switch (operators[j])
                        {
                            case "*":
                                values[j] *= values[j + 1];
                                checker = true;
                                break;
                            case "/":
                                values[j] /= values[j + 1];
                                checker = true;
                                break;
                            case "^":
                                values[j] = Mathf.Pow(values[j], values[j + 1]);
                                checker = true;
                                break;
                        }
                        break;
                    case 1:
                        switch (operators[j])
                        {
                            case "+":
                                values[j] += values[j + 1];
                                checker = true;
                                break;
                            case "-":
                                values[j] -= values[j + 1];
                                checker = true;
                                break;
                            default:
                                break;
                        }
                        break;
                }

                if (checker)
                {
                    values.RemoveAt(j + 1);
                    operators.RemoveAt(j);
                    j--;
                }
            }
        }

        return values[0];
    }



    #endregion

    #region old formula

    private List<string> DivideWord(string text)
    {
        //Debug.Log("qqqq") ;
        string wordToAdd = "";
        //foreach(char character in text)
        foreach (char character in text)
        {

            if (character.ToString() == " ")
            {
                wordList.Add(wordToAdd);
                wordToAdd = "";
            }
            else
            {
                wordToAdd += character;
            }

            string operatorE = character.ToString();
            switch (operatorE)
            {

                case "+":
                    operatorList.Add(operatorE);
                    break;

                case "-":
                    operatorList.Add(operatorE);
                    break;

                case "*":
                    operatorList.Add(operatorE);
                    break;

                case "/":
                    operatorList.Add(operatorE);
                    break;

            }
        }

        wordList.Add(wordToAdd);
        //Debug.Log(Motquejeveuxcreer);
        //Debug.Log(wordToAdd);
        return wordList;
    }

    void CompareString()
    {
        foreach (string word in wordList)
        {
            if (dicoData.ContainsKey(word))
            {
                //float value;
                dicoData.TryGetValue(word, out float value);
                //print(value);
                dicoKey.Add(word);
                formula2.Add(value);
            }
            else if (float.TryParse(word, out float value))
            {
                dicoKey.Add(word);
                float f = float.Parse(word);
                formula2.Add(f);
            }
        }
    }

    float BuildFormula()
    {
        float result;
        result = formula2[0];
        for (int i = 0; i < operatorList.Count; i++)
        {
            //print(formula2[i]);      // * formula2[1]);
            // result = PerformCalculation(operatorList[i], result, formula2[i + 1]);
        }
        return result;
        // result    
        //print(result);
    }

    private float PerformCalculation(string text, float number1, float? number2)
    {
        float? nullableNumber = number2;
        float nonNullableNumber = nullableNumber.Value;

        if (text == "+")
        {
            return number1 + nonNullableNumber;
        }
        else if (text == "-")
        {
            return number1 - nonNullableNumber;
        }
        else if (text == "*")
        {
            return number1 * nonNullableNumber;
        }
        else if (text == "/")
        {
            // Warning: Integer division probably won't produce the result you're looking for.
            // Try using `double` instead of `int` for your numbers.
            return number1 / nonNullableNumber;
        }
        else if (number2.HasValue != true)
        {
            return number1;
        }
        else
        {
            return 0f;
        }
    }
    #endregion

    void SetupDico()
    {
        dicoData.Add("a.baseAcc", caster.Battle_Stats.baseAcc);
        dicoData.Add("a.baseAtk", caster.Battle_Stats.baseAtk);
        dicoData.Add("a.baseDef", caster.Battle_Stats.baseDef);
        dicoData.Add("a.baseEva", caster.Battle_Stats.baseEva);
        dicoData.Add("a.baseHP", caster.life_Stats.baseHP);
        dicoData.Add("a.baseLuck", caster.Battle_Stats.baseLuck);
        dicoData.Add("a.baseMatk", caster.Battle_Stats.baseMatk);
        dicoData.Add("a.baseMdef", caster.Battle_Stats.baseMdef);
        dicoData.Add("a.baseMP", caster.life_Stats.baseMP);
        dicoData.Add("a.baseShield", caster.life_Stats.baseShield);
        dicoData.Add("a.baseSpeed", caster.Battle_Stats.baseSpeed);
        dicoData.Add("a.baseMult", caster.Battle_Stats.baseCritMult);
        dicoData.Add("a.baseEvaCrit", caster.Battle_Stats.baseCritEva);
        dicoData.Add("a.baseRate", caster.Battle_Stats.baseCritRate);

        dicoData.Add("a.currPost", caster.life_Stats.postureCurr);
        dicoData.Add("a.currAcc", caster.Battle_Stats.currentAcc);
        dicoData.Add("a.currAtk", caster.Battle_Stats.currentAtk);
        dicoData.Add("a.currDef", caster.Battle_Stats.currentDef);
        dicoData.Add("a.currEva", caster.Battle_Stats.currentEva);
        dicoData.Add("a.currHP", caster.life_Stats.currentHP);
        dicoData.Add("a.currLuck", caster.Battle_Stats.currentLuck);
        dicoData.Add("a.currMatk", caster.Battle_Stats.currentMatk);
        dicoData.Add("a.currMdef", caster.Battle_Stats.currentMdef);
        dicoData.Add("a.currMP", caster.life_Stats.currentMP);
        dicoData.Add("a.currShield", caster.life_Stats.currentShield);
        dicoData.Add("a.currSpeed", caster.Battle_Stats.currentSpeed);
        dicoData.Add("a.currMult", caster.Battle_Stats.currentCritMult);
        dicoData.Add("a.currEvaCrit", caster.Battle_Stats.currentCritEva);
        dicoData.Add("a.currRate", caster.Battle_Stats.currentCritRate);

        dicoData.Add("b.baseAcc", target.Battle_Stats.baseAcc);
        dicoData.Add("b.baseAtk", target.Battle_Stats.baseAtk);
        dicoData.Add("b.baseDef", target.Battle_Stats.baseDef);
        dicoData.Add("b.baseEva", target.Battle_Stats.baseEva);
        dicoData.Add("b.baseHP", target.life_Stats.baseHP);
        dicoData.Add("b.baseLuck", target.Battle_Stats.baseLuck);
        dicoData.Add("b.baseMatk", target.Battle_Stats.baseMatk);
        dicoData.Add("b.baseMdef", target.Battle_Stats.baseMdef);
        dicoData.Add("b.baseMP", target.life_Stats.baseMP);
        dicoData.Add("b.baseShield", target.life_Stats.baseShield);
        dicoData.Add("b.baseSpeed", target.Battle_Stats.baseSpeed);
        dicoData.Add("b.baseMult", target.Battle_Stats.baseCritMult);
        dicoData.Add("b.baseEvaCrit", target.Battle_Stats.baseCritEva);
        dicoData.Add("b.baseRate", target.Battle_Stats.baseCritRate);

        dicoData.Add("b.currPost", target.life_Stats.postureCurr);
        dicoData.Add("b.currAcc", target.Battle_Stats.currentAcc);
        dicoData.Add("b.currAtk", target.Battle_Stats.currentAtk);
        dicoData.Add("b.currDef", target.Battle_Stats.currentDef);
        dicoData.Add("b.currEva", target.Battle_Stats.currentEva);
        dicoData.Add("b.currHP", target.life_Stats.currentHP);
        dicoData.Add("b.currLuck", target.Battle_Stats.currentLuck);
        dicoData.Add("b.currMatk", target.Battle_Stats.currentMatk);
        dicoData.Add("b.currMdef", target.Battle_Stats.currentMdef);
        dicoData.Add("b.currMP", target.life_Stats.currentMP);
        dicoData.Add("b.currShield", target.life_Stats.currentShield);
        dicoData.Add("b.currSpeed", target.Battle_Stats.currentSpeed);
        dicoData.Add("b.currMult", target.Battle_Stats.baseCritMult);
        dicoData.Add("b.currEvaCrit", target.Battle_Stats.baseCritEva);
        dicoData.Add("b.currRate", target.Battle_Stats.baseCritRate);


        // operator
        //dicoSymbols.Add("qqqq", + );



        //dicoData.Add(" + ", + );

    }

    void ClearList()
    {
        dicoData.Clear();
        wordList.Clear();
        dicoKey.Clear();
        formula2.Clear();
        operatorList.Clear();
        caster = null;
        target = null;
    }
}
