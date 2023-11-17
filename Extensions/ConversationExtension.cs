
using ToEat.Models;
using Azure.AI.OpenAI;
namespace ToEat.Extensions;

public static class ConversationExtension
{
    public static List<ChatMessage> ToOpenAiConversation(this Conversation conversation)
    {
        var messages = new List<ChatMessage>();
        foreach (var message in conversation.Messages)
        {
            if (message.Role == "user")
            {
                messages.Add(new ChatMessage(ChatRole.User, message.Text));
            }
            if (message.Role == "system")
            {
                messages.Add(new ChatMessage(ChatRole.System, message.Text));
            }
            if (message.Role == "assistant")
            {
                messages.Add(new ChatMessage(ChatRole.Assistant, message.Text));
            }
        }
        return messages;
    }
}