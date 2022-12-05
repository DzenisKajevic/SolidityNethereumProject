using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nethereum_Project
{
    class ContractService
    {
        private readonly Web3 web3;
        private readonly Contract contract;
        private readonly Account account;

        private static readonly HexBigInteger GAS_LIMIT = new HexBigInteger(4600000);

        public ContractService(string provider, string contractAddress, string abi, string privateKey)
        {
            this.account = new Account(privateKey);
            this.web3 = new Web3(account, provider);
            this.contract = web3.Eth.GetContract(abi, contractAddress);
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
            catch(Exception e)
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
            var filter5 = await multiplyEvent.CreateFilterAsync(7);

            //var receipt = await multiplyFunction.SendTransactionAndWaitForReceiptAsync(account.Address, GAS_LIMIT, GAS_LIMIT, null, 4);
            //var receipt5 = await multiplyFunction.SendTransactionAndWaitForReceiptAsync(account.Address, GAS_LIMIT, GAS_LIMIT, null, 5);
            
            // comment 3 lines below, uncomment 3 above

            var transactionHash = await multiplyFunction.SendTransactionAsync(account.Address, GAS_LIMIT, GAS_LIMIT, 4); // await multiplyFunction.SendTransactionAsync(account.Address, 4);
            transactionHash = await multiplyFunction.SendTransactionAsync(account.Address, GAS_LIMIT, GAS_LIMIT,  5);
            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            Console.WriteLine(receipt);

            // use the generic method to be able to decode it (<MultipliedEvent>)
            // if we have complex output, we can create "function output" or a DTO
            var log = await multiplyEvent.GetFilterChangesAsync<MultipliedEventClass>(filterAll);
            var log5 = await multiplyEvent.GetFilterChangesAsync<MultipliedEventClass>(filter5);

            Console.WriteLine("log count: " + log.Count);
            Console.WriteLine("log5 count: " + log5.Count);
            Console.WriteLine("log5 first parameter (input): " + log5[0].Event.multiplicationInput);
            Console.WriteLine("log5 result: " + log5[0].Event.result);
            Console.WriteLine("complete log" + log);
        }

        public async Task functionDTOs()
        {

        }

        // class that can be decoded as the event output
        // event multiplied(int256 indexed val, address indexed sender, int256 result);
        public class MultipliedEventClass
        {
            public int multiplicationInput { get; set; }

            public string sender { get; set; }

            public int result { get; set; }

        }

        static async Task Main(string[] args)
        {
            var provider = "https://goerli.infura.io/v3/890a7f3aaf0d4b8eae37992a8e12a982";
            var contractAddress = "0x3bf85A914dD259440BB2CcB589127bfF21a6Ef2B";
            var abi = "[{'inputs':[{'internalType':'int256','name':'multiplier','type':'int256'}],'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'int256','name':'val','type':'int256'},{'indexed':true,'internalType':'address','name':'sender','type':'address'},{'indexed':false,'internalType':'int256','name':'result','type':'int256'}],'name':'multiplied','type':'event'},{'inputs':[{'internalType':'int256','name':'val','type':'int256'}],'name':'multiplyWithEvent','outputs':[{'internalType':'int256','name':'result','type':'int256'}],'stateMutability':'payable','type':'function'},{'inputs':[{'internalType':'int256','name':'val','type':'int256'}],'name':'multiplyWithoutEvent','outputs':[{'internalType':'int256','name':'result','type':'int256'}],'stateMutability':'view','type':'function'}]";
            var privateKey = "d56fafc4fec3addbcf487a807d76be569942d16ef7e5bd3dc05196e8844e0626";
            

            ContractService contractService = new ContractService(provider, contractAddress, abi, privateKey);
            Console.WriteLine($"Multiplied value: {await contractService.Multiply(4)}");

            Console.WriteLine("Entering event territory");
            await contractService.MultipliedEvent();
            //var idk = await contractService.Multiply(4);
            //Console.WriteLine(idk);
        }
    }
}