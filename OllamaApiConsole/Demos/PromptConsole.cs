using OllamaSharp;
using OllamaSharp.Models;
using OllamaSharp.Streamer;
using Spectre.Console;
using System.IO;

public class PromptConsole : OllamaConsole
{
	public PromptConsole(IOllamaApiClient ollama)
		: base(ollama)
	{
	}

	public override async Task Run()
	{
		AnsiConsole.Write(new Rule("Prmopt demo").LeftJustified());
		AnsiConsole.WriteLine();

		Ollama.SelectedModel = await SelectModel("Select a model you want to invoke:");

		if (!string.IsNullOrEmpty(Ollama.SelectedModel))
		{
			AnsiConsole.MarkupLine($"You are talking to [blue]{Ollama.SelectedModel}[/] now.");
			AnsiConsole.MarkupLine("[gray]Type \"[red]exit[/]\" to leave.[/]");

			
			string message;

			do
			{
				AnsiConsole.WriteLine();
				message = ReadInput();

				if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
					break;

				var response = await Ollama.GetCompletion(message, null);
				AnsiConsole.MarkupInterpolated($"[cyan]{response.Response ?? ""}[/]");

				var tokensPerSecond = response.Metadata.EvalCount / (response.Metadata.EvalDuration / 1e9);
				AnsiConsole.MarkupInterpolated($"[gray]TPS: {tokensPerSecond}[/]");

                AnsiConsole.WriteLine();
			} while (!string.IsNullOrEmpty(message));
		}
	}
}