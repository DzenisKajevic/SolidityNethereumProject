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
using Nethereum tutorial.Contracts.documentStore.ContractDefinition;

namespace Nethereum tutorial.Contracts.documentStore
{
    public partial class DocumentStoreService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, DocumentStoreDeployment documentStoreDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<DocumentStoreDeployment>().SendRequestAndWaitForReceiptAsync(documentStoreDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, DocumentStoreDeployment documentStoreDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<DocumentStoreDeployment>().SendRequestAsync(documentStoreDeployment);
        }

        public static async Task<DocumentStoreService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, DocumentStoreDeployment documentStoreDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, documentStoreDeployment, cancellationTokenSource);
            return new DocumentStoreService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public DocumentStoreService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> StoreDocumentRequestAsync(StoreDocumentFunction storeDocumentFunction)
        {
             return ContractHandler.SendRequestAsync(storeDocumentFunction);
        }

        public Task<TransactionReceipt> StoreDocumentRequestAndWaitForReceiptAsync(StoreDocumentFunction storeDocumentFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(storeDocumentFunction, cancellationToken);
        }

        public Task<string> StoreDocumentRequestAsync(byte[] key, string name, string description)
        {
            var storeDocumentFunction = new StoreDocumentFunction();
                storeDocumentFunction.Key = key;
                storeDocumentFunction.Name = name;
                storeDocumentFunction.Description = description;
            
             return ContractHandler.SendRequestAsync(storeDocumentFunction);
        }

        public Task<TransactionReceipt> StoreDocumentRequestAndWaitForReceiptAsync(byte[] key, string name, string description, CancellationTokenSource cancellationToken = null)
        {
            var storeDocumentFunction = new StoreDocumentFunction();
                storeDocumentFunction.Key = key;
                storeDocumentFunction.Name = name;
                storeDocumentFunction.Description = description;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(storeDocumentFunction, cancellationToken);
        }

        public Task<DocumentsOutputDTO> DocumentsQueryAsync(DocumentsFunction documentsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<DocumentsFunction, DocumentsOutputDTO>(documentsFunction, blockParameter);
        }

        public Task<DocumentsOutputDTO> DocumentsQueryAsync(byte[] returnValue1, BigInteger returnValue2, BlockParameter blockParameter = null)
        {
            var documentsFunction = new DocumentsFunction();
                documentsFunction.ReturnValue1 = returnValue1;
                documentsFunction.ReturnValue2 = returnValue2;
            
            return ContractHandler.QueryDeserializingToObjectAsync<DocumentsFunction, DocumentsOutputDTO>(documentsFunction, blockParameter);
        }
    }
}
