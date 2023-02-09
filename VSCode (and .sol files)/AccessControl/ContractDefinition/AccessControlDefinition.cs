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

namespace NethereumProject.Contracts.AccessControl.ContractDefinition
{


    public partial class AccessControlDeployment : AccessControlDeploymentBase
    {
        public AccessControlDeployment() : base(BYTECODE) { }
        public AccessControlDeployment(string byteCode) : base(byteCode) { }
    }

    public class AccessControlDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b506040516420a226a4a760d91b602082015261004b90602501604051602081830303815290604052805190602001203361005060201b60201c565b6100a9565b6000828152602081815260408083206001600160a01b0385168085529252808320805460ff1916600117905551909184917f5a06360d65acf95e98445dc834f205063424c636e65418d928cdfabc33a953999190a35050565b6105af806100b86000396000f3fe608060405234801561001057600080fd5b50600436106100625760003560e01c80632a0acc6a146100675780632f2ff15d146100aa57806391d14854146100bf578063d547741f14610106578063ef0b236814610119578063f8fc08b914610139575b600080fd5b6100976040516420a226a4a760d91b60208201526025016040516020818303038152906040528051906020012081565b6040519081526020015b60405180910390f35b6100bd6100b836600461047d565b610164565b005b6100f66100cd36600461047d565b6000918252602082815260408084206001600160a01b0393909316845291905290205460ff1690565b60405190151581526020016100a1565b6100bd61011436600461047d565b610207565b61012c6101273660046104b9565b6102ed565b6040516100a191906104d2565b6100f661014736600461047d565b600060208181529281526040808220909352908152205460ff1681565b6040516420a226a4a760d91b602082015260250160408051601f198184030181529181528151602092830120600081815280845282812033825290935291205460ff166101f85760405162461bcd60e51b815260206004820152601e60248201527f556e617574686f72697a65643a20526f6c65206e6f742070726573656e74000060448201526064015b60405180910390fd5b6102028383610424565b505050565b6040516420a226a4a760d91b602082015260250160408051601f198184030181529181528151602092830120600081815280845282812033825290935291205460ff166102965760405162461bcd60e51b815260206004820152601e60248201527f556e617574686f72697a65643a20526f6c65206e6f742070726573656e74000060448201526064016101ef565b6000838152602081815260408083206001600160a01b0386168085529252808320805460ff1916905551909185917f76e6093c136cd7faa5a6d92b2b633f3b4595abd4a529b7a13917398355fea6949190a3505050565b606060005b60208160ff161080156103265750828160ff166020811061031557610315610520565b1a60f81b6001600160f81b03191615155b1561033d578061033581610536565b9150506102f2565b60008160ff1667ffffffffffffffff81111561035b5761035b610563565b6040519080825280601f01601f191660200182016040528015610385576020820181803683370190505b509050600091505b60208260ff161080156103c15750838260ff16602081106103b0576103b0610520565b1a60f81b6001600160f81b03191615155b1561041d57838260ff16602081106103db576103db610520565b1a60f81b818360ff16815181106103f4576103f4610520565b60200101906001600160f81b031916908160001a9053508161041581610536565b92505061038d565b9392505050565b6000828152602081815260408083206001600160a01b0385168085529252808320805460ff1916600117905551909184917f5a06360d65acf95e98445dc834f205063424c636e65418d928cdfabc33a953999190a35050565b6000806040838503121561049057600080fd5b8235915060208301356001600160a01b03811681146104ae57600080fd5b809150509250929050565b6000602082840312156104cb57600080fd5b5035919050565b600060208083528351808285015260005b818110156104ff578581018301518582016040015282016104e3565b506000604082860101526040601f19601f8301168501019250505092915050565b634e487b7160e01b600052603260045260246000fd5b600060ff821660ff810361055a57634e487b7160e01b600052601160045260246000fd5b60010192915050565b634e487b7160e01b600052604160045260246000fdfea2646970667358221220e136d8550e5521f44e14f343a3cf1f7b2044dd6b003b4b69489e8e01b8ca6be964736f6c63430008110033";
        public AccessControlDeploymentBase() : base(BYTECODE) { }
        public AccessControlDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class ADMINFunction : ADMINFunctionBase { }

    [Function("ADMIN", "bytes32")]
    public class ADMINFunctionBase : FunctionMessage
    {

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
        [Parameter("bytes32", "role", 1, true )]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2, true )]
        public virtual string Account { get; set; }
    }

    public partial class RevokeRoleEventDTO : RevokeRoleEventDTOBase { }

    [Event("RevokeRole")]
    public class RevokeRoleEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "role", 1, true )]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2, true )]
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



    public partial class RolesOutputDTO : RolesOutputDTOBase { }

    [FunctionOutput]
    public class RolesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}
