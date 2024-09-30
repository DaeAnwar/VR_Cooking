using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
public interface IGameManager
{
    // ToolInteractionDetector needs
    GameObject GetOutcome(string IngridentName);
    public  Task InstantiateOutcomeAndHandleInteraction(string outcomePrefabName);
}