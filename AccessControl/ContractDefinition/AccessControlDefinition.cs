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
        public static string BYTECODE = "608060405234801561001057600080fd5b506040516420a226a4a760d91b602082015261004b90602501604051602081830303815290604052805190602001203361005060201b60201c565b6100a9565b6000828152602081815260408083206001600160a01b0385168085529252808320805460ff1916600117905551909184917f5a06360d65acf95e98445dc834f205063424c636e65418d928cdfabc33a953999190a35050565b61063d806100b86000396000f3fe608060405234801561001057600080fd5b50600436106100625760003560e01c80632a0acc6a146100675780632f2ff15d146100aa57806391d14854146100bf578063d547741f146100e2578063ef0b2368146100f5578063f8fc08b914610115575b600080fd5b6100976040516420a226a4a760d91b60208201526025016040516020818303038152906040528051906020012081565b6040519081526020015b60405180910390f35b6100bd6100b83660046104bd565b610140565b005b6100d26100cd3660046104bd565b610230565b60405190151581526020016100a1565b6100bd6100f03660046104bd565b610259565b6101086101033660046104f9565b610356565b6040516100a19190610536565b6100d26101233660046104bd565b600060208181529281526040808220909352908152205460ff1681565b6040516420a226a4a760d91b602082015260250160408051601f198184030181529181528151602092830120600081815280845282812033825290935291205460ff166101d45760405162461bcd60e51b815260206004820152601e60248201527f556e617574686f72697a65643a20526f6c65206e6f742070726573656e74000060448201526064015b60405180910390fd5b6101de8133610230565b610221576101eb81610356565b6040516020016101fb9190610569565b60408051601f198184030181529082905262461bcd60e51b82526101cb91600401610536565b61022b8383610464565b505050565b6000918252602082815260408084206001600160a01b0393909316845291905290205460ff1690565b6040516420a226a4a760d91b602082015260250160408051601f198184030181529181528151602092830120600081815280845282812033825290935291205460ff166102e85760405162461bcd60e51b815260206004820152601e60248201527f556e617574686f72697a65643a20526f6c65206e6f742070726573656e74000060448201526064016101cb565b6102f28133610230565b6102ff576101eb81610356565b6000838152602081815260408083206001600160a01b0386168085529252808320805460ff1916905551909185917f76e6093c136cd7faa5a6d92b2b633f3b4595abd4a529b7a13917398355fea6949190a3505050565b60606000805b60208260ff161080156103905750838260ff166020811061037f5761037f6105ae565b1a60f81b6001600160f81b03191615155b156103a7578161039f816105c4565b92505061035c565b60008260ff1667ffffffffffffffff8111156103c5576103c56105f1565b6040519080825280601f01601f1916602001820160405280156103ef576020820181803683370190505b509050600091505b8260ff168260ff16101561045c57848260ff166020811061041a5761041a6105ae565b1a60f81b818360ff1681518110610433576104336105ae565b60200101906001600160f81b031916908160001a90535081610454816105c4565b9250506103f7565b949350505050565b6000828152602081815260408083206001600160a01b0385168085529252808320805460ff1916600117905551909184917f5a06360d65acf95e98445dc834f205063424c636e65418d928cdfabc33a953999190a35050565b600080604083850312156104d057600080fd5b8235915060208301356001600160a01b03811681146104ee57600080fd5b809150509250929050565b60006020828403121561050b57600080fd5b5035919050565b60005b8381101561052d578181015183820152602001610515565b50506000910152565b6020815260008251806020840152610555816040850160208701610512565b601f01601f19169190910160400192915050565b7f416363657373436f6e74726f6c3a206d697373696e6720726f6c6500000000008152600082516105a181601b850160208701610512565b91909101601b0192915050565b634e487b7160e01b600052603260045260246000fd5b600060ff821660ff81036105e857634e487b7160e01b600052601160045260246000fd5b60010192915050565b634e487b7160e01b600052604160045260246000fdfea264697066735822122066d448f98e0ab1e00df1fe0f69423ff0cbd64d469a48a9db50e60d28106cb5bd64736f6c63430008110033";
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



    public partial class RolesOutputDTO : RolesOutputDTOBase { }

    [FunctionOutput]
    public class RolesOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}
