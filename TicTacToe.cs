using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    //Game configurations
    string menuHint = "You may type menu at anytime";
    string[] boardGame = {"_", "_", "_", "_", "_", "_", "_", "_","_"};
    int numberOfPieces = 0;
    //Game States
    enum Screen { MainMenu, Play, EndGame };
    enum playerTurn { P1, P2 };
    Screen currentScreen = Screen.MainMenu;
    playerTurn currentTurn = playerTurn.P1;
    int turn = 1;
    string xo = "x";
    //Number of Wins in game
    int xWins = 0; // Player 1 wins
    int oWins = 0; // Player 2 wins
    void Start()
    {
        Terminal.WriteLine("Welcome to Tic Tac Toe!");
        Terminal.WriteLine("Player 1 will be X\nPlayer 2 will be O");
        showMainMenu();
    }

    void OnUserInput(string input) 
    {
        if(input == "menu") { // we can always go to direct to main menu
            Terminal.ClearScreen();
            showMainMenu();
            numberOfPieces = 0;
        }
        else if (currentScreen == Screen.MainMenu) {
            runMainMenu(input);
        }
        else if (currentScreen == Screen.Play) {
            play(input);
        }
        else if (currentScreen == Screen.EndGame) {
            resetBoard();
            Terminal.WriteLine(menuHint);
        }
    }

    void showMainMenu()
    {
        resetBoard();
        currentScreen = Screen.MainMenu;
        Terminal.WriteLine("Player 1 Wins = " + xWins + "\nPlayer 2 Wins = " + oWins);
        Terminal.WriteLine("Press 1 to start the game.");
    }

    void runMainMenu(string input)
    {
        Terminal.ClearScreen();
        checkPlayerTurn();
        if(input == "1")
        {
            currentScreen = Screen.Play;
            Terminal.WriteLine("Enter in your row and column between 1 and 3\nFor example, like this: 1 3");
            Terminal.WriteLine("Player Turn: " + turn);
            printBoardGame();
        }
        else{Terminal.WriteLine("That is not 1");}
    }
    
    void play(string input)
    { 
        Terminal.ClearScreen();
        checkPlayerTurn();
        Terminal.WriteLine("Enter in your row and column between 1 and 3\nFor example, like this: 1 3");
        if(input.Length < 3){Terminal.WriteLine("Invalid input");}//prevent out of bounds
        else if(isValidSpot(input) && isSpotEmpty(input))
        {
            numberOfPieces++;
            boardGame[(int.Parse(input[0] + "") - 1) * 3 + (int.Parse(input[2] + "") - 1)] = xo;
            if (numberOfPieces >= 5){checkWin();}
            if (numberOfPieces == 9 && currentScreen != Screen.EndGame)
            {
                currentScreen = Screen.EndGame;
                Terminal.WriteLine("DRAW!");
                Terminal.WriteLine(menuHint);
            }
            switchPlayerTurn();
        }
        else if(isValidSpot(input))
        {
            Terminal.WriteLine("Spot already taken.");
        }
        else{Terminal.WriteLine("Invalid input");}
        Terminal.WriteLine("Player Turn: " + turn);
        printBoardGame();
    }

    void printBoardGame()
    {
        Terminal.WriteLine(@"
_" + boardGame[0] + "_|_" + boardGame[1] + "_|_" + boardGame[2] + 
"_\n_" + boardGame[3] + "_|_" + boardGame[4] + "_|_" + boardGame[5] + 
"_\n_" + boardGame[6] + "_|_" + boardGame[7] + "_|_" + boardGame[8] + "_");
    }
    
    void checkPlayerTurn()
    {
        if(currentTurn == playerTurn.P1)
        {
            turn = 1;
        } 
        else if(currentTurn == playerTurn.P2)
        {
            turn = 2;
        }
    }

    void switchPlayerTurn()
    {
        if(currentTurn == playerTurn.P1)
        {
            currentTurn = playerTurn.P2;
            turn = 2;
            xo = "o";
        } 
        else if(currentTurn == playerTurn.P2)
        {
            currentTurn = playerTurn.P1;
            turn = 1;
            xo = "x";
        }
    }

    bool isValidSpot(string input)
    {
        input += "   "; //this is to prevent outOfBounds errors
        if(input[0] >= '1' && input[2] <= '3')
        {
            return true;
        }
        else{ return false;}
    }

    bool isSpotEmpty(string input)
    {
        int spot = (int.Parse(input[0] + "") - 1) * 3 + (int.Parse(input[2] + "") - 1);
        if(boardGame[spot] == "x" ||boardGame[spot] == "o")
        {
            return false;
        }
        else {return true;}
    }

    void checkWin()
    {
        if( (boardGame[0] == boardGame[1] && boardGame[0] == boardGame[2] && boardGame[0] != "_") || //horizontal row 1
            (boardGame[3] == boardGame[4] && boardGame[3] == boardGame[5] && boardGame[3] != "_") ||//horizontal row 2
            (boardGame[6] == boardGame[7] && boardGame[6] == boardGame[8] && boardGame[6] != "_") ||//horizontal row 3
            (boardGame[0] == boardGame[3] && boardGame[0] == boardGame[6] && boardGame[0] != "_") ||//vertical row 1
            (boardGame[1] == boardGame[4] && boardGame[1] == boardGame[7] && boardGame[1] != "_") ||//vertical row 2
            (boardGame[2] == boardGame[5] && boardGame[2] == boardGame[8] && boardGame[2] != "_") ||//vertical row 3
            (boardGame[0] == boardGame[4] && boardGame[0] == boardGame[8] && boardGame[0] != "_") ||//diagonal 1 check
            (boardGame[2] == boardGame[4] && boardGame[2] == boardGame[6] && boardGame[2] != "_"))//diagonal 2 check
            {
                currentScreen = Screen.EndGame;
                Terminal.WriteLine("Player " + turn + " wins!\nGame Over");
                if(turn == 1){ xWins++;}
                else{oWins++;}
                Terminal.WriteLine(menuHint);
            }
    }

    void resetBoard()
    {
        for(int i = 0; i < boardGame.Length; i++)
        {
            boardGame[i] = "_";
        }
    }
}
