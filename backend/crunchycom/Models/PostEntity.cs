namespace CrunchyCom.Models;

using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

public class PostEntity : ITableEntity
{
    public required string PartitionKey { get; set; }
    
    public required string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    public string? Title { get; set; }
    public string? Content { get; set; }
	public string? Author { get;set; } 
    public DateTime? Published { get; set; }
}