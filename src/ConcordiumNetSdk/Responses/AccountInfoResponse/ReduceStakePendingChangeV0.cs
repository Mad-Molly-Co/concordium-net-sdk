namespace ConcordiumNetSdk.Responses.AccountInfoResponse;

/// <summary>
/// Represents the information about a reduce stake pending change version 0.
/// </summary>
public record ReduceStakePendingChangeV0 : StakePendingChangeV0, IReduceStakePendingChange
{
    /// <summary>
    /// Gets or initiates the new stake.
    /// </summary>
    public long NewStake { get; init; }
    
    /// <summary>
    /// Gets or initiates the stake pending change type.
    /// </summary>
    public StakePendingChangeType Change { get; init; } // StakePendingChangeType.ReduceStake
}
