// SPDX-License-Identifier: MIT
pragma solidity >=0.8.7;

// Created AccessControl contract which will be inherited later by Leaderboard
contract AccessControl {
    // indexing allows us to filter through the logs faster
    event GrantRole(bytes32 indexed role, address indexed account);
    event RevokeRole(bytes32 indexed role, address indexed account);

    mapping(bytes32 => mapping(address => bool)) public roles;

    // private is cheaper, but public allows us to see the hash for the role
    bytes32 public constant ADMIN = keccak256(abi.encodePacked("ADMIN"));

    // owner gets the ADMIN role
    constructor() {
        _grantRole(ADMIN, msg.sender);
    }

    // if a contract inherits AccessControl, it will be able to call _grantRole (internal)
    function _grantRole(bytes32 _role, address _account) internal {
        // roles[_role] returns mapping(address => bool)
        // and if we access that one:
        // roles[_role][_account], we will get access to the boolean
        roles[_role][_account] = true;
        emit GrantRole(_role, _account);
    }

    function hasRole(bytes32 _role, address _account)
        public
        view
        virtual
        returns (bool)
    {
        return roles[_role][_account];
    }

    function bytes32ToStr(bytes32 _bytes32)
        public
        pure
        returns (string memory resultingString)
    {
        uint8 i = 0;
        while (i < 32 && _bytes32[i] != 0) {
            i++;
        }
        bytes memory bytesArray = new bytes(i);
        for (i = 0; i < 32 && _bytes32[i] != 0; i++) {
            bytesArray[i] = _bytes32[i];
        }
        return string(bytesArray);
    }

    modifier onlyRole(bytes32 _role) {
        require(roles[_role][msg.sender], "Unauthorized: Role not present");
        _;
    }

    function grantRole(bytes32 _role, address _account)
        external
        onlyRole(ADMIN)
    {
        _grantRole(_role, _account);
    }

    function revokeRole(bytes32 _role, address _account)
        external
        onlyRole(ADMIN)
    {
        roles[_role][_account] = false;
        emit RevokeRole(_role, _account);
    }
}

contract Leaderboard is AccessControl {
    address owner;
    uint8 maxLeaderboardLength = 5;
    uint8 leaderboardLength = 0;

    // create an array of ScoreSubmissions
    mapping(uint256 => ScoreSubmission) public leaderboard;

    // each submission tracks the account address and score
    struct ScoreSubmission {
        address scoreHolder;
        uint256 score;
    }

    constructor() {
        owner = msg.sender;
    }

    // allows owner only
    modifier onlyOwner() {
        require(owner == msg.sender, "Sender not authorized");
        _;
    }

    function getLeaderboardLength() public view returns (uint8) {
        return leaderboardLength;
    }

    function getLeaderboard()
        public
        view
        returns (address[] memory, uint256[] memory)
    {
        if (leaderboardLength == 0) {
            address[] memory addresses;
            uint256[] memory scores;
            return (addresses, scores);
        } else {
            address[] memory addresses = new address[](leaderboardLength);
            uint256[] memory scores = new uint256[](leaderboardLength);

            for (uint8 i = 0; i < leaderboardLength; i++) {
                addresses[i] = leaderboard[i].scoreHolder;
                scores[i] = leaderboard[i].score;
            }
            return (addresses, scores);
        }
    }

    //Emitted when a player submits their score to the leaderboard.
    event UploadScore(address indexed scoreHolder, uint256 score);

    // owner calls to update leaderboard
    function addScore(address scoreHolderAddress, uint256 score)
        public
        onlyRole(ADMIN)
        returns (bool addedToLeaderboard)
    {
        if (leaderboardLength < 5) {
            leaderboard[leaderboardLength] = ScoreSubmission({
                scoreHolder: scoreHolderAddress,
                score: score
            });
            leaderboardLength++;
            emit UploadScore(scoreHolderAddress, score);
            return true;
        }

        // if the score is too low, don't update
        if (leaderboard[maxLeaderboardLength - 1].score >= score) return false;

        // loop through the leaderboard
        for (uint256 i = 0; i < maxLeaderboardLength; i++) {
            // find where to insert the new score
            if (leaderboard[i].score < score) {
                // shift leaderboard
                ScoreSubmission memory currentUser = leaderboard[i];
                for (uint256 j = i + 1; j < maxLeaderboardLength + 1; j++) {
                    ScoreSubmission memory nextUser = leaderboard[j];
                    leaderboard[j] = currentUser;
                    currentUser = nextUser;
                }

                // insert
                leaderboard[i] = ScoreSubmission({
                    scoreHolder: scoreHolderAddress,
                    score: score
                });

                // delete last from list
                delete leaderboard[maxLeaderboardLength];

                return true;
            }
        }
    }
}
