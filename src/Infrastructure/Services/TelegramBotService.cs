﻿using LightsOn.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LightsOn.Infrastructure.Services;

public class TelegramBotService : ITelegramBot
{
    private readonly TelegramBotClient _botClient;
    private readonly List<long> _allowedChatIds;
    private readonly ILogger<TelegramBotService> _logger;

    public TelegramBotService(IConfiguration configuration, ILogger<TelegramBotService> logger)
    {
        _logger = logger;

        var token = configuration["TelegramBotSettings:Token"];
        var chatIdsValue = configuration["TelegramBotSettings:AllowedChatIds"];
        var chatIds = chatIdsValue?.Split(',').Select(long.Parse).ToList();

        _botClient = new TelegramBotClient(token!);
        _allowedChatIds = chatIds ?? [];
    }

    public async Task SendMessageToAllowedUsers(string message)
    {
        foreach (var chatId in _allowedChatIds)
        {
            try
            {
                await _botClient.SendTextMessageAsync(chatId, message);
                _logger.LogInformation($"Message sent to {chatId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send message to {chatId}: {ex.Message}");
            }
        }
    }
}