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

namespace Nethereum tutorial.Contracts.documentStore.ContractDefinition
{


    public partial class DocumentStoreDeployment : DocumentStoreDeploymentBase
    {
        public DocumentStoreDeployment() : base(BYTECODE) { }
        public DocumentStoreDeployment(string byteCode) : base(byteCode) { }
    }

    public class DocumentStoreDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x";
        public DocumentStoreDeploymentBase() : base(BYTECODE) { }
        public DocumentStoreDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class StoreDocumentFunction : StoreDocumentFunctionBase { }

    [Function("StoreDocument", "bool")]
    public class StoreDocumentFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
        [Parameter("string", "name", 2)]
        public virtual string Name { get; set; }
        [Parameter("string", "description", 3)]
        public virtual string Description { get; set; }
    }

    public partial class DocumentsFunction : DocumentsFunctionBase { }

    [Function("documents", typeof(DocumentsOutputDTO))]
    public class DocumentsFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
        [Parameter("uint256", "", 2)]
        public virtual BigInteger ReturnValue2 { get; set; }
    }



    public partial class DocumentsOutputDTO : DocumentsOutputDTOBase { }

    [FunctionOutput]
    public class DocumentsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "name", 1)]
        public virtual string Name { get; set; }
        [Parameter("string", "description", 2)]
        public virtual string Description { get; set; }
        [Parameter("address", "sender", 3)]
        public virtual string Sender { get; set; }
    }
}
