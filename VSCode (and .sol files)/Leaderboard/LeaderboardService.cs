using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Nethereum tutorial.Contracts.Leaderboard.ContractDefinition;

namespace Nethereum tutorial.Contracts.Leaderboard
{
    public partial class LeaderboardService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, LeaderboardDeployment leaderboardDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<LeaderboardDeployment>().SendRequestAndWaitForReceiptAsync(leaderboardDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, LeaderboardDeployment leaderboardDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<LeaderboardDeployment>().SendRequestAsync(leaderboardDeployment);
        }

        public static async Task<LeaderboardService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, LeaderboardDeployment leaderboardDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, leaderboardDeployment, cancellationTokenSource);
            return new LeaderboardService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public LeaderboardService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<byte[]> ADMINQueryAsync(ADMINFunction aDMINFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ADMINFunction, byte[]>(aDMINFunction, blockParameter);
        }

        
        public Task<byte[]> ADMINQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ADMINFunction, byte[]>(null, blockParameter);
        }

        public Task<string> AddScoreRequestAsync(AddScoreFunction addScoreFunction)
        {
             return ContractHandler.SendRequestAsync(addScoreFunction);
        }

        public Task<TransactionReceipt> AddScoreRequestAndWaitForReceiptAsync(AddScoreFunction addScoreFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addScoreFunction, cancellationToken);
        }

        public Task<string> AddScoreRequestAsync(string scoreHolderAddress, BigInteger score)
        {
            var addScoreFunction = new AddScoreFunction();
                addScoreFunction.ScoreHolderAddress = scoreHolderAddress;
                addScoreFunction.Score = score;
            
             return ContractHandler.SendRequestAsync(addScoreFunction);
        }

        public Task<TransactionReceipt> AddScoreRequestAndWaitForReceiptAsync(string scoreHolderAddress, BigInteger score, CancellationTokenSource cancellationToken = null)
        {
            var addScoreFunction = new AddScoreFunction();
                addScoreFunction.ScoreHolderAddress = scoreHolderAddress;
                addScoreFunction.Score = score;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addScoreFunction, cancellationToken);
        }

        public Task<string> Bytes32ToStrQueryAsync(Bytes32ToStrFunction bytes32ToStrFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Bytes32ToStrFunction, string>(bytes32ToStrFunction, blockParameter);
        }

        
        public Task<string> Bytes32ToStrQueryAsync(byte[] bytes32, BlockParameter blockParameter = null)
        {
            var bytes32ToStrFunction = new Bytes32ToStrFunction();
                bytes32ToStrFunction.Bytes32 = bytes32;
            
            return ContractHandler.QueryAsync<Bytes32ToStrFunction, string>(bytes32ToStrFunction, blockParameter);
        }

        public Task<GetLeaderboardOutputDTO> GetLeaderboardQueryAsync(GetLeaderboardFunction getLeaderboardFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetLeaderboardFunction, GetLeaderboardOutputDTO>(getLeaderboardFunction, blockParameter);
        }

        public Task<GetLeaderboardOutputDTO> GetLeaderboardQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetLeaderboardFunction, GetLeaderboardOutputDTO>(null, blockParameter);
        }

        public Task<byte> GetLeaderboardLengthQueryAsync(GetLeaderboardLengthFunction getLeaderboardLengthFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLeaderboardLengthFunction, byte>(getLeaderboardLengthFunction, blockParameter);
        }

        
        public Task<byte> GetLeaderboardLengthQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLeaderboardLengthFunction, byte>(null, blockParameter);
        }

        public Task<string> GrantRoleRequestAsync(GrantRoleFunction grantRoleFunction)
        {
             return ContractHandler.SendRequestAsync(grantRoleFunction);
        }

        public Task<TransactionReceipt> GrantRoleRequestAndWaitForReceiptAsync(GrantRoleFunction grantRoleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grantRoleFunction, cancellationToken);
        }

        public Task<string> GrantRoleRequestAsync(byte[] role, string account)
        {
            var grantRoleFunction = new GrantRoleFunction();
                grantRoleFunction.Role = role;
                grantRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAsync(grantRoleFunction);
        }

        public Task<TransactionReceipt> GrantRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null)
        {
            var grantRoleFunction = new GrantRoleFunction();
                grantRoleFunction.Role = role;
                grantRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grantRoleFunction, cancellationToken);
        }

        public Task<bool> HasRoleQueryAsync(HasRoleFunction hasRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HasRoleFunction, bool>(hasRoleFunction, blockParameter);
        }

        
        public Task<bool> HasRoleQueryAsync(byte[] role, string account, BlockParameter blockParameter = null)
        {
            var hasRoleFunction = new HasRoleFunction();
                hasRoleFunction.Role = role;
                hasRoleFunction.Account = account;
            
            return ContractHandler.QueryAsync<HasRoleFunction, bool>(hasRoleFunction, blockParameter);
        }

        public Task<LeaderboardOutputDTO> LeaderboardQueryAsync(LeaderboardFunction leaderboardFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<LeaderboardFunction, LeaderboardOutputDTO>(leaderboardFunction, blockParameter);
        }

        public Task<LeaderboardOutputDTO> LeaderboardQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var leaderboardFunction = new LeaderboardFunction();
                leaderboardFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<LeaderboardFunction, LeaderboardOutputDTO>(leaderboardFunction, blockParameter);
        }

        public Task<string> RevokeRoleRequestAsync(RevokeRoleFunction revokeRoleFunction)
        {
             return ContractHandler.SendRequestAsync(revokeRoleFunction);
        }

        public Task<TransactionReceipt> RevokeRoleRequestAndWaitForReceiptAsync(RevokeRoleFunction revokeRoleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeRoleFunction, cancellationToken);
        }

        public Task<string> RevokeRoleRequestAsync(byte[] role, string account)
        {
            var revokeRoleFunction = new RevokeRoleFunction();
                revokeRoleFunction.Role = role;
                revokeRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAsync(revokeRoleFunction);
        }

        public Task<TransactionReceipt> RevokeRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null)
        {
            var revokeRoleFunction = new RevokeRoleFunction();
                revokeRoleFunction.Role = role;
                revokeRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeRoleFunction, cancellationToken);
        }

        public Task<bool> RolesQueryAsync(RolesFunction rolesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RolesFunction, bool>(rolesFunction, blockParameter);
        }

        
        public Task<bool> RolesQueryAsync(byte[] returnValue1, string returnValue2, BlockParameter blockParameter = null)
        {
            var rolesFunction = new RolesFunction();
                rolesFunction.ReturnValue1 = returnValue1;
                rolesFunction.ReturnValue2 = returnValue2;
            
            return ContractHandler.QueryAsync<RolesFunction, bool>(rolesFunction, blockParameter);
        }
    }
}
