﻿namespace Concordium.Sdk.Types.New;

public record RewardStatusV1(
    CcdAmount TotalAmount,
    CcdAmount TotalEncryptedAmount,
    CcdAmount BakingRewardAccount,
    CcdAmount FinalizationRewardAccount,
    CcdAmount GasAccount,
    CcdAmount FoundationTransactionRewards,
    DateTimeOffset NextPaydayTime,
    decimal NextPaydayMintRate,
    CcdAmount TotalStakedCapital) : RewardStatusBase(TotalAmount, TotalEncryptedAmount, BakingRewardAccount, FinalizationRewardAccount, GasAccount);