using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace NethereumProject.Contracts.Leaderboard.ContractDefinition
{


    public partial class LeaderboardDeployment : LeaderboardDeploymentBase
    {
        public LeaderboardDeployment() : base(BYTECODE) { }
        public LeaderboardDeployment(string byteCode) : base(byteCode) { }
    }

    public class LeaderboardDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040526001805461ffff60a01b1916600560a01b17905534801561002457600080fd5b506040516420a226a4a760d91b602082015261005f90602501604051602081830303815290604052805190602001203361007660201b60201c565b600180546001600160a01b031916331790556100cf565b6000828152602081815260408083206001600160a01b0385168085529252808320805460ff1916600117905551909184917f5a06360d65acf95e98445dc834f205063424c636e65418d928cdfabc33a953999190a35050565b610caa806100de6000396000f3fe608060405234801561001057600080fd5b506004361061009e5760003560e01c8063bf36839911610066578063bf36839914610153578063d119db4c146101a5578063d547741f146101b8578063ef0b2368146101cb578063f8fc08b9146101eb57600080fd5b80632a0acc6a146100a35780632f2ff15d146100e65780636d763a6e146100fb57806391d1485414610111578063ab48660714610134575b600080fd5b6100d36040516420a226a4a760d91b60208201526025016040516020818303038152906040528051906020012081565b6040519081526020015b60405180910390f35b6100f96100f43660046109ef565b610216565b005b6101036102da565b6040516100dd929190610a1b565b61012461011f3660046109ef565b61045a565b60405190151581526020016100dd565b600154600160a81b900460ff1660405160ff90911681526020016100dd565b610186610161366004610a9f565b600260205260009081526040902080546001909101546001600160a01b039091169082565b604080516001600160a01b0390931683526020830191909152016100dd565b6101246101b3366004610ab8565b610485565b6100f96101c63660046109ef565b610776565b6101de6101d9366004610a9f565b610843565b6040516100dd9190610b06565b6101246101f93660046109ef565b600060208181529281526040808220909352908152205460ff1681565b6040516420a226a4a760d91b602082015260250160408051601f198184030181529181528151602092830120600081815280845282812033825290935291205460ff1661027e5760405162461bcd60e51b815260040161027590610b39565b60405180910390fd5b610288813361045a565b6102cb5761029581610843565b6040516020016102a59190610b70565b60408051601f198184030181529082905262461bcd60e51b825261027591600401610b06565b6102d5838361097a565b505050565b6001546060908190600160a81b900460ff166000036102fc5750606091829150565b600154600090600160a81b900460ff1667ffffffffffffffff81111561032457610324610bb5565b60405190808252806020026020018201604052801561034d578160200160208202803683370190505b50600154909150600090600160a81b900460ff1667ffffffffffffffff81111561037957610379610bb5565b6040519080825280602002602001820160405280156103a2578160200160208202803683370190505b50905060005b60015460ff600160a81b909104811690821610156104505760ff811660008181526002602052604090205484516001600160a01b0390911691859181106103f1576103f1610bcb565b6001600160a01b0390921660209283029190910182015260ff821660008181526002909252604090912060010154835190918491811061043357610433610bcb565b60209081029190910101528061044881610bf7565b9150506103a8565b5090939092509050565b6000828152602081815260408083206001600160a01b038516845290915290205460ff165b92915050565b6040516420a226a4a760d91b602082015260009060250160408051601f198184030181529181528151602092830120600081815280845282812033825290935291205460ff166104e75760405162461bcd60e51b815260040161027590610b39565b6104f1813361045a565b6104fe5761029581610843565b6001546005600160a81b90910460ff1610156105a7576040805180820182526001600160a01b038681168252602080830187815260018054600160a81b9081900460ff9081166000908152600290955296909320945185546001600160a01b0319169416939093178455519282019290925580549190910490911690601561058583610bf7565b91906101000a81548160ff021916908360ff160217905550506001915061076f565b6001805484916002916000916105c691600160a01b900460ff16610c16565b60ff16815260200190815260200160002060010154106105e9576000915061076f565b60005b600154600160a01b900460ff1681101561076d5760008181526002602052604090206001015484111561075b576000818152600260209081526040808320815180830190925280546001600160a01b03168252600190810154928201929092529190610659908490610c2f565b90505b600160149054906101000a900460ff1660016106789190610c42565b60ff168110156106eb576000818152600260208181526040808420815180830190925280546001600160a01b038082168452600183018054858701529688905294845287516001600160a01b031990911694169390931790925593909301519055806106e381610c5b565b91505061065c565b50506040805180820182526001600160a01b0380881682526020808301888152600095865260029091528385209251835492166001600160a01b031992831617835551600192830155815460ff600160a01b9091041684529183208054909216825590810191909155915061076f565b8061076581610c5b565b9150506105ec565b505b5092915050565b6040516420a226a4a760d91b602082015260250160408051601f198184030181529181528151602092830120600081815280845282812033825290935291205460ff166107d55760405162461bcd60e51b815260040161027590610b39565b6107df813361045a565b6107ec5761029581610843565b6000838152602081815260408083206001600160a01b0386168085529252808320805460ff1916905551909185917f76e6093c136cd7faa5a6d92b2b633f3b4595abd4a529b7a13917398355fea6949190a3505050565b606060005b60208160ff1610801561087c5750828160ff166020811061086b5761086b610bcb565b1a60f81b6001600160f81b03191615155b15610893578061088b81610bf7565b915050610848565b60008160ff1667ffffffffffffffff8111156108b1576108b1610bb5565b6040519080825280601f01601f1916602001820160405280156108db576020820181803683370190505b509050600091505b60208260ff161080156109175750838260ff166020811061090657610906610bcb565b1a60f81b6001600160f81b03191615155b1561097357838260ff166020811061093157610931610bcb565b1a60f81b818360ff168151811061094a5761094a610bcb565b60200101906001600160f81b031916908160001a9053508161096b81610bf7565b9250506108e3565b9392505050565b6000828152602081815260408083206001600160a01b0385168085529252808320805460ff1916600117905551909184917f5a06360d65acf95e98445dc834f205063424c636e65418d928cdfabc33a953999190a35050565b80356001600160a01b03811681146109ea57600080fd5b919050565b60008060408385031215610a0257600080fd5b82359150610a12602084016109d3565b90509250929050565b604080825283519082018190526000906020906060840190828701845b82811015610a5d5781516001600160a01b031684529284019290840190600101610a38565b5050508381038285015284518082528583019183019060005b81811015610a9257835183529284019291840191600101610a76565b5090979650505050505050565b600060208284031215610ab157600080fd5b5035919050565b60008060408385031215610acb57600080fd5b610ad4836109d3565b946020939093013593505050565b60005b83811015610afd578181015183820152602001610ae5565b50506000910152565b6020815260008251806020840152610b25816040850160208701610ae2565b601f01601f19169190910160400192915050565b6020808252601e908201527f556e617574686f72697a65643a20526f6c65206e6f742070726573656e740000604082015260600190565b7f416363657373436f6e74726f6c3a206d697373696e6720726f6c650000000000815260008251610ba881601b850160208701610ae2565b91909101601b0192915050565b634e487b7160e01b600052604160045260246000fd5b634e487b7160e01b600052603260045260246000fd5b634e487b7160e01b600052601160045260246000fd5b600060ff821660ff8103610c0d57610c0d610be1565b60010192915050565b60ff828116828216039081111561047f5761047f610be1565b8082018082111561047f5761047f610be1565b60ff818116838216019081111561047f5761047f610be1565b600060018201610c6d57610c6d610be1565b506001019056fea26469706673582212209cec2520c79a72ed474b6a339765bbf1cf0241a1e5c22bdb244aad9b6791c8e064736f6c63430008110033";
        public LeaderboardDeploymentBase() : base(BYTECODE) { }
        public LeaderboardDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class ADMINFunction : ADMINFunctionBase { }

    [Function("ADMIN", "bytes32")]
    public class ADMINFunctionBase : FunctionMessage
    {

    }

    public partial class AddScoreFunction : AddScoreFunctionBase { }

    [Function("addScore", "bool")]
    public class AddScoreFunctionBase : FunctionMessage
    {
        [Parameter("address", "scoreHolderAddress", 1)]
        public virtual string ScoreHolderAddress { get; set; }
        [Parameter("uint256", "score", 2)]
        public virtual BigInteger Score { get; set; }
    }

    public partial class Bytes32ToStrFunction : Bytes32ToStrFunctionBase { }

    [Function("bytes32ToStr", "string")]
    public class Bytes32ToStrFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "_bytes32", 1)]
        public virtual byte[] Bytes32 { get; set; }
    }

    public partial class GetLeaderboardFunction : GetLeaderboardFunctionBase { }

    [Function("getLeaderboard", typeof(GetLeaderboardOutputDTO))]
    public class GetLeaderboardFunctionBase : FunctionMessage
    {

    }

    public partial class GetLeaderboardLengthFunction : GetLeaderboardLengthFunctionBase { }

    [Function("getLeaderboardLength", "uint8")]
    public class GetLeaderboardLengthFunctionBase : FunctionMessage
    {

    }

    public partial class GrantRoleFunction : GrantRoleFunctionBase { }

    [Function("grantRole")]
    public class GrantRoleFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "_role", 1)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "_account", 2)]
        public virtual string Account { get; set; }
    }

    public partial class HasRoleFunction : HasRoleFunctionBase { }

    [Function("hasRole", "bool")]
    public class HasRoleFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "_role", 1)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "_account", 2)]
        public virtual string Account { get; set; }
    }

    public partial class LeaderboardFunction : LeaderboardFunctionBase { }

    [Function("leaderboard", typeof(LeaderboardOutputDTO))]
    public class LeaderboardFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class RevokeRoleFunction : RevokeRoleFunctionBase { }

    [Function("revokeRole")]
    public class RevokeRoleFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "_role", 1)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "_account", 2)]
        public virtual string Account { get; set; }
    }

    public partial class RolesFunction : RolesFunctionBase { }

    [Function("roles", "bool")]
    public class RolesFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
        [Parameter("address", "", 2)]
        public virtual string ReturnValue2 { get; set; }
    }

    public partial class GrantRoleEventDTO : GrantRoleEventDTOBase { }

    [Event("GrantRole")]
    public class GrantRoleEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "role", 1, true)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2, true)]
        public virtual string Account { get; set; }
    }

    public partial class RevokeRoleEventDTO : RevokeRoleEventDTOBase { }

    [Event("RevokeRole")]
    public class RevokeRoleEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "role", 1, true)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2, true)]
        public virtual string Account { get; set; }
    }

    public partial class ADMINOutputDTO : ADMINOutputDTOBase { }

    [FunctionOutput]
    public class ADMINOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }



    public partial class Bytes32ToStrOutputDTO : Bytes32ToStrOutputDTOBase { }

    [FunctionOutput]
    public class Bytes32ToStrOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("string", "resultingString", 1)]
        public virtual string ResultingString { get; set; }
    }

    public partial class GetLeaderboardOutputDTO : GetLeaderboardOutputDTOBase { }

    [FunctionOutput]
    public class GetLeaderboardOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
        [Parameter("uint256[]", "", 2)]
        public virtual List<BigInteger> ReturnValue2 { get; set; }
    }

    public partial class GetLeaderboardLengthOutputDTO : GetLeaderboardLengthOutputDTOBase { }

    [FunctionOutput]
    public class GetLeaderboardLengthOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }



    public partial class HasRoleOutputDTO : HasRoleOutputDTOBase { }

    [FunctionOutput]
    public class HasRoleOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class LeaderboardOutputDTO : LeaderboardOutputDTOBase { }

    [FunctionOutput]
    public class LeaderboardOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "scoreHolder", 1)]
        public virtual string ScoreHolder { get; set; }
        [Parameter("uint256", "score", 2)]
        public virtual BigInteger Score { get; set; }
    }



    public partial class RolesOutputDTO : RolesOutputDTOBase { }

    [FunctionOutput]
    public class RolesOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}
