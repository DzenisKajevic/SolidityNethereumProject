using Nethereum.Hex.HexTypes;
using System;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Nethereum.RPC.TransactionManagers;

namespace Nethereum_Project
{
    public class TestClass
    {
        private static readonly HexBigInteger GAS = new HexBigInteger(4600000);
//        static async Task Main(string[] args) {
//            await ShouldBeAbleToDeployAContract();
//        }

        [Fact]
        static async Task ShouldBeAbleToDeployAContract()
        {
            var senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            var password = "password";
            var abi = @"[{'inputs':[{'internalType':'int256','name':'multiplier','type':'int256'}],'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'int256','name':'val','type':'int256'},{'indexed':true,'internalType':'address','name':'sender','type':'address'},{'indexed':false,'internalType':'int256','name':'result','type':'int256'}],'name':'multiplied','type':'event'},{'inputs':[{'internalType':'int256','name':'val','type':'int256'}],'name':'multiply','outputs':[{'internalType':'int256','name':'result','type':'int256'}],'stateMutability':'nonpayable','type':'function'}]";
            var byteCode = "0x608060405234801561001057600080fd5b506040516101a83803806101a883398101604081905261002f91610037565b600055610050565b60006020828403121561004957600080fd5b5051919050565b6101498061005f6000396000f3fe608060405234801561001057600080fd5b506004361061002b5760003560e01c80631df4f14414610030575b600080fd5b61004361003e3660046100ae565b610055565b60405190815260200160405180910390f35b6000805461006390836100dd565b9050336001600160a01b0316827fe4b5609d42bdc604de81c0e89871802d367c2ce35a352c14630cc79e63a3facc836040516100a191815260200190565b60405180910390a3919050565b6000602082840312156100c057600080fd5b5035919050565b634e487b7160e01b600052601160045260246000fd5b80820260008212600160ff1b841416156100f9576100f96100c7565b818105831482151761010d5761010d6100c7565b9291505056fea2646970667358221220482cc13c675efa428e1e7c17898fc8edd7673cfceb15193a2ea053ee271ef95f64736f6c63430008110033";

            var multiplier = 7;

            var web3Geth = new Nethereum.Geth.Web3Geth("http://127.0.0.1:8545");
            var web3 = new Nethereum.Web3.Web3(); //defalut RPC 8545
            var unlockAccountResult = await web3Geth.Personal.UnlockAccount.SendRequestAsync
                (senderAddress, password, 120);
            Assert.True(unlockAccountResult);

            var transactionManager = web3.TransactionManager;

            /*
            web3Geth.TransactionManager.DefaultGas = transactionManager.DefaultGas;
            web3Geth.TransactionManager.DefaultGasPrice = transactionManager.DefaultGasPrice;
*/
            // returned before the transaction is mined
            //, new HexBigInteger(transactionManager.DefaultGas)
            var transactionHash = await web3Geth.Eth.DeployContract.SendRequestAsync(abi, byteCode, senderAddress, GAS, multiplier);

            // if we post this on a public chain, we don't need to mine it, but since this is on a private chain, we do need to do it
            var mineResult = await web3Geth.Miner.Start.SendRequestAsync(6);

            Assert.True(mineResult);

            // receipt of the transaction that will contain the contract adddress
            // receipt contains various details about the transaction, including whether it was mined successfully or not,
            // how much the Gas Price was, etc.
            var receipt = await web3Geth.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            // if the transaction hasn't been mined yet, the receipt will be null
            while(receipt == null)
            {
                Thread.Sleep(5000);
                receipt = await web3Geth.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }

            Console.WriteLine(receipt);
            var contractAddress = receipt.ContractAddress;

            // creates contract wrapper from the contract address & abi
            var contract = web3.Eth.GetContract(abi, contractAddress);

            var multiplyFunction = contract.GetFunction("multiply");

            // call vs transcation
            // call is execution of a function within a blockchain without any verification
            // call and get the data, it doesn't require any kind of consensus from the chain
            // transaction -> submits result, requires consensus from the chain, doesn't return anything

            // events -> Put information on the log (does not need to be paid)
            // Events allow the convenient usage of the EVM logging facilities, which in turn can be used
            // to “call” JavaScript callbacks in the user interface of a dapp, which listen for these events.
            // log information can be retrieved using an ethereum client or something like nethereum 


            var result = await multiplyFunction.CallAsync<int>(7);

            Assert.Equal(49, result);
        }
    }
}
