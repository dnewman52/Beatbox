////AI Component
////Returns tables which dictace the harmonic resonance of each note over the root note
////Should eventually be able to compile tables based on dissonance values calculated in other scripts
////Currently using fixed calcuations based on ratio calculated levels of dissonance

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TableManager {
    /// <summary>
    /// Returns a list of integers which dictate the percentage chance of each note (step value) occuring over the given set of harmonies
    /// </summary>
    public static List<int> GetHarmonyTable(NoteName[] harmonies, TableType tableType) {
        return GetHarmonyTable(harmonies, GetTable(tableType));
    }

    /// <summary>
    /// Returns a list of integers which dictate the percentage chance of each note (step value) occuring over the given set of harmonies
    /// </summary>
    public static List<int> GetHarmonyTable(NoteName[] harmonies, List<int> baseTable)
    {
        List<int> weightedTable = baseTable; //the template / assumed values
        List<int> harmonyTable = new List<int>(); //the average values of all harmonies stacked together
        List<int> percentageTable = new List<int>(); //the average values converted to percentages

        //initially populate the tables with 0 for every note
        foreach (NoteName n in Enum.GetValues(typeof(NoteName)))
        {
            harmonyTable.Add(0);
            percentageTable.Add(0);
        }

        //for every harmony, stack all the different weighted values together for each possible note
        foreach (NoteName hNote in harmonies)
        {
            foreach (NoteName nNote in Enum.GetValues(typeof(NoteName)))
            {
                int h = KeyboardInput.NoteToStep(hNote);
                int n = KeyboardInput.NoteToStep(nNote);
                int i = (n - h + 12) % 12;
                int r = weightedTable[i];
                harmonyTable[n] += r;
            }
        }

        //divide the totals by the number of harmonies added together to get the average values
        for (int i = 0; i < 12; i++)
        {
            harmonyTable[i] = Mathf.RoundToInt(harmonyTable[i] * 10 / harmonies.Length);
        }

        //turn average weights into percentages to easily calculate probabilities
        int total = 0;
        string output = "| ";
        foreach (int h in harmonyTable) { total += h; }
        for (int i = 0; i < 12; i++)
        {
            percentageTable[i] = Mathf.RoundToInt(harmonyTable[i] * 100.0f / total);
            output += percentageTable[i] + " | ";
        }
        Debug.Log(output);
        return percentageTable;
    }

    private static List<int> GetTable(TableType tableType)
    {
        List<int> result = new List<int>();

        if (tableType == TableType.AllEqual)
        {
            foreach (NoteName n in Enum.GetValues(typeof(NoteName)))
            {
                result.Add(1);
            }
        }
        else if (tableType == TableType.RatioBasedTable)
        {
            result.Add(1);  //C
            result.Add(11); //Db
            result.Add(8);  //D
            result.Add(6);  //Eb
            result.Add(5);  //E
            result.Add(3);  //F
            result.Add(12); //Gb
            result.Add(2);  //G
            result.Add(7);  //Ab
            result.Add(4);  //A
            result.Add(10); //Bb
            result.Add(9);  //B
        }
        else if (tableType == TableType.MajorScale) {
            result.Add(100);  //C
            result.Add(1); //Db
            result.Add(50);  //D
            result.Add(1);  //Eb
            result.Add(50);  //E
            result.Add(100);  //F
            result.Add(1); //Gb
            result.Add(100);  //G
            result.Add(1);  //Ab
            result.Add(100);  //A
            result.Add(1); //Bb
            result.Add(50);  //B
        }

        return result;
    }
}
