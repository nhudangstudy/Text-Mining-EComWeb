using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public record ProblemModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Type { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; init; }

        [Required]
        [Range(400, 511)]
        public int Status { get; init; }

        [Required]
        public string Detail { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Instance { get; init; }

        [Required]
        [JsonExtensionData]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, object?>? Extensions { get; init; }

        public ProblemModel
            (
                Exception exception,
                string? instance = null,
                int status = StatusCodes.Status500InternalServerError,
                string? title = null
            )
        {
            Title ??= title;
            Status = status;
            Instance ??= instance;
            if (exception != null)
            {
                Detail = exception.Message;
                Type ??= exception.GetType().FullName;
            }
            else
            {
                Detail = "Error.";
            }
        }

        public ProblemModel
            (
                string detail,
                string? instance = null,
                [Range(400, 511)] int status = StatusCodes.Status500InternalServerError,
                string? title = null
            )
        {
            Title ??= title;
            Status = status;
            Instance ??= instance;
            Detail = detail;
        }

        public ObjectResult Response() => new(this)
        {
            StatusCode = Status
        };

        public static readonly ProblemModel Unauthorized = new("The method require authorization to execute.", "/token", 401, "Unauthorized");
        public static readonly ProblemModel AcessTokenFormatWrong = new("Enter the access token to Authorization field in Header with <bearer> is prefix and try again.", "/token", 412, "The access token does not match format.");
        public static readonly ProblemModel InvalidToken = new("Your token is invalid.", "/token", 401, "Invalid Token");
        public static readonly ProblemModel Forbidden = new("You are not allowed call to this resource.", null, 403, "Forbidden");
        public static readonly ProblemModel AccountNotFound = new("Account does not exist.", null, 404, "Not Found");
    }
}
