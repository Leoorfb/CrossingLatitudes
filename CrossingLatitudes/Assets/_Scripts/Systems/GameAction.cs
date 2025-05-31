using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction
{
    public List<GameAction> PreReactions { get; private set; }
    public List<GameAction> PerformReactions { get; private set; }
    public List<GameAction> PostReactions { get; private set; }
}
