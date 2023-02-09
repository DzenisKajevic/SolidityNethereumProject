using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.NonceServices;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum_Project
{
    class MainClass
    {
        private static readonly HexBigInteger GAS_LIMIT = new HexBigInteger(4600000);
        class ContractService
        {
            private readonly Contract contract;
            private readonly Account account;
            private readonly Web3 web3;

            public ContractService(string provider, string contractAddress, string abi, Account account, Web3 web3)
            {
                this.account = account;
                this.contract = web3.Eth.GetContract(abi, contractAddress);
                this.web3 = web3;
            }

            //        public async Task<Nethereum.RPC.Eth.DTOs.TransactionReceipt> Multiply(int val)
            public async Task<int> Multiply(int val)

            {
                /* // params for SendTransactionAsync
                The value parameter refers to the amount of Ether you want to send to the contract with that transaction.

                When sending a transaction there are common parameters:

                To (the address where you sending the transaction, in this case, the contract address which is automatically set)
                From (the address from)
                Gas (the total amount of gas you want to spend, or gas limit)
                Gas Price (gas price)
                Value (the amount of ether (in Wei) you want to send, this can be to an account or a contract, in your scenario, you will be sending it to a contract. 
                Your function in solidity should be able to access it using msg.value)
                Data (in your scenario, this is the function and parameters encoded)
                 */
                try
                {
                    var multiplyFunction = contract.GetFunction("multiplyWithoutEvent");
                    var result = await multiplyFunction.CallAsync<int>(val);
                    //var receipt = await multiplyFunction.SendTransactionAndWaitForReceiptAsync(account.Address, GAS_LIMIT, new HexBigInteger(0), null, val);
                    //Console.WriteLine(receipt.BlockHash);
                    //var txHash = multiplyFunction.SendTransactionAsync(account.Address, GAS_LIMIT, new HexBigInteger(0), val);
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return -1;
            }

            public async Task MultipliedEvent()
            {
                var multiplyFunction = contract.GetFunction("multiplyWithEvent");
                var multiplyEvent = contract.GetEvent("multiplied");

                var filterAll = await multiplyEvent.CreateFilterAsync();
                var filter5 = await multiplyEvent.CreateFilterAsync(5);

                var receipt = await multiplyFunction.SendTransactionAndWaitForReceiptAsync(account.Address, GAS_LIMIT, new HexBigInteger(0), null, 4);
                Console.WriteLine("Second transaction");
                //var receipt5 = await multiplyFunction.SendTransactionAndWaitForReceiptAsync(account.Address, GAS_LIMIT, new HexBigInteger(0), null, 5);

                // comment 3 lines below, uncomment 3 above

                //var transactionHash = await multiplyFunction.SendTransactionAsync(account.Address, GAS_LIMIT, GAS_LIMIT, 4); // await multiplyFunction.SendTransactionAsync(account.Address, 4);
                //transactionHash = await multiplyFunction.SendTransactionAsync(account.Address, GAS_LIMIT, GAS_LIMIT,  5);
                //var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
                Console.WriteLine(receipt.GasUsed);

                //System.Threading.Thread.Sleep(20000);
                // use the generic method to be able to decode it (<MultipliedEvent>)
                // if we have complex output, we can create "function output" or a DTO
                var allLogs = multiplyEvent.GetAllChangesAsync<MultipliedEventClass>(filterAll).Result;
                var log = multiplyEvent.GetFilterChangesAsync<MultipliedEventClass>(filterAll).Result;
                var log5 = multiplyEvent.GetFilterChangesAsync<MultipliedEventClass>(filter5).Result;

                Console.WriteLine("allLog: " + allLogs.Count);
                Console.WriteLine("log count: " + log.Count);
                Console.WriteLine("log5 count: " + log5.Count);
                Console.WriteLine(log5[0]);
                //            Console.WriteLine("log first parameter (input): " + log[0].Event;
                //Console.WriteLine("log5 result: " + log5[0].Event.result);
                //Console.WriteLine("complete log" + log);
            }

            public async Task functionDTOs()
            {
                var storeFunction = contract.GetFunction("StoreDocument");
                var documentsFunctionMap = contract.GetFunction("documents");

                var estimateGasStore = await storeFunction.EstimateGasAsync("key1", "hello", "description1");
                Console.WriteLine("estimate gas for storing:" + estimateGasStore);

                Console.WriteLine("Calling function 1");
                var transactionReceipt = await storeFunction.SendTransactionAndWaitForReceiptAsync(account.Address, GAS_LIMIT, estimateGasStore, null, "key1", "hello", "description1");

                Console.WriteLine("Calling function 1");
                transactionReceipt = await storeFunction.SendTransactionAndWaitForReceiptAsync(account.Address, GAS_LIMIT, estimateGasStore, null, "key1", "hello2", "description2");

                Console.WriteLine("Calling function 3");
                var result1 = documentsFunctionMap.CallDeserializingToObjectAsync<Document>("key0", 0);

                Console.WriteLine("Calling function 4");
                var result2 = documentsFunctionMap.CallDeserializingToObjectAsync<Document>("key0", 1);

                Console.WriteLine("result1: " + result1);
                Console.WriteLine("result2: " + result2);

            }

            /*
            struct Document{
            string name;
            string description;
            address sender;
            }
             */
            [FunctionOutput]
            public class Document
            {
                [Parameter("string", "name", 1)]
                public string Name { get; set; }

                [Parameter("string", "description", 2)]
                public string Description { get; set; }

                [Parameter("address", "sender", 3)]
                public string Sender { get; set; }
            }

            // class that can be decoded as the event output
            // event multiplied(int256 indexed val, address indexed sender, int256 result);
            [Event("multiplied")]
            public class MultipliedEventClass
            {
                [Parameter("int", "val", 1, true)]
                public int val { get; set; }

                [Parameter("string", "sender", 2, true)]
                public string sender { get; set; }

                [Parameter("int", "result", 3, false)]
                public int result { get; set; }
            }

            static async Task Main(string[] args)
            {
                var provider = "https://goerli.infura.io/v3/890a7f3aaf0d4b8eae37992a8e12a982";
                var contractAddress = "0x3bf85A914dD259440BB2CcB589127bfF21a6Ef2B";
                var abi = "[{'inputs':[{'internalType':'int256','name':'multiplier','type':'int256'}],'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'int256','name':'val','type':'int256'},{'indexed':true,'internalType':'address','name':'sender','type':'address'},{'indexed':false,'internalType':'int256','name':'result','type':'int256'}],'name':'multiplied','type':'event'},{'inputs':[{'internalType':'int256','name':'val','type':'int256'}],'name':'multiplyWithEvent','outputs':[{'internalType':'int256','name':'result','type':'int256'}],'stateMutability':'payable','type':'function'},{'inputs':[{'internalType':'int256','name':'val','type':'int256'}],'name':'multiplyWithoutEvent','outputs':[{'internalType':'int256','name':'result','type':'int256'}],'stateMutability':'view','type':'function'}]";
                var privateKey = "d56fafc4fec3addbcf487a807d76be569942d16ef7e5bd3dc05196e8844e0626";

                Account account = new Account(privateKey);
                var web3 = new Web3(account, provider);

                account.NonceService = new InMemoryNonceService(account.Address, web3.Client);
                var currentNonce = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(account.Address, Nethereum.RPC.Eth.DTOs.BlockParameter.CreatePending());
                var futureNonce = await account.NonceService.GetNextNonceAsync();
                Console.WriteLine("currNon" + currentNonce);
                Console.WriteLine("futNon" + futureNonce);

                //var documentContractAddress = "0xF628F1a6F014f1160431FADe49b47237168f13D3";
                var documentAbi = "[{'inputs':[{'internalType':'bytes32','name':'key','type':'bytes32'},{'internalType':'string','name':'name','type':'string'},{'internalType':'string','name':'description','type':'string'}],'name':'StoreDocument','outputs':[{'internalType':'bool','name':'success','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32','name':'','type':'bytes32'},{'internalType':'uint256','name':'','type':'uint256'}],'name':'documents','outputs':[{'internalType':'string','name':'name','type':'string'},{'internalType':'string','name':'description','type':'string'},{'internalType':'address','name':'sender','type':'address'}],'stateMutability':'view','type':'function'}]";
                var documentByteCode = "0x608060405234801561001057600080fd5b506105e9806100206000396000f3fe608060405234801561001057600080fd5b50600436106100365760003560e01c80634a75c0ff1461003b57806379c17cc514610063575b600080fd5b61004e610049366004610334565b610085565b60405190151581526020015b60405180910390f35b6100766100713660046103ae565b61018b565b60405161005a93929190610416565b600080604051806060016040528087878080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250505090825250604080516020601f8801819004810282018101909252868152918101919087908790819084018382808284376000920182905250938552505033602093840152508981528082526040812080546001810182559082529190208251929350839260039092020190819061013f90826104f3565b506020820151600182019061015490826104f3565b5060409190910151600290910180546001600160a01b0319166001600160a01b039092169190911790555060019695505050505050565b600060205281600052604060002081815481106101a757600080fd5b9060005260206000209060030201600091509150508060000180546101cb9061046a565b80601f01602080910402602001604051908101604052809291908181526020018280546101f79061046a565b80156102445780601f1061021957610100808354040283529160200191610244565b820191906000526020600020905b81548152906001019060200180831161022757829003601f168201915b5050505050908060010180546102599061046a565b80601f01602080910402602001604051908101604052809291908181526020018280546102859061046a565b80156102d25780601f106102a7576101008083540402835291602001916102d2565b820191906000526020600020905b8154815290600101906020018083116102b557829003601f168201915b505050600290930154919250506001600160a01b031683565b60008083601f8401126102fd57600080fd5b50813567ffffffffffffffff81111561031557600080fd5b60208301915083602082850101111561032d57600080fd5b9250929050565b60008060008060006060868803121561034c57600080fd5b85359450602086013567ffffffffffffffff8082111561036b57600080fd5b61037789838a016102eb565b9096509450604088013591508082111561039057600080fd5b5061039d888289016102eb565b969995985093965092949392505050565b600080604083850312156103c157600080fd5b50508035926020909101359150565b6000815180845260005b818110156103f6576020818501810151868301820152016103da565b506000602082860101526020601f19601f83011685010191505092915050565b60608152600061042960608301866103d0565b828103602084015261043b81866103d0565b91505060018060a01b0383166040830152949350505050565b634e487b7160e01b600052604160045260246000fd5b600181811c9082168061047e57607f821691505b60208210810361049e57634e487b7160e01b600052602260045260246000fd5b50919050565b601f8211156104ee57600081815260208120601f850160051c810160208610156104cb5750805b601f850160051c820191505b818110156104ea578281556001016104d7565b5050505b505050565b815167ffffffffffffffff81111561050d5761050d610454565b6105218161051b845461046a565b846104a4565b602080601f831160018114610556576000841561053e5750858301515b600019600386901b1c1916600185901b1785556104ea565b600085815260208120601f198616915b8281101561058557888601518255948401946001909101908401610566565b50858210156105a35787850151600019600388901b60f8161c191681555b5050505050600190811b0190555056fea264697066735822122066a7cca84f46739b25c0c61e001ec3c5678c6abea649c4b991c08af47698bc8e64736f6c63430008110033";

                ContractDeploymentClass contractDeployment = new ContractDeploymentClass(account, web3);
                Console.WriteLine("Deploying");
                var documentContractAddress = await contractDeployment.deployDocumentContract(documentByteCode, documentAbi);

                ContractService contractService = new ContractService(provider, documentContractAddress, documentAbi, account, web3);
                Console.WriteLine("Entering contract");
                await contractService.functionDTOs();
                //ContractService contractService = new ContractService(provider, contractAddress, abi, privateKey);
                //Console.WriteLine($"Multiplied value: {await contractService.Multiply(4)}");

                var balance = await web3.Eth.GetBalance.SendRequestAsync("0xeCb30b600C2a60EC9721edf57DfF7949155F03ff");
                Console.WriteLine("Account balance Wei: " + balance.Value);
                // Convert the balance into Ether from Wei (lowest unit)       
                Console.WriteLine("Account balance in Ether: " + Web3.Convert.FromWei(balance.Value));

                //Console.WriteLine("Entering event territory");
                //await contractService.MultipliedEvent();
                //var idk = await contractService.Multiply(4);
                //Console.WriteLine(idk);


            }
        }
        class ContractDeploymentClass
        {
            private readonly Account account;
            private readonly Web3 web3;
            public ContractDeploymentClass(Account account, Web3 web3)
            {
                this.web3 = web3;
                this.account = account;
            }
            public async Task<string> deployDocumentContract(string byteCode, string abi)
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                var receipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi, byteCode, account.Address, new HexBigInteger(900000), null);
                Console.WriteLine("Contract address: " + receipt.ContractAddress);
                return receipt.ContractAddress;
            }
        }
    }
}