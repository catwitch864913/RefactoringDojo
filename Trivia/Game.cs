// Refactoring Playlist: https://www.youtube.com/playlist?list=PLv3bW4BDh6I8tg1LSJoB7Ioz64s8Bcufz
// By: ITsLifeOverAll, https://github.com/ITsLifeOverAll

using Trivia;

namespace UglyTrivia;

public class Game
{
    List<Player> players = new();

    LinkedList<string> popQuestions = new LinkedList<string>();
    LinkedList<string> scienceQuestions = new LinkedList<string>();
    LinkedList<string> sportsQuestions = new LinkedList<string>();
    LinkedList<string> rockQuestions = new LinkedList<string>();

    int currentPlayer = 0;
    bool isGettingOutOfPenaltyBox;

    public Player CurrentPlayer => players[currentPlayer];

    public Game()
    {
        for (int i = 0; i < 50; i++)
        {
            popQuestions.AddLast("Pop Question " + i);
            scienceQuestions.AddLast(("Science Question " + i));
            sportsQuestions.AddLast(("Sports Question " + i));
            rockQuestions.AddLast(createRockQuestion(i));
        }
    }

    public String createRockQuestion(int index)
    {
        return "Rock Question " + index;
    }

    public bool isPlayable()
    {
        return (howManyPlayers() >= 2);
    }

    public bool AddPlayer(String playerName)
    {
        players.Add(new Player(playerName));

        Console.WriteLine(playerName + " was added");
        Console.WriteLine("They are player number " + players.Count);
        return true;
    }

    public int howManyPlayers()
    {
        return players.Count;
    }

    public void roll(int roll)
    {
        Console.WriteLine(CurrentPlayer.Name + " is the current player");
        Console.WriteLine("They have rolled a " + roll);

        if (CurrentPlayer.InPenaltyBox)
        {
            if (roll % 2 != 0)
            {
                isGettingOutOfPenaltyBox = true;

                Console.WriteLine(CurrentPlayer.Name + " is getting out of the penalty box");
                CurrentPlayer.Place += roll;
                // if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;
                CurrentPlayer.Place %= 12;

                Console.WriteLine(CurrentPlayer.Name
                                  + "'s new location is "
                                  + CurrentPlayer.Place);
                Console.WriteLine("The category is " + CurrentCategory());
                askQuestion();
            }
            else
            {
                Console.WriteLine(CurrentPlayer.Name + " is not getting out of the penalty box");
                isGettingOutOfPenaltyBox = false;
            }
        }
        else
        {
            CurrentPlayer.Place += roll;
            CurrentPlayer.Place %= 12;

            Console.WriteLine(CurrentPlayer.Name
                              + "'s new location is "
                              + CurrentPlayer.Place);
            Console.WriteLine("The category is " + CurrentCategory());
            askQuestion();
        }

    }

    private void askQuestion()
    {
        if (CurrentCategory() == "Pop")
        {
            Console.WriteLine(popQuestions.First());
            popQuestions.RemoveFirst();
        }
        if (CurrentCategory() == "Science")
        {
            Console.WriteLine(scienceQuestions.First());
            scienceQuestions.RemoveFirst();
        }
        if (CurrentCategory() == "Sports")
        {
            Console.WriteLine(sportsQuestions.First());
            sportsQuestions.RemoveFirst();
        }
        if (CurrentCategory() == "Rock")
        {
            Console.WriteLine(rockQuestions.First());
            rockQuestions.RemoveFirst();
        }
    }


    private String CurrentCategory()
    {
        var category = CurrentPlayer.Place switch
        {
            var n when (n %= 4) == 0 => "Pop",
            var n when (n %= 4) == 1 => "Science",
            var n when (n %= 4) == 2 => "Sports",
            var n when (n %= 4) == 3 => "Rock",
        };
        return category;
        // if (places[currentPlayer] == 0) return "Pop";
        // if (places[currentPlayer] == 4) return "Pop";
        // if (places[currentPlayer] == 8) return "Pop";

        // if (places[currentPlayer] == 1) return "Science";
        // if (places[currentPlayer] == 5) return "Science";
        // if (places[currentPlayer] == 9) return "Science";

        // if (places[currentPlayer] == 2) return "Sports";
        // if (places[currentPlayer] == 6) return "Sports";
        // if (places[currentPlayer] == 10) return "Sports";
        // return "Rock";
    }

    public bool wasCorrectlyAnswered()
    {
        if (CurrentPlayer.InPenaltyBox)
        {
            if (isGettingOutOfPenaltyBox)
            {
                Console.WriteLine("Answer was correct!!!!");
                CurrentPlayer.Purse++;
                Console.WriteLine(CurrentPlayer.Name
                                  + " now has "
                                  + CurrentPlayer.Purse
                                  + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
            else
            {
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;
                return true;
            }



        }
        else
        {

            Console.WriteLine("Answer was corrent!!!!");
            CurrentPlayer.Purse++;
            Console.WriteLine(CurrentPlayer.Name
                              + " now has "
                              + CurrentPlayer.Purse
                              + " Gold Coins.");

            bool winner = didPlayerWin();
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;

            return winner;
        }
    }

    public bool wrongAnswer()
    {
        Console.WriteLine("Question was incorrectly answered");
        Console.WriteLine(CurrentPlayer.Name + " was sent to the penalty box");
        CurrentPlayer.InPenaltyBox = true;

        currentPlayer++;
        if (currentPlayer == players.Count) currentPlayer = 0;
        return true;
    }


    private bool didPlayerWin()
    {
        return CurrentPlayer.Purse != 6;
    }
}