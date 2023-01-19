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

namespace NethereumProject.Contracts.Contracts.Leaderboard.ContractDefinition
{


    public partial class LeaderboardDeployment : LeaderboardDeploymentBase
    {
        public LeaderboardDeployment() : base(BYTECODE) { }
        public LeaderboardDeployment(string byteCode) : base(byteCode) { }
    }

    public class LeaderboardDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6080604052600560025534801561001557600080fd5b506040516420a226a4a760d91b602082015261005090602501604051602081830303815290604052805190602001203361006760201b60201c565b600180546001600160a01b031916331790556100c0565b6000828152602081815260408083206001600160a01b0385168085529252808320805460ff1916600117905551909184917f5a06360d65acf95e98445dc834f205063424c636e65418d928cdfabc33a953999190a35050565b610937806100cf6000396000f3fe608060405234801561001057600080fd5b50600436106100885760003560e01c8063d119db4c1161005b578063d119db4c1461015a578063d547741f1461016d578063ef0b236814610180578063f8fc08b9146101a057600080fd5b80632a0acc6a1461008d5780632f2ff15d146100d057806391d14854146100e5578063bf36839914610108575b600080fd5b6100bd6040516420a226a4a760d91b60208201526025016040516020818303038152906040528051906020012081565b6040519081526020015b60405180910390f35b6100e36100de36600461071f565b6101cb565b005b6100f86100f336600461071f565b61028f565b60405190151581526020016100c7565b61013b61011636600461074b565b600360205260009081526040902080546001909101546001600160a01b039091169082565b604080516001600160a01b0390931683526020830191909152016100c7565b6100f8610168366004610764565b6102ba565b6100e361017b36600461071f565b6104cf565b61019361018e36600461074b565b61059c565b6040516100c791906107b2565b6100f86101ae36600461071f565b600060208181529281526040808220909352908152205460ff1681565b6040516420a226a4a760d91b602082015260250160408051601f198184030181529181528151602092830120600081815280845282812033825290935291205460ff166102335760405162461bcd60e51b815260040161022a906107e5565b60405180910390fd5b61023d813361028f565b6102805761024a8161059c565b60405160200161025a919061081c565b60408051601f198184030181529082905262461bcd60e51b825261022a916004016107b2565b61028a83836106aa565b505050565b6000828152602081815260408083206001600160a01b038516845290915290205460ff165b92915050565b6040516420a226a4a760d91b602082015260009060250160408051601f198184030181529181528151602092830120600081815280845282812033825290935291205460ff1661031c5760405162461bcd60e51b815260040161022a906107e5565b610326813361028f565b6103335761024a8161059c565b826003600060016002546103479190610877565b8152602001908152602001600020600101541061036757600091506104c8565b60005b6002548110156104c6576000818152600360205260409020600101548411156104b4576000818152600360209081526040808320815180830190925280546001600160a01b031682526001908101549282019290925291906103cd90849061088a565b90505b6002546103de90600161088a565b81101561044e576000818152600360208181526040808420815180830190925280546001600160a01b038082168452600183018054858701529688905294845287516001600160a01b031990911694169390931790925593909301519055806104468161089d565b9150506103d0565b50506040805180820182526001600160a01b0380881682526020808301888152600095865260039091528385209251835492166001600160a01b0319928316178355516001928301556002548452918320805490921682559081019190915591506104c8565b806104be8161089d565b91505061036a565b505b5092915050565b6040516420a226a4a760d91b602082015260250160408051601f198184030181529181528151602092830120600081815280845282812033825290935291205460ff1661052e5760405162461bcd60e51b815260040161022a906107e5565b610538813361028f565b6105455761024a8161059c565b6000838152602081815260408083206001600160a01b0386168085529252808320805460ff1916905551909185917f76e6093c136cd7faa5a6d92b2b633f3b4595abd4a529b7a13917398355fea6949190a3505050565b60606000805b60208260ff161080156105d65750838260ff16602081106105c5576105c56108b6565b1a60f81b6001600160f81b03191615155b156105ed57816105e5816108cc565b9250506105a2565b60008260ff1667ffffffffffffffff81111561060b5761060b6108eb565b6040519080825280601f01601f191660200182016040528015610635576020820181803683370190505b509050600091505b8260ff168260ff1610156106a257848260ff1660208110610660576106606108b6565b1a60f81b818360ff1681518110610679576106796108b6565b60200101906001600160f81b031916908160001a9053508161069a816108cc565b92505061063d565b949350505050565b6000828152602081815260408083206001600160a01b0385168085529252808320805460ff1916600117905551909184917f5a06360d65acf95e98445dc834f205063424c636e65418d928cdfabc33a953999190a35050565b80356001600160a01b038116811461071a57600080fd5b919050565b6000806040838503121561073257600080fd5b8235915061074260208401610703565b90509250929050565b60006020828403121561075d57600080fd5b5035919050565b6000806040838503121561077757600080fd5b61078083610703565b946020939093013593505050565b60005b838110156107a9578181015183820152602001610791565b50506000910152565b60208152600082518060208401526107d181604085016020870161078e565b601f01601f19169190910160400192915050565b6020808252601e908201527f556e617574686f72697a65643a20526f6c65206e6f742070726573656e740000604082015260600190565b7f416363657373436f6e74726f6c3a206d697373696e6720726f6c65000000000081526000825161085481601b85016020870161078e565b91909101601b0192915050565b634e487b7160e01b600052601160045260246000fd5b818103818111156102b4576102b4610861565b808201808211156102b4576102b4610861565b6000600182016108af576108af610861565b5060010190565b634e487b7160e01b600052603260045260246000fd5b600060ff821660ff81036108e2576108e2610861565b60010192915050565b634e487b7160e01b600052604160045260246000fdfea264697066735822122029b731448879d1dace0f45feca0d22a283236fb92b464a7387f523b8b7831f9664736f6c63430008110033";
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
