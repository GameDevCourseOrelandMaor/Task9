using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Score : NetworkBehaviour
{
    [SerializeField] NumberField ScoreDisplay;

    [Networked(OnChanged = nameof(NetworkedHealthChanged))]
    public int NetworkedScore { get; set; } = 0;
    private static void NetworkedHealthChanged(Changed<Score> changed)
    {
        // Here you would add code to update the player's healthbar.
        Debug.Log($"Score changed to: {changed.Behaviour.NetworkedScore}");
        changed.Behaviour.ScoreDisplay.SetNumber(changed.Behaviour.NetworkedScore);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    // All players can call this function; only the StateAuthority receives the call.
    public void ScoreRpc()
    {
        // The code inside here will run on the client which owns this object (has state and input authority).
        Debug.Log("Received ScoreRpc on StateAuthority, modifying Networked variable");
        NetworkedScore += 1;
    }
}
