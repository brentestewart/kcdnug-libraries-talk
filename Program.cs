﻿// See https://aka.ms/new-console-template for more information
using Polly.Retry;
using Polly;
using System.Threading.Tasks;
using System;

class Program
{
    static AsyncRetryPolicy retryPolicy = Policy
        .Handle<HttpRequestException>()  // Handles transient HTTP request exceptions
        .WaitAndRetryAsync(3,  // Retry up to 3 times
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),  // Exponential backoff (2, 4, 8 seconds)
            onRetry: (exception, timeSpan, retryCount, context) =>
            {
                Console.WriteLine($"Retry {retryCount} after {timeSpan.TotalSeconds} seconds due to: {exception.Message}");
            });

    static async Task Main(string[] args)
    {
        // Simulate calling an API
        var httpClient = new HttpClient();
        var apiUrl = "https://jsonplaceholder.typicode.com/posts";

        // Make a request with retry logic
        try
        {
            var response = await CallApiWithRetry(httpClient, apiUrl);
            Console.WriteLine($"Response received with status code: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed after retries: {ex.Message}");
        }

        Console.ReadKey();
    }

    static async Task<HttpResponseMessage> CallApiWithRetry(HttpClient httpClient, string url)
    {
        // Execute the HTTP request with Polly's retry policy
        return await retryPolicy.ExecuteAsync(async () =>
        {
            Console.WriteLine("Making HTTP request...");
            // Simulate random failures
            Random rand = new Random();
            if (rand.Next(0, 20) > 3)  // Randomly fail
            {
                throw new HttpRequestException("Simulated request failure!");
            }
            else
            {
                return await httpClient.GetAsync(url);  // Simulated successful response
            }
        });
    }
}