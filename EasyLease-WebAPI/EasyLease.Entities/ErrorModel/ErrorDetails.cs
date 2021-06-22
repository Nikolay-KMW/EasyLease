using System.Text.Json;

namespace EasyLease.Entities.ErrorModel {
    public class ErrorDetails {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public byte[] ToUtf8Bytes() => JsonSerializer.SerializeToUtf8Bytes(this);

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}