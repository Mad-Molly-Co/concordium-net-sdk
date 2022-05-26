using ConcordiumNetSdk.Responses.AccountInfoResponse;
using ConcordiumNetSdk.Responses.BlockInfoResponse;
using ConcordiumNetSdk.Responses.ConsensusStatusResponse;
using ConcordiumNetSdk.Responses.NextAccountNonceResponse;
using ConcordiumNetSdk.Types;

namespace ConcordiumNetSdk;

public interface IConcordiumNodeClient
{
    /// <summary>
    /// Retrieves an information about a state of account corresponding to account address and block hash.
    /// </summary>
    /// <param name="accountAddress">the base58 check with version byte 1 encoded address (with Bitcoin mapping table).</param>
    /// <param name="blockHash">the base16 encoded hash of a block (64 characters).</param>
    /// <returns><see cref="AccountInfo"/> - the state of an account in the given block.</returns>
    Task<AccountInfo?> GetAccountInfoAsync(AccountAddress accountAddress, BlockHash blockHash);

    /// <summary>
    /// Retrieves the best guess as to what the next account nonce should be.
    /// If all account transactions are finalized then this information is reliable.
    /// Otherwise this is the best guess, assuming all other transactions will be committed to blocks and eventually finalized.
    /// </summary>
    /// <param name="accountAddress">the base58 check with version byte 1 encoded address (with Bitcoin mapping table).</param>
    /// <returns><see cref="NextAccountNonce"/> - the next account nonce.</returns>
    Task<NextAccountNonce?> GetNextAccountNonceAsync(AccountAddress accountAddress);

    /// <summary>
    /// Retrieves an information about a current state of the consensus layer.
    /// </summary>
    /// <returns><see cref="ConsensusStatus"/> - the consensus status.</returns>
    Task<ConsensusStatus?> GetConsensusStatusAsync();

    /// <summary>
    /// Retrieves an information about a particular block with various details.
    /// </summary>
    /// <param name="blockHash">the base16 encoded hash of a block (64 characters).</param>
    /// <returns><see cref="BlockInfo"/> - details about a particular block.</returns>
    Task<BlockInfo?> GetBlockInfoAsync(BlockHash blockHash);

    /// <summary>
    /// Returns the a list of the given block hash and the hashes of its ancestors going back the given number of generations.
    /// The length of the list will be the given number, or the list will be the entire chain going back from the given block
    /// until the closest genesis or regenesis block.
    /// If the block is not live or finalized, the function returns null.
    /// </summary>
    /// <param name="amount">the number of hash and hashes of its ancestors generations.</param>
    /// <param name="blockHash">the base16 encoded hash of a block (64 characters).</param>
    /// <returns><see cref="List{BlockHash}"/> - the list of the given block hash and the hashes of its ancestors.</returns>
    Task<List<BlockHash>> GetAncestorsAsync(ulong amount, BlockHash blockHash);

    /// <summary>
    /// Sends any account transaction.
    /// </summary>
    /// <param name="payload">the binary encoding of the transaction (details <a href="https://github.com/Concordium/concordium-node/blob/main/docs/grpc-for-smart-contracts.md#sendtransaction">here</a>).</param>
    /// <param name="networkId">the id for the network. Default id is 100.</param>
    /// <returns><see cref="bool"/> - true or false depending on if transaction was successfully sent.</returns>
    Task<bool> SendTransactionAsync(byte[] payload, uint networkId = 100);
}
