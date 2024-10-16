
namespace Trivia;

public enum QuestionType
{
    Pop,
    Science,
    Sports,
    Rock
}

public class Questions
{
    private const int maxQuestionCount = 50;
    public Dictionary<QuestionType, LinkedList<string>> QuestionDictionary { get; } = new();

    public Questions(List<QuestionType> questionTypes)
    {
        foreach (var questionType in questionTypes)
        {
            var list = new LinkedList<string>();

            for (int i = 0; i < maxQuestionCount; i++)
            {
                list.AddLast($"{questionType} Question{i}");
            }
            QuestionDictionary.Add(questionType, list);
        }
    }

    public string TakeQuestion(QuestionType category)
    {
        var question = QuestionDictionary[category].First();
        QuestionDictionary[category].RemoveFirst();
        return question;
    }
}