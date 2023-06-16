﻿using Concordium.Sdk.Types.Mapped;

namespace Concordium.Sdk.Types.New;

public record PendingUpdatesV0(
    UpdateQueue<HigherLevelAccessStructureRootKeys> RootKeys,
    UpdateQueue<HigherLevelAccessStructureLevel1Keys> Level1Keys,
    UpdateQueue<AuthorizationsV0> Level2Keys,
    UpdateQueue<ProtocolUpdate> Protocol,
    UpdateQueue<decimal> ElectionDifficulty,
    UpdateQueue<ExchangeRate> EuroPerEnergy,
    UpdateQueue<ExchangeRate> MicroGTUPerEuro,
    UpdateQueue<ulong> FoundationAccount,
    UpdateQueue<MintDistributionV0> MintDistribution,
    UpdateQueue<TransactionFeeDistribution> TransactionFeeDistribution,
    UpdateQueue<GasRewards> GasRewards,
    UpdateQueue<BakerParameters> BakerStakeThreshold,
    UpdateQueue<AnonymityRevokerInfo> AddAnonymityRevoker,
    UpdateQueue<IdentityProviderInfo> AddIdentityProvider);