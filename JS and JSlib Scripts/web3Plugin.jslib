// Neth.Flappy web3
// plugin for calling js functions through C#

mergeInto(LibraryManager.library, {
    GetAccount: function () {
        var account = '';
        if (typeof window.ethereum !== 'undefined' && window.ethereum.isMetaMask) {
            account = window.ethereum.selectedAddress;
            if (typeof account === 'undefined') {
                account = '';
            }
        }
        var buffer = _malloc(lengthBytesUTF8(account) + 1);
        stringToUTF8(account, buffer, account.length + 1);
        return buffer;
    },

    SendTransaction: function (to, data, returnObj, returnFunc) {
        var tostr = Pointer_stringify(to);
        var wei = parseFloat(Pointer_stringify(data)) * Math.pow(10, 18);
        console.log(wei);
        var from = "";

        //save variables in scope
        var gameObject = Pointer_stringify(returnObj);
        var callback = Pointer_stringify(returnFunc);

        console.log("Outside Scope Callback : " + gameObject);


        ethereum.request({ method: 'eth_accounts' }).then(function (response) {
            from = response[0];
            var args = {
                "from": from,
                "to": tostr,
                "value": wei.toString(16) //convert to hex
                //"data": datastr
            };

            ethereum.request({ method: 'eth_sendTransaction', params: [args], }).then(function (txHash) {
                console.log("TxHash: " + txHash);
                console.log("My GameObject: " + gameObject);
                console.log("My Function: " + callback);
                // similar to the Unity sendMessage function, but here we pass in the GameObject, the function on a script
                // that is on that GameObject, as well as the data associated with that

                // the GameObject and Function name had to be hard-coded since they would get lost in Unity due to WebGL not being able 
                // to handle async actions properly
                unityInstance.SendMessage("Ethereum", "TransactionCallback", txHash);
            });

        });
    },
});
