using Microsoft.EntityFrameworkCore;
using ToEat.Data;
using ToEat.Models;

namespace ToEat.Services;

public class ConversationService
{
    private readonly ToEatContext _context;
    private readonly ChatCompletionService _chatCompletionService;
    public ConversationService(ToEatContext context, ChatCompletionService chatCompletionService)
    {
        _context = context;
        _chatCompletionService = chatCompletionService;
    }

    public async Task<Conversation> CreateNewConversation(int metaPromptElementId)
    {
        var metaPromptElement = await _context.MetaPromptElements.FindAsync(metaPromptElementId);
        if (metaPromptElement == null)
        {
            return null;
        }
        var conversation = new Conversation();
        conversation.Messages.Add(new Message { Text = metaPromptElement.Text, Role = "system" });
        _context.Conversations.Add(conversation);

        await _context.SaveChangesAsync();

        return conversation;
    }

    public async Task<Conversation> AddMessage(int conversationId, string text)
    {
        var conversation = await _context.Conversations.FindAsync(conversationId);
        if (conversation == null)
        {
            return null;
        }
        conversation.Messages.Add(new Message { Text = text, Role = "user" });
        await _context.SaveChangesAsync();

        return conversation;
    }

    public async Task<string> GetAnswer(int conversationId)
    {
        var conversationWithMessages = _context.Conversations
            .Include(c => c.Messages)
            .FirstOrDefault(c => c.Id == conversationId);
        if (conversationWithMessages == null)
        {
            return null;
        }
        var answer = await _chatCompletionService.GetAnswer(conversationWithMessages);
        conversationWithMessages.Messages.Add(new Message { Text = answer, Role = "assistant" });
        _context.SaveChanges();
        
        return answer;
    }
}