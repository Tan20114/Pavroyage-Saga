using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager
{
    List<Character> turnOrder;
    List<Character> listToAdd = new List<Character>();

    public void CreateTurnOrder(List<Character> characters)
    {
        turnOrder = new List<Character>(
            characters
                .Where(character => !character.IsDead)
                .OrderByDescending(character =>
                    character.derivedStats.GetDerived(DerivedType.Speed))
        );

        foreach (Character character in listToAdd)
        {
            if (!character.IsDead && !turnOrder.Contains(character))
            {
                turnOrder.Add(character);
            }
        }

        listToAdd.Clear();

        //foreach (Character character in turnOrder)
        //{
        //    Debug.Log(character.name + $"'s speed : {character.derivedStats.GetDerived(DerivedType.Speed)}");
        //}
    }

    public Character GetNextCharacter()
    {
        if (turnOrder.Count == 0) return null;

        Character character = turnOrder[0];
        turnOrder.RemoveAt(0);

        return character;
    }

    public void AddCharacter(Character character)
    {
        if (character == null || character.IsDead) return;

        if (turnOrder.Contains(character) || listToAdd.Contains(character)) return;

        listToAdd.Add(character);
    }


    public void RemoveCharacter(Character character)
    {
        turnOrder.Remove(character);
    }

    public IReadOnlyList<Character> VisualOnlyTurnOrder()
    {
        return turnOrder.Concat(listToAdd).ToList();
    }

    public bool HasNextTurn()
    {
        return turnOrder.Count > 0;
    }
}