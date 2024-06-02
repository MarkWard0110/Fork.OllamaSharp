using OllamaSharp;
using OllamaSharp.Models;
using OllamaSharp.Models.Chat;
using OllamaSharp.Streamer;
using System.Threading;

namespace Tests;

public class TestOllamaApiClient : IOllamaApiClient
{
	private ChatRole _role;
	private string _answer = string.Empty;

	public string SelectedModel { get; set; } = string.Empty;

	public Task CopyModel(CopyModelRequest request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task CreateModel(CreateModelRequest request, IResponseStreamer<CreateStatus> streamer, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task DeleteModel(string model, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<GenerateEmbeddingResponse> GenerateEmbeddings(GenerateEmbeddingRequest request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<ConversationContextWithResponse> GetCompletion(GenerateCompletionRequest request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Model>> ListLocalModels(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task PullModel(PullModelRequest request, IResponseStreamer<PullStatus> streamer, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task PushModel(PushRequest request, IResponseStreamer<PushStatus> streamer, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<ShowModelResponse> ShowModelInformation(string model, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task<ConversationResponse> SendChat(ChatRequest chatRequest, Action<ChatResponseStream> streamer, CancellationToken cancellationToken)
	{
		var message = new Message(_role, _answer);
		streamer(new ChatResponseStream { Done = true, Message = message, CreatedAt = DateTime.UtcNow.ToString(), Model = chatRequest.Model });

		await Task.Yield();

		var response = new ConversationResponse(message);
        return response;

    }

	public async Task<ConversationResponse> SendChat(ChatRequest chatRequest, IResponseStreamer<ChatResponseStream> streamer, CancellationToken cancellationToken)
	{
		var message = new Message(_role, _answer);
		streamer.Stream(new ChatResponseStream { Done = true, Message = message, CreatedAt = DateTime.UtcNow.ToString(), Model = chatRequest.Model });

		await Task.Yield();

        var response = new ConversationResponse(message);
        return response;
    }

	public Task<ConversationContext> StreamCompletion(GenerateCompletionRequest request, IResponseStreamer<GenerateCompletionResponseStream> streamer, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	internal void DefineChatResponse(ChatRole role, string answer)
	{
		_role = role;
		_answer = answer;
	}
}